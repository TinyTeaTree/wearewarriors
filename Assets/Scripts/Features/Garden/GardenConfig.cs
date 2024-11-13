using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class GardenConfig : BaseConfig
    {
        public GardenPlotVisual plotPrefab;
        public GardenPlotData[] GardenPlots;
    }
}