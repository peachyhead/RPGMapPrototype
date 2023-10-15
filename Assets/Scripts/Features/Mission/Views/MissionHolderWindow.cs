// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Features.Mission.Views
{
    public class MissionHolderWindow : BaseMissionWindow
    {
        [SerializeField] private Transform _layoutHolder; 
        private List<BaseMissionWindow> _missionWindows;
        
        [Inject]
        public void Construct(List<BaseMissionWindow> missionWindows)
        {
            _missionWindows = missionWindows;
        }

        public override void Show()
        {
            foreach (var missionWindow in _missionWindows)
            {
                missionWindow.Show();
                missionWindow.transform.SetParent(_layoutHolder);
            }
        }

        public override void Close()
        {
            foreach (var missionWindow in _missionWindows)
            {
                missionWindow.Close();
            }
            Destroy(gameObject);
        }
    }
}