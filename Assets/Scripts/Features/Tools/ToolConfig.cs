using Core;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class ToolConfig : BaseSO
    {
        public ToolVisual prefab;
        public TTools ToolID;
        public TPlant GrainBagSeedType;
    }
}