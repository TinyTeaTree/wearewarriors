using System.Linq;
using System.Threading.Tasks;
using Core;
using Services;
using UnityEngine;

namespace Game
{
    public class Shop : BaseVisualFeature<ShopVisual>, IShop
    {
        [Inject] public ShopRecord Record { get; set; }
        [Inject] public ILocalConfigService ConfigService { get; set; }
        [Inject] public IHud Hud { get; set; }
        [Inject] public ITools Tools { get; set; }
        [Inject] public IWallet Wallet { get; set; }
        
        private ShopConfig config;
        public ShopConfig Config => config;

        private void SetButtonListener()
        {
            foreach (var item in Record.ItemVisuals)
            {
               var buyButton = item.GetItemButton();
               
               buyButton.onClick.AddListener(() =>
               {
                   if (Wallet.CheckWalletBalance() >= item.GetItemPrice())
                   {
                       Wallet.Pay(item.GetItemPrice());
                       _visual.DisplayShop(false);
                       _visual.ClearShopItems();
                       Tools.SpawnShopTool(item);
                   }
               });
            }
        }
        
        public async Task LoadShop()
        {
            config = ConfigService.GetConfig<ShopConfig>();
            await CreateVisual(Hud.Canvas);
        }

        public Task BuyProduct(string productName)
        {
            throw new System.NotImplementedException();
        }

        public Task SellProduct(string productName)
        {
            throw new System.NotImplementedException();
        }

        public void LoadItems(TShops shopType)
        {
            _visual.LoadItems(shopType);
            SetButtonListener();
        }

        public bool IsShopOpen()
        {
            return _visual.OnDisplay();
        }

        public bool WasAlreadyOpen()
        {
            return _visual.WasOpen;
        }

        public void WasOpen(bool status)
        {
            _visual.SetTriggerStatus(status);
        }
    }
}