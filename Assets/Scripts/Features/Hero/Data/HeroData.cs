// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using UnityEngine;

namespace Features.Hero.Data
{
    [Serializable]
    public class HeroData
    {
        [SerializeField] private HeroType _type;
        [SerializeField] private string _id;
        [SerializeField] private Sprite _cover;
        [SerializeField] private int _defaultReward;
        
        public HeroType Type => _type;
        public string ID => _id;
        public Sprite Cover => _cover;
        public int DefaultReward => _defaultReward;
    }
}