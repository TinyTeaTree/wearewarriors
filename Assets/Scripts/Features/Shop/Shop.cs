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
        [Inject] public IAvatar Avatar { get; set; }
        
        private ShopConfig config;
        public ShopConfig Config => config;

        public void PressedBuyShopItem(ShopItemVisual item)
        {
            _visual.DisplayShop(false);
            _visual.ClearShopItems();

            if (item is SeedItemVisual seedItem)
            {
                Tools.AddGrainBag(seedItem.GetSeedType());
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
            if (!_visual)
                return;
            
            _visual.LoadItems(shopType);
        }

        public bool IsShopOpen()
        {
            return _visual.OnDisplay();
        }

        public bool WasAlreadyOpen()
        {
            if (!_visual)
                return false;
            
            return _visual.WasOpen;
        }

        public void MarkShop(bool status)
        {
            if (!_visual)
                return;
            
            _visual.MarkShopOpen(status);
        }

        public void CheckLocation(ShopEnterDetector shopEnterDetector)
        {
            if (!_visual) //Was not loaded yet
                return;
            
            if (Avatar.AvatarTransform == null)
                return; //No Avatar Yet
            
            var avatarPosition = Avatar.AvatarTransform.position;
            
            if(Physics.SphereCast(avatarPosition + Vector3.up * 20f, 1f, Vector3.down * 50f, out var hit, 100f, LayerMask.GetMask("Store")))
            {
                if (shopEnterDetector.ShopType == TShops.SeedShop)
                {
                    if (hit.transform == shopEnterDetector.transform)
                    {
                        if (!WasAlreadyOpen())
                        {
                            MarkShop(true);
                            LoadItems(TShops.SeedShop);
                        }
                    }
                }
                else if (shopEnterDetector.ShopType == TShops.SellCropShop)
                {
                    
                }
            }
            else
            {
                MarkShop(false);
            }
        }
    }
}