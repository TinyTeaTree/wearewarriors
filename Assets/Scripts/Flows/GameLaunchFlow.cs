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
             AddNext(action: () => bootstrap.Agents.Get<IAppLaunchAgent>().AppLaunch())
            .AddNext(() => { bootstrap.Features.Get<ILoadingScreen>().Show(true); })
            .AddNext(() => bootstrap.Features.Get<IGarden>().Load())
            .AddNext(() => { return Task.Delay(TimeSpan.FromSeconds(2f)); })
            .AddNext(() => { bootstrap.Features.Get<ILoadingScreen>().ProgressControl(0.2f); })
            .AddNext(() => { return Task.Delay(TimeSpan.FromSeconds(2f)); })
            .AddNext(() => { bootstrap.Features.Get<ILoadingScreen>().ProgressControl(0.4f); })
            .AddNext(() => { return Task.Delay(TimeSpan.FromSeconds(2f)); })
            .AddNext(() => { bootstrap.Features.Get<ILoadingScreen>().ProgressControl(0.6f); })
            .AddNext(() => { return Task.Delay(TimeSpan.FromSeconds(2f)); })
            .AddNext(() => { bootstrap.Features.Get<ILoadingScreen>().ProgressControl(0.8f); })
            .AddNext(() => { return Task.Delay(TimeSpan.FromSeconds(2f)); })
            .AddNext(() => { bootstrap.Features.Get<ILoadingScreen>().ProgressControl(1f); })
            .AddNext(() =>{ bootstrap.Features.Get<ILoadingScreen>().Close(); })
            ;
            
        }
    }
}