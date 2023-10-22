// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System.Collections.Generic;
using UnityEngine;

namespace Features.UI.Views
{
    public class CanvasController : MonoBehaviour
    {
        [SerializeField] private List<CanvasLayerHolder> _layers;

        public List<CanvasLayerHolder> Layers => _layers;
    }
}