using Core;
using UnityEngine;

namespace Game
{
    public class CameraVisual : BaseVisual<Camera>
    {
        [SerializeField] private UnityEngine.Camera _camera;

        private Transform _target;
        
        public void SetSpot(Transform gardenCameraStartSpot)
        {
            transform.position = gardenCameraStartSpot.position;
            transform.rotation = gardenCameraStartSpot.rotation;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void FollowTarget(Transform target)
        {
            
        }
    }
}