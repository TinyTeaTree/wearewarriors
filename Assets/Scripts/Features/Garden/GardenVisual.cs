using Core;
using UnityEngine;

namespace Game
{
    public class GardenVisual : BaseVisual<Garden>
    {
        [SerializeField] private Transform _avatarStartSpot;
        [SerializeField] private Transform _cameraStartSpot;
        [SerializeField] private GardenPlotVisual _gardenplot;

        public Transform AvatarStartSpot => _avatarStartSpot;
        public Transform CameraStartSpot => _cameraStartSpot;

    }
}