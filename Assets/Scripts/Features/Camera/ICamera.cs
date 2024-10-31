using System.Threading.Tasks;
using Core;

namespace Game
{
    public interface ICamera : IFeature
    {
        Task Load();
    }
}