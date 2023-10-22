// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using UnityEngine;
using UnityEngine.UI;

namespace Features.Map.Views
{
    public class MapView : MonoBehaviour
    {
        [SerializeField] private ToggleGroup _toggleGroup;

        public ToggleGroup ToggleGroup => _toggleGroup;
    }
}