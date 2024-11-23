using Core;
using UnityEngine;

namespace Game
{
    public class GardenSeedPoolVisual : BaseVisual<Garden>
    {
        [SerializeField] private TPlant _seedPoolType;

        public TPlant SeedPoolType => _seedPoolType;
        
        public TPlant FillGrainBag()
        {
            return _seedPoolType;   
        }   
    }
}