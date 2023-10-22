// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System.Collections.Generic;
using System.Linq;
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
    public class MissionResultWindow : BaseMissionWindow
    {
        [SerializeField] private TMP_Text _nameField;
        [SerializeField] private Image _cover;
        [SerializeField] private TMP_Text _storyField;
        [SerializeField] private TMP_Text _alliesField;
        [SerializeField] private TMP_Text _opponentsField;
        [SerializeField] private Button _completeButton;
        [SerializeField] private TMP_Text _buttonTitleField;

        private MissionModel _model;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(MissionModel data, SignalBus signalBus)
        {
            _model = data;
            _signalBus = signalBus;
        }

        public override void Show()
        {
            base.Show();
            _nameField.text = $"{_model.Data.Name}";
            _cover.sprite = _model.Data.Cover;
            _storyField.text = $"{_model.Data.MissionStory}";
            _buttonTitleField.text = $"{MissionConsts.MissionBeginMessage}";
            
            var alliesAssembled = AssembleTeamLine(_model.Data.Allies);
            var opponentsAssembled = AssembleTeamLine(_model.Data.Opponents);
            
            _alliesField.text = $"{alliesAssembled}";
            _opponentsField.text = $"{opponentsAssembled}";
            
            _completeButton
                .OnClickAsObservable()
                .Subscribe(_ => _signalBus.TryFire(new MissionSignals.CompleteMission(_model.ID)))
                .AddTo(this);
        }

        public string AssembleTeamLine(List<string> targetList)
        {
            return targetList.Skip(1).Aggregate(targetList.First(),
                (current, member) => current + ", " + member);
        }
        
        public override void Close()
        {
            Destroy(gameObject);
        }
    }
}