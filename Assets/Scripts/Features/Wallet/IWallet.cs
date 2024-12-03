using System.Threading.Tasks;
using Core;

namespace Game
{
    public interface IWallet : IFeature
    {
        Task LoadWallet();
        Task AddToWallet(int amount);
        Task Pay(int amount);
    }
}