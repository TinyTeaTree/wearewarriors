using Core;
using UnityEngine;

namespace Game
{
    public class CameraVisual : BaseVisual<Camera>
    {
        [SerializeField] private UnityEngine.Camera _camera;

        public void SetSpot(Transform gardenCameraStartSpot)
        {
            transform.position = gardenCameraStartSpot.position;
            transform.rotation = gardenCameraStartSpot.rotation;
        }
    }
}