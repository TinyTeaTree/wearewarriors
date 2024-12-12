using System.Threading.Tasks;
using Core;

namespace Game
{
    public interface IShop : IFeature
    {
        Task LoadShop();
        Task BuyProduct(string productName);
        Task SellProduct(string productName);
        void LoadItems(TShops shopType);
        void WasOpen(bool status);
        bool IsShopOpen();
        bool WasAlreadyOpen();
        
    }
}