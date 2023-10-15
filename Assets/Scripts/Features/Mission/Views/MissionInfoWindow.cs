// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Mission.Data;
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
        [SerializeField] private TMP_Text _nameField;
        [SerializeField] private Image _cover;
        [SerializeField] private TMP_Text _descriptionField;
        [SerializeField] private Button _startButton;

        private MissionData _data;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(MissionData data, SignalBus signalBus)
        {
            _data = data;
            _signalBus = signalBus;
        }

        public override void Show()
        {
            _nameField.text = $"{_data.Name}";
            _cover.sprite = _data.Cover;
            _descriptionField.text = $"{_data.Description}";
            
            _startButton
                .OnClickAsObservable()
                .Subscribe(_ => _signalBus.TryFire(new MissionSignals.StartMission(_data.Name)))
                .AddTo(this);
        }

        public override void Close()
        {
            Destroy(gameObject);
        }
    }
}