// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Mission.Base;

namespace Features.Mission.Models.States
{
    public class ActiveMissionState : BaseMissionState
    {
        public override void Initialize()
        {
            Context.SetAvailability(true);
            Context.SetShown(true);
        }
    }
}