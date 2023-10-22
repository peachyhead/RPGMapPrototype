// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using Features.Mission.Models;

namespace Features.Mission.Base
{
    public abstract class BaseMissionState : IDisposable
    {
        protected MissionModel Context;

        public void SetContext(MissionModel context)
        {
            Context = context;
        }
        
        public abstract void Initialize();

        public void Dispose()
        {
            
        }
    }
}