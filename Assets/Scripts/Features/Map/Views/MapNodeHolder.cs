// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.Map.Views
{
    public class MapNodeHolder : MonoBehaviour
    {
        [SerializeField] private RectTransform _layoutRoot;
        [SerializeField] private HorizontalLayoutGroup _layoutGroup;
        [SerializeField] private RectTransform _rectTransform;

        private Vector2 _position;

        [Inject]
        public void Construct(Vector2 position)
        {
            _position = position;
        }

        private void Start()
        {
            _rectTransform.anchoredPosition = _position;
        }

        public void AddNode(MapNodeView nodeView)
        {
            nodeView.transform.SetParent(_layoutRoot);
            var padding = new Vector2(_layoutGroup.padding.right, 0);
            _rectTransform.sizeDelta += nodeView.GetRectTransform().sizeDelta + padding;
        }
    }
}