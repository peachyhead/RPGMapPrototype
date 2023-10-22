// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using UnityEngine;

namespace Features.Hero.Data
{
    [Serializable]
    public class HeroRewardData
    {
        [SerializeField] private HeroType _type;
        [SerializeField] private int _rewardAmount;

        public HeroType Type => _type;
        public int RewardAmount => _rewardAmount;
    }
}