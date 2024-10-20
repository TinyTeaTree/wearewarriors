using System;
using UnityEngine;
using System.Threading.Tasks;
using Agents;
using Core;

namespace Game
{
    public class GameLaunchFlow : SequenceFlow
    {
        public GameLaunchFlow(IBootstrap bootstrap)
        {
             AddNext(action: () => bootstrap.Agents.Get<IAppLaunchAgent>().AppLaunch())
            .AddNext(() => { bootstrap.Features.Get<ILoadingScreen>().Show(); })
            .AddNext(() => { Debug.Log("Started"); })
            .AddNext(() => { return Task.Delay(TimeSpan.FromSeconds(2f)); })
            .AddNext(() => { bootstrap.Features.Get<IField>().Show(); })
            ;
            
        }
    }
}