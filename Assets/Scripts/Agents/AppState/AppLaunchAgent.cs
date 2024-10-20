using System.Collections.Generic;
using System.Threading.Tasks;
using Core;

namespace Agents
{
    public class AppLaunchAgent : BaseAgent<IAppLaunchAgent>, IAppLaunchAgent
    {
        public Task AppLaunch()
        {
            List<Task> tasks = new();

            foreach (var receiver in _features)
            {
                tasks.Add(receiver.AppLaunch());
            }

            foreach (var receiver in _services)
            {
                tasks.Add(receiver.AppLaunch());
            }

            return Task.WhenAll(tasks);
        }
    }
}