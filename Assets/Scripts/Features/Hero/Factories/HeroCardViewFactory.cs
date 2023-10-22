// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Hero.Data;
using Features.Hero.Models;
using Features.Hero.Views;
using Zenject;

namespace Features.Hero.Factories
{
    public class HeroCardViewFactory : IFactory<HeroModel, HeroCardView>
    {
        private readonly DiContainer _container;

        public HeroCardViewFactory(DiContainer container)
        {
            _container = container;
        }

        public HeroCardView Create(HeroModel model)
        {
            return _container.InstantiatePrefabResourceForComponent<HeroCardView>(
                HeroConsts.HeroCardPrefab, new object[] { model });
        }
    }
}