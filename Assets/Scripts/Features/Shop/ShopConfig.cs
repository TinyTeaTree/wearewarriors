using System.Collections.Generic;
using Core;

namespace Game
{
    [System.Serializable]
    public class ShopConfig : BaseConfig
    {
        public ShopConfigData[] ShopConfigData;

        public List<CropPrice> Prices;
    }
}