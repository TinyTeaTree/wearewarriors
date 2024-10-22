using Agents;
using Core;

namespace Game
{
    public class GameLaunchFlow : SequenceFlow
    {
        public GameLaunchFlow(IBootstrap bootstrap)
        {
            var joystick = bootstrap.Features.Get<IJoystick>();
            var avatar = bootstrap.Features.Get<IAvatar>();
            
             AddNext(action: () => bootstrap.Agents.Get<IAppLaunchAgent>().AppLaunch())
            .AddNext(() => { bootstrap.Features.Get<ILoadingScreen>().Show(); })
            .AddNext(() => bootstrap.Features.Get<IGarden>().Load())
            .AddNext(() => avatar.Load())
            .AddNext(() => joystick.Load())
            .AddNext(() => joystick.Show())
            .AddNext(() => avatar.Activate())
            .AddNext(() => { bootstrap.Features.Get<ILoadingScreen>().Close(); })
            ;
            
        }
    }
}