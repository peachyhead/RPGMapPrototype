// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System.Collections.Generic;
using System.Linq;
using Features.Hero.Data;
using Features.Hero.Data.Config;
using Features.Hero.Factories;
using Features.Hero.Models;
using Features.Hero.Storages;
using Features.Hero.Views;

namespace Features.Hero.Services
{
    public class HeroService
    {
        private HeroModel _selectedHero;

        private readonly HeroModelStorage _heroModelStorage;
        private readonly HeroModelFactory _heroModelFactory;
        private readonly HeroRegistry _heroRegistry;

        public HeroService(HeroModelStorage heroModelStorage, 
            HeroModelFactory heroModelFactory, 
            HeroRegistry heroRegistry)
        {
            _heroModelStorage = heroModelStorage;
            _heroModelFactory = heroModelFactory;
            _heroRegistry = heroRegistry;
        }
        
        public void Attend(string id)
        {
            _selectedHero?.AttendMission(id);
        }

        public void Select(HeroType type)
        {
            if (_heroModelStorage.TryGetItemByType(type, out var hero))
                _selectedHero = hero;
        }
        
        public void Unlock(HeroType type)
        {
            if (!_heroRegistry.TryGetDataByType(type, out var data)) 
                return;
            if (_heroModelStorage.GetItems().Any(item => item.Data.Type == type)) 
                return;
            
            var model = _heroModelFactory.Create(data);
            _heroModelStorage.AddItem(model);
        }
        
        public void Reward(List<HeroRewardData> rewards)
        {
            _selectedHero?.Reward();
            
            foreach (var rewardData in rewards)
            {
                if (_heroModelStorage.TryGetItemByType(rewardData.Type, out var hero))
                    hero.Reward(rewardData.RewardAmount);
            }
        }
    }
}