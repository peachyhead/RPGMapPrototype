// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using TMPro;
using UnityEngine;
using Zenject;

namespace Features.Map.Views
{
    public class MapNodeView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _idField;

        private string _nodeID;

        [Inject]
        public void Construct(string nodeID)
        {
            _nodeID = nodeID;;
        }

        private void Start()
        {
            _idField.text = _nodeID;
        }
    }
}