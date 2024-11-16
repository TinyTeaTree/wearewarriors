using Core;
using UnityEngine;

namespace Game
{
    public class GardenVisual : BaseVisual<Garden>
    {
        [SerializeField] private Transform _avatarStartSpot;
        [SerializeField] private Transform _cameraStartSpot;
        [SerializeField] private PlotFieldVisual[] _plotFieldVisual;
        
        public PlotFieldVisual[] PlotFieldVisual => _plotFieldVisual;
        public Transform AvatarStartSpot => _avatarStartSpot;
        public Transform CameraStartSpot => _cameraStartSpot;

        public void LoadPlotFieldVisuals()
        {
            foreach (var fieldVisual in _plotFieldVisual)
            {
                fieldVisual.SetFeature(Feature);
                fieldVisual.LoadPlotField();
            }
        }

    }
}