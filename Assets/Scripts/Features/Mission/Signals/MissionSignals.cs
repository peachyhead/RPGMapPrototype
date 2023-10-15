// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Map.Data;

namespace Features.Mission.Signals
{
    public sealed class MissionSignals
    {
        public class StartMission
        {
            public string Name { get; }

            public StartMission(string name)
            {
                Name = name;
            }
        }
        
        public class CompleteMission
        {
            public string Name { get; }

            public CompleteMission(string name)
            {
                Name = name;
            }
        }
    }
}