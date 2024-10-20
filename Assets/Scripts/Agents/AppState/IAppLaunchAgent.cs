using System.Threading.Tasks;
using Core;

namespace Agents
{
    public interface IAppLaunchAgent : IAgent
    {
        Task AppLaunch();
    }
}