// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.UI.Data;
using UnityEngine;

namespace Features.UI.Views
{
    public class CanvasLayerHolder : MonoBehaviour
    {
        [SerializeField] private HolderType _type;

        public HolderType Type => _type;
    }
}