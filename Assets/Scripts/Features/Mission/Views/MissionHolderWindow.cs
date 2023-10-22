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
        
        private List<BaseMissionWindow> _missionWindows = new ();
        
        [Inject]
        public void Construct(List<BaseMissionWindow> missionWindows)
        {
            _missionWindows = missionWindows;
        }

        public override void Show()
        {
            base.Show();
            RectTransform.sizeDelta = new Vector2(0, Screen.height);
            
            foreach (var missionWindow in _missionWindows)
            {
                missionWindow.Show();
                missionWindow.transform.SetParent(_layoutHolder);
                missionWindow.transform.localScale = Vector3.one;
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