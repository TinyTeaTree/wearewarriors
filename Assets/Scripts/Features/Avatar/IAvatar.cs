using System.Threading.Tasks;
using Core;

namespace Game
{
    public interface IAvatar : IFeature
    {
        Task Load();

        void Activate();
    }
}