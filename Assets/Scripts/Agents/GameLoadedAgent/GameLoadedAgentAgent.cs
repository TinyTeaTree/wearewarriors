using Core;

namespace Agents
{
    public class GameLoadedAgentAgent : BaseAgent<IGameLoadedAgentAgent>, IGameLoadedAgentAgent
    {
        public void OnGameLoaded()
        {
            foreach (var receiver in _features)
            {
                receiver.OnGameLoaded();
            }

            foreach (var receiver in _services)
            {
                receiver.OnGameLoaded();
            }
        }
    }
}