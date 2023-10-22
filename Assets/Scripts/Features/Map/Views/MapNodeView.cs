// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Map.Data;
using Features.Map.Models;
using Features.Map.Signals;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.Map.Views
{
    public class MapNodeView : MonoBehaviour
    {
        public MapNodeID ID => _nodeModel.NodeID;

        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private TMP_Text _idField;
        [SerializeField] private Toggle _toggle;

        private MapNodeModel _nodeModel;

        private SignalBus _signalBus;
        
        [Inject]
        public void Construct(MapNodeModel nodeModel, SignalBus signalBus)
        {
            _nodeModel = nodeModel;
            _signalBus = signalBus;
        }

        private void Start()
        {
            _idField.text = _nodeModel.NodeID.ToString();

            _toggle
                .OnValueChangedAsObservable()
                .Select(status => status)
                .Where(status => status)
                .Subscribe(_ => _signalBus.TryFire(new MapSignals.SelectNode(_nodeModel.NodeID.Node)))
                .AddTo(this);

            _nodeModel
                .OnShowStatusChange()
                .Subscribe(status =>
                {
                    if (status)
                        Show();
                    else
                        Hide();
                })
                .AddTo(this);

            _nodeModel
                .OnLockStatusChange()
                .Subscribe(status =>
                {
                    if (status)
                        Unlock();
                    else
                        Lock();
                })
                .AddTo(this);
        }
        
        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
        
        private void Unlock()
        {
            _toggle.interactable = true;
        }

        private void Lock()
        {
            _toggle.interactable = false;
        }
        
        public void SetToggleGroup(ToggleGroup group)
        {
            _toggle.group = group;
        }

        public void SetTargetGraphic(Image graphic)
        {
            _toggle.graphic = graphic;
        }

        public RectTransform GetRectTransform() => _rectTransform;
    }
}