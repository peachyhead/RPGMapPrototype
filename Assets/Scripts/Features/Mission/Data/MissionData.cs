// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using System.Collections.Generic;
using System.Linq;
using Features.Hero.Data;
using Features.Map.Data;
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
        [SerializeField] private List<string> _allies;
        [SerializeField] private List<string> _opponents;
        [SerializeField] private List<HeroType> _unlocksHeroes;
        [SerializeField] private List<MissionUnlockData> _requiredMissions;
        [SerializeField] private List<HeroRewardData> _rewardData;

        public string Name => _name;
        public Sprite Cover => _cover;
        public string Description => _description;
        public string MissionStory => _missionStory;
        public List<string> Allies => _allies.ToList();
        public List<string> Opponents => _opponents.ToList();
        public List<HeroType> UnlocksHeroes => _unlocksHeroes.ToList();
        public List<MissionUnlockData> RequiredMissions => _requiredMissions;
        public List<HeroRewardData> RewardData => _rewardData;
    }
}