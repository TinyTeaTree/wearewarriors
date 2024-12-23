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
        [SerializeField] int price;

        public int Price => price;
        protected virtual void Start()
        {
            buyButton.onClick.AddListener(PressedBuy);
        }

        private void PressedBuy()
        {
            Feature.PressedBuyShopItem(this);
        }


        

    }
}