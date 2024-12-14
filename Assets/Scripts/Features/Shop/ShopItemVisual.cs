using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Core;

namespace Game
{
    public class ShopItemVisual: BaseVisual<Shop>
    {
        [SerializeField] Image itemImage;
        [SerializeField] Button buyButton;
        [SerializeField] TextMeshProUGUI priceText;

        private TPlant seedType;

        protected virtual void Start()
        {
            buyButton.onClick.AddListener(PressedBuy);
        }

        private void PressedBuy()
        {
            Debug.LogError("Pressed Buy");
            Feature.PressedBuyShopItem(this);
        }

        public TPlant GetSeedType()
        {
            return seedType;
        }
        
        protected void SetSeedType(TPlant seedType)
        {
            this.seedType = seedType;
        }
    }
}