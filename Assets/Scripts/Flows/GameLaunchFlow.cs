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
            this.AddNext(action: () => bootstrap.Agents.Get<IAppLaunchAgent>().AppLaunch())
            .AddNext(() => { Debug.Log("Started"); });
        }
    }
}