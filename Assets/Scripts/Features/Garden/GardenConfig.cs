using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    [System.Serializable]
    public class GardenConfig : BaseConfig
    {
        public GardenPlotVisual plotPrefab;
        public PlotFieldData[] plotFields;
        public PlantVisual[] plantVisuals;
    }
}