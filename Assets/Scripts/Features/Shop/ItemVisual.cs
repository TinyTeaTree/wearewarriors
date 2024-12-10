using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Core;

namespace Game
{
    public class ItemVisual: BaseVisual<Shop>
    {
        [SerializeField] Image itemImage;
        [SerializeField] Button buyButton;
        [SerializeField] TextMeshProUGUI priceText;

        private TPlant seedType;
        public Button GetItemButton()
        {
            return buyButton;
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