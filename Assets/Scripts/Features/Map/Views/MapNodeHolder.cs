// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Map.Signals;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.Map.Views
{
    public class MapNodeHolder : MonoBehaviour
    {
        public Transform LayoutRoot => _layoutRoot;
        
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Transform _layoutRoot;
        [SerializeField] private RectTransform _rectTransform;

        private SignalBus _signalBus;

        private int _node;
        private Vector2 _position;

        [Inject]
        public void Construct(int node, Vector2 position, SignalBus signalBus)
        {
            _node = node;
            _position = position;
            _signalBus = signalBus;
        }

        private void Start()
        {
            _rectTransform.anchoredPosition = _position;
            
            _toggle
                .OnValueChangedAsObservable()
                .Select(status => status)
                .Where(status => status)
                .Subscribe(_ => _signalBus.TryFire(new MapSignals.SelectNode(_node)))
                .AddTo(this);
        }
        
        public void SetToggleGroup(ToggleGroup group)
        {
            _toggle.group = group;
        }
    }
}