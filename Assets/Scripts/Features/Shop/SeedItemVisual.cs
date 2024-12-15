using UnityEngine;

namespace Game
{
    public class SeedItemVisual : ShopItemVisual
    { 
        [SerializeField] private TPlant seedType;

        protected override void Start()
        {
            base.Start();
            base.SetSeedType(seedType);
        }
    }
}