using System.Threading.Tasks;
using Core;
using Services;

namespace Game
{
    public class Shop : BaseVisualFeature<ShopVisual>, IShop
    {
        [Inject] public ShopRecord Record { get; set; }
        [Inject] public ILocalConfigService ConfigService { get; set; }
        [Inject] public IHud Hud { get; set; }
        public ShopVisual Visual { get; set; }
        
        private ShopConfig config;
        public ShopConfig Config => config;

        public async Task LoadShop()
        {
            config = ConfigService.GetConfig<ShopConfig>();
            await CreateVisual(Hud.Canvas);

            Visual = _visual;

        }

        public Task BuyProduct(string productName)
        {
            throw new System.NotImplementedException();
        }

        public Task SellProduct(string productName)
        {
            throw new System.NotImplementedException();
        }

      
    }
}