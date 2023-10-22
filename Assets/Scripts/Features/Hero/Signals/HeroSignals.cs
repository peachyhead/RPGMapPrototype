// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Hero.Data;

namespace Features.Hero.Signals
{
    public sealed class HeroSignals
    {
        public class SelectHero
        {
            public HeroType Type { get; }

            public SelectHero(HeroType type)
            {
                Type = type;
            }
        }
    }
}