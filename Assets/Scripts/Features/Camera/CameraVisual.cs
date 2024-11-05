using System;
using Core;
using UnityEngine;

namespace Game
{
    public class CameraVisual : BaseVisual<Camera>
    {
        [Header("Camera Components")]
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private Animator _animator;

        [Header("Camera Settings")]
        [SerializeField] private Vector3 _offsetVector = new(0, 15f, -15f);
        
        [SerializeField] private float _angle = 0;
        [SerializeField] private float _radius = 1;
        
        [SerializeField] bool _isFollowing = true;
        [SerializeField] bool _isCircular = false;
        
        private Transform _target;
        private Vector3 _velocity = Vector3.zero;
        private void LateUpdate()
        {
            if (_isFollowing)
            {
                FollowTarget(_target);
                LookAtTarget(_target);
            }
            
            if (_isCircular)
            {
                CircularMotion();
                LookAtTarget(_target);
            }
        }
        
        public void SetSpot(Transform gardenCameraStartSpot)
        {
            transform.position = gardenCameraStartSpot.position;
            transform.rotation = gardenCameraStartSpot.rotation;
        }
        
        private void FollowTarget(Transform target)
        {
            Vector3 targetOffset = target.position + _offsetVector;
            
            transform.position = Vector3.SmoothDamp
                (transform.position, 
                targetOffset,
                ref _velocity,
                1f / Feature.Config.LerpSpeed
                );
        }

        private void CircularMotion()
        {
            float x = _target.position.x + Mathf.Cos(_angle) * _radius;
            float z = _target.position.z + Mathf.Sin(_angle) * _radius;
            Vector3 targetPosition = new Vector3(x, transform.position.y, z);
            
            transform.position = Vector3.Lerp
                (transform.position, 
                targetPosition, 
                Feature.Config.LerpSpeed
                );
        }
        
        private void LookAtTarget(Transform target)
        {
            transform.LookAt(target ,Vector3.up);
        }

        public void ActivateAnimator()
        {
            _animator.enabled = true;
        }
        
        // <Called by an Animation event>
        public void DeactivateAnimator()
        {
            _animator.enabled = false;
        }
        
        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}