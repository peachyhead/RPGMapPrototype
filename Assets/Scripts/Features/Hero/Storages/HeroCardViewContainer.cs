// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using System.Collections.Generic;
using Features.Hero.Factories;
using Features.Hero.Models;
using Features.Hero.Views;
using UniRx;
using Zenject;

namespace Features.Hero.Storages
{
    public class HeroCardViewContainer : IInitializable, IDisposable
    {
        private readonly HeroCardHolder _cardHolder;
        
        private readonly HeroModelStorage _heroModelStorage;
        private readonly HeroCardViewFactory _cardViewFactory;

        private readonly Dictionary<HeroModel, HeroCardView> _heroMap = new ();
        
        private readonly CompositeDisposable _compositeDisposable = new ();

        public HeroCardViewContainer(HeroModelStorage heroModelStorage,
            HeroCardViewFactory cardViewFactory, HeroCardHolder cardHolder)
        {
            _heroModelStorage = heroModelStorage;
            _cardViewFactory = cardViewFactory;
            _cardHolder = cardHolder;
        }

        public void Initialize()
        {
            foreach (var heroModel in _heroModelStorage.GetItems())
                CreateView(heroModel);
            
            _heroModelStorage
                .OnItemAdded()
                .Subscribe(CreateView)
                .AddTo(_compositeDisposable);

            _heroModelStorage
                .OnItemRemoved()
                .Subscribe(RemoveView)
                .AddTo(_compositeDisposable);
        }

        private void CreateView(HeroModel model)
        {
            var view = _cardViewFactory.Create(model);
            _cardHolder.AddCard(view);
            _heroMap.Add(model, view);
        }

        private void RemoveView(HeroModel model)
        {
            if (!_heroMap.TryGetValue(model, out var view)) 
                return;
            
            _heroMap.Remove(model);
            view.Dispose();
        }
        
        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}