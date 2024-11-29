using System;

namespace Game
{
    [Serializable]
    public class PlotData
    {
        public int Id;
        public TPlant Plant;
        public TPlotState State;
        
        public float Progress;
        public float TimeLeft;
    }
}