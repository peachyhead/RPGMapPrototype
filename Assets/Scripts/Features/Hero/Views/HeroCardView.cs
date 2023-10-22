// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using Features.Hero.Data;
using Features.Hero.Models;
using Features.Hero.Signals;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.Hero.Views
{
    public class HeroCardView : MonoBehaviour
    {
        public HeroType Type => _model.Data.Type;
        public Toggle SelectionToggle => _selectionToggle;
        
        [SerializeField] private TMP_Text _pointsField;
        [SerializeField] private Image _cover;
        [SerializeField] private TMP_Text _nameField;
        [SerializeField] private Toggle _selectionToggle;

        private readonly Subject<HeroType> _selectionSubject = new ();

        private HeroModel _model;

        [Inject]
        public void Construct(HeroModel model)
        {
            _model = model;
        }

        private void Start()
        {
            _nameField.text = _model.Data.ID;
            _cover.sprite = _model.Data.Cover;
            
            _model
                .OnPointsChange()
                .Subscribe(amount => _pointsField.text = $"{amount}")
                .AddTo(this);

            _selectionToggle
                .OnValueChangedAsObservable()
                .Where(value => value)
                .Subscribe(_ => _selectionSubject.OnNext(Type))
                .AddTo(this);
        }

        public void SetToggleGroup(ToggleGroup group)
        {
            _selectionToggle.group = group;
        }

        public IObservable<HeroType> OnSelection()
        {
            return _selectionSubject.AsObservable();
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}