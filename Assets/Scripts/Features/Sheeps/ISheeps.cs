using System.Threading.Tasks;
using Core;

namespace Game
{
    public interface ISheeps : IFeature
    {
        Task LoadSheep();
    }
}