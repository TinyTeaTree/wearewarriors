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
        public FieldData[] plotFields;
        public PlantVisual[] plantVisuals;
    }
}