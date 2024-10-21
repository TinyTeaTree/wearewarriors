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
            .AddNext(() => bootstrap.Features.Get<IGarden>().Load())
            .AddNext(() =>{ bootstrap.Features.Get<ILoadingScreen>().Close(); })
            ;
            
        }
    }
}