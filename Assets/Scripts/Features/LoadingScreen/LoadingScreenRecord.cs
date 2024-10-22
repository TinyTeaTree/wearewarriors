using Core;
using UnityEngine;

namespace Game
{
    public class LoadingScreenRecord : BaseRecord
    {
       public LoadingScreenVisual loadingScreenVisual;

        public bool IsShowing { get; set; }
        public float LoadingPercentage { get; set; }
        public float TipDisplayDuration { get; set; }

    }
}