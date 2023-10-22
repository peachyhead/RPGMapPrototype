// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using System.Collections.Generic;
using System.Linq;
using Features.Hero.Data;
using UniRx;

namespace Features.Hero.Models
{
    public class HeroModel
    {
        public HeroData Data { get; }
        
        private readonly List<string> _completedMissionsID = new ();
        private readonly ReactiveProperty<int> _pointsProperty = new ();

        private HeroModel(HeroData data)
        {
            Data = data;
        }

        public void AttendMission(string id)
        {
            _completedMissionsID.Add(id);
        }
        
        public void Reward(int pointsAmount = default)
        {
            _pointsProperty.Value += pointsAmount == default 
                ? Data.DefaultReward
                : pointsAmount;
        }

        public bool IsCompletedMission(string id)
        {
            return _completedMissionsID.Any(item => item == id);
        }
        
        public IObservable<int> OnPointsChange()
        {
            return _pointsProperty.AsObservable();
        }
    }
}