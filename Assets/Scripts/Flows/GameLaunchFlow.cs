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
            var playerAccount = bootstrap.Features.Get<IPlayerAccount>();
            var tools = bootstrap.Features.Get<ITools>();
            var camera = bootstrap.Features.Get<ICamera>();
            var loading = bootstrap.Features.Get<ILoadingScreen>();
            
             AddNext(action: () => bootstrap.Agents.Get<IAppLaunchAgent>().AppLaunch())
            .AddNext(() => { loading.Show(true); })
            .AddNext(() => bootstrap.Features.Get<IGarden>().Load())
            .AddNext(() => { return Task.Delay(TimeSpan.FromSeconds(0.25f)); })
            .AddNext(() => { loading.ProgressControl(0.2f); })
            .AddNext(() => playerAccount.Login())
            .AddNext(() => avatar.Load())
            .AddNext(() => camera.Load())
            .AddNext(() => { loading.ProgressControl(0.4f); })
            .AddNext(() => { return Task.Delay(TimeSpan.FromSeconds(0.25f)); })
            .AddNext(() => tools.LoadTools())
            .AddNext(() => { loading.ProgressControl(0.6f); })
            .AddNext(() => { return Task.Delay(TimeSpan.FromSeconds(0.2f)); })
            .AddNext(() => { loading.ProgressControl(0.8f); })
            .AddNext(() => { return Task.Delay(TimeSpan.FromSeconds(0.1f)); })
            .AddNext(() => { loading.ProgressControl(1f); })

            .AddNext(() => joystick.Load())
            .AddNext(() => joystick.Show())
            .AddNext(() => avatar.Activate())

            .AddNext(() => { bootstrap.Features.Get<ILoadingScreen>().Close(); })
            
            .AddNext(() => camera.ActivateAnimation())

            ;
            
        }
    }
}