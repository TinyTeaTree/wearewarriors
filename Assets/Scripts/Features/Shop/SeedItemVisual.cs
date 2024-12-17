using UnityEngine;

namespace Game
{
    public class SeedItemVisual : ShopItemVisual
    { 
        [SerializeField] private TPlant seedType;

        protected override void Start()
        {
            base.Start();
            SetSeedType(seedType);
        }
        
        protected void SetSeedType(TPlant seedType)
        {
            this.seedType = seedType;
        }
        
        public TPlant GetSeedType()
        {
            return seedType;
        }
    }
}