// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Cysharp.Threading.Tasks;
using Features.Hero.Signals;
using Features.Mission.Data;
using Features.Mission.Models;
using Features.Mission.Signals;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.Mission.Views
{
    public class MissionInfoWindow : BaseMissionWindow
    {
        [SerializeField] private Image _cover;
        [SerializeField] private TMP_Text _nameField;
        [SerializeField] private TMP_Text _descriptionField;
        [SerializeField] private Button _startButton;
        [SerializeField] private TMP_Text _buttonTitleField;

        private MissionModel _model;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(MissionModel model, SignalBus signalBus)
        {
            _model = model;
            _signalBus = signalBus;
        }

        public override async void Show()
        {
            base.Show(); 
            DisplayData();
            
            var couldBeStarted = false;
            RectTransform.anchoredPosition = Vector2.zero;           

            _signalBus
                .GetStream<HeroSignals.SelectHero>()
                .Subscribe(_ => couldBeStarted = true)
                .AddTo(this);

            _startButton
                .OnClickAsObservable()
                .Subscribe(_ => _signalBus.TryFire(new MissionSignals.StartMission(_model.ID)))
                .AddTo(this);

            await UniTask.WaitUntil(() => couldBeStarted);

            var completed = _model.CurrentState == MissionStateType.Completed;
            
            _startButton.interactable = !completed;
            _buttonTitleField.text = completed 
                ? $"{MissionConsts.MissionCompletedMessage}" 
                : $"{MissionConsts.MissionAcceptMessage}";
        }

        private void DisplayData()
        {
            _nameField.text = $"{_model.Data.Name}";
            _cover.sprite = _model.Data.Cover;
            _descriptionField.text = $"{_model.Data.Description}";
            _buttonTitleField.text = $"{MissionConsts.NeedToSelectHeroMessage}";
            _startButton.interactable = false;
        }

        public override void Close()
        {
            Destroy(gameObject);
        }
    }
}