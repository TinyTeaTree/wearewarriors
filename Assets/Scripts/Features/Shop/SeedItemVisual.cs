using UnityEngine;

namespace Game
{
    public class SeedItemVisual : ItemVisual
    { 
        [SerializeField] private TPlant seedType;

        private void Start()
        {
            base.SetSeedType(seedType);
        }
    }
}