using System.Threading.Tasks;
using Core;

namespace Game
{
    public interface IShop : IFeature
    {
        Task LoadShop();
        Task BuyProduct(string productName);
        Task SellProduct(string productName);
        ShopVisual Visual { get; set; }
    }
}