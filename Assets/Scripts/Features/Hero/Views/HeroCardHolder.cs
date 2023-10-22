// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using DG.Tweening;
using Features.Hero.Data;
using Features.Hero.Signals;
using Features.Map.Signals;
using Features.Mission.Signals;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.Hero.Views
{
    public class HeroCardHolder : MonoBehaviour
    {
        [SerializeField] private ToggleGroup _toggleGroup;
        [SerializeField] private RectTransform _layout;

        private readonly ReactiveDictionary<Toggle, HeroCardView> _toggleMap = new ();
        
        private SignalBus _signalBus;
        private bool _groupState;
        private Tween _moveTween;

        private IDisposable _selectionStream;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        private void Start()
        {
            SetTogglesActive(false);
            ToggleVisibility(false);

            _signalBus
                .GetStream<MapSignals.SelectNode>()
                .Subscribe(_ =>
                {
                    ToggleVisibility(true);
                    SetTogglesActive(true);
                })
                .AddTo(this);
            
            _signalBus
                .GetStream<MissionSignals.StartMission>()
                .Subscribe(_ =>
                {
                    SetTogglesActive(false);
                    ToggleVisibility(false);
                    _toggleGroup.SetAllTogglesOff();
                })
                .AddTo(this);
        }

        private void ToggleVisibility(bool status)
        {
            _moveTween?.Kill();
            _moveTween = transform.DOMoveY(status 
                ? HeroConsts.HolderAppearRegion 
                : HeroConsts.HolderDisappearRegion, 0.25f);
        }
        
        public void AddCard(HeroCardView cardView)
        {
            cardView.SetToggleGroup(_toggleGroup);
            cardView.SelectionToggle.interactable = _groupState;
            cardView.transform.SetParent(_layout.transform);
            
            _toggleMap.Add(cardView.SelectionToggle, cardView);
            _selectionStream?.Dispose();
            _selectionStream = AssembleSelection()
                .Subscribe(heroType => _signalBus
                    .TryFire(new HeroSignals.SelectHero(heroType)));
        }
        
        private IObservable<HeroType> AssembleSelection()
        {
            var selectionObservable = Observable.Empty<HeroType>();

            foreach (var cardView in _toggleMap.Values)
                selectionObservable = selectionObservable
                    .Merge(cardView.OnSelection());

            return selectionObservable;
        }

        private void SetTogglesActive(bool value)
        {
            _groupState = value;
            foreach (var toggle in _toggleMap.Keys)
            {
                toggle.interactable = value;
            }
        }
    }
}