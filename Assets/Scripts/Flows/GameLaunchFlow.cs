using Agents;
using Core;
using System;
using System.Threading.Tasks;

namespace Game
{
    public class GameLaunchFlow : SequenceFlow
    {
        public GameLaunchFlow(IBootstrap bootstrap)
        {
            var joystick = bootstrap.Features.Get<IJoystick>();
            var avatar = bootstrap.Features.Get<IAvatar>();
            
             AddNext(action: () => bootstrap.Agents.Get<IAppLaunchAgent>().AppLaunch())
            .AddNext(() => { bootstrap.Features.Get<ILoadingScreen>().Show(true); })
            .AddNext(() => bootstrap.Features.Get<IGarden>().Load())

            .AddNext(() => { return Task.Delay(TimeSpan.FromSeconds(0.5f)); })
            .AddNext(() => { bootstrap.Features.Get<ILoadingScreen>().ProgressControl(0.2f); })
            .AddNext(() => { return Task.Delay(TimeSpan.FromSeconds(0.5f)); })
            .AddNext(() => { bootstrap.Features.Get<ILoadingScreen>().ProgressControl(0.4f); })
            .AddNext(() => { return Task.Delay(TimeSpan.FromSeconds(0.5f)); })
            .AddNext(() => { bootstrap.Features.Get<ILoadingScreen>().ProgressControl(0.6f); })
            .AddNext(() => { return Task.Delay(TimeSpan.FromSeconds(0.5f)); })
            .AddNext(() => { bootstrap.Features.Get<ILoadingScreen>().ProgressControl(0.8f); })
            .AddNext(() => { return Task.Delay(TimeSpan.FromSeconds(0.5f)); })
            .AddNext(() => { bootstrap.Features.Get<ILoadingScreen>().ProgressControl(1f); })

            .AddNext(() => avatar.Load())
            .AddNext(() => joystick.Load())
            .AddNext(() => joystick.Show())
            .AddNext(() => avatar.Activate())

            .AddNext(() => { bootstrap.Features.Get<ILoadingScreen>().Close(); })

            ;
            
        }
    }
}