using System.Threading.Tasks;
using Core;

namespace Game
{
    public interface IAnimals : IFeature
    {
        Task LoadSheep();
    }
}