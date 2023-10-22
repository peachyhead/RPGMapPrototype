// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Features.Hero.Data.Config
{
    [CreateAssetMenu(fileName = "HeroRegistry", menuName = "Registry/Hero registry")]
    public class HeroRegistry : ScriptableObject
    {
        [SerializeField] private List<HeroData> _heroes;

        public bool TryGetDataByType(HeroType type, out HeroData data)
        {
            data = _heroes.FirstOrDefault(hero => hero.Type == type);
            return data != default;
        }
    }
}