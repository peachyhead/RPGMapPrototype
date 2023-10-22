// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using UnityEngine;

namespace Features.Mission.Views
{
    public abstract class BaseMissionWindow : MonoBehaviour
    {
        [SerializeField] protected RectTransform RectTransform;

        public virtual void Show()
        {
            RectTransform.anchoredPosition = Vector2.zero;
            RectTransform.localScale = Vector3.one;
        }
        public abstract void Close();
    }
}