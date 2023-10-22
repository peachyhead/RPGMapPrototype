// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using System.Collections.Generic;
using System.Linq;
using Features.Hero.Data;
using Features.Hero.Models;
using UniRx;

namespace Features.Hero.Storages
{
    public class HeroModelStorage
    {
        private readonly List<HeroModel> _items = new ();

        private readonly Subject<HeroModel> _addSubject = new ();
        private readonly Subject<HeroModel> _removeSubject = new ();

        public void AddItem(HeroModel model)
        {
            _items.Add(model);
            _addSubject.OnNext(model);
        }

        public void RemoveItem(HeroModel model)
        {
            _items.Remove(model);
            _removeSubject.OnNext(model);
        }

        public bool TryGetItemByType(HeroType type, out HeroModel model)
        {
            model = _items.FirstOrDefault(item => item.Data.Type == type);
            return model != default;
        }

        public List<HeroModel> GetItems() => _items.ToList();
        
        public IObservable<HeroModel> OnItemAdded()
        {
            return _addSubject.AsObservable();
        }
        
        public IObservable<HeroModel> OnItemRemoved()
        {
            return _removeSubject.AsObservable();
        }
    }
}