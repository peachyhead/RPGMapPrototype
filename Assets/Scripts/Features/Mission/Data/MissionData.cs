// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using System.Collections.Generic;
using Features.Hero.Data;
using UnityEngine;

namespace Features.Mission.Data
{
    [Serializable]
    public class MissionData
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _cover;
        [SerializeField] private string _description;
        [SerializeField] private string _missionStory;
        [SerializeField] private HeroType _unlocksHero;
        [SerializeField] private List<string> _requiredMissions;

        public string Name => _name;
        public Sprite Cover => _cover;
        public string Description => _description;
        public string MissionStory => _missionStory;
        public HeroType UnlocksHero => _unlocksHero;
        public List<string> RequiredMissions => _requiredMissions;
    }
}