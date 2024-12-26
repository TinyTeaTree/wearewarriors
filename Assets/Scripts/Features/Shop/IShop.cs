using System.Threading.Tasks;
using Core;

namespace Game
{
    public interface IShop : IFeature
    {
        Task LoadShop();
        void LoadItems(TShops shopType);
        void MarkShop(bool status);
        bool IsShopOpen();
        bool WasAlreadyOpen();
        
    }
}