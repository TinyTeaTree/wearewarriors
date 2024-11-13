using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Game
{
    public class AvatarVisual : BaseVisual<Avatar>
    {
        private IProvideDirection _directionProvider;
        [SerializeField] private float _speed;
        
        [SerializeField, Tooltip("This Anchor should be at the bottom of the Sole and will Raycast to Elevate from Floor properly")] 
        private Transform _leftFootDownAnchor;
        [SerializeField, Tooltip("This Anchor should be at the bottom of the Sole and will Raycast to Elevate from Floor properly")] 
        private Transform _rightFootDownAnchor;

        [Header("Tools Pivot")] 
        [SerializeField] private List<AvatarAnchors> _avatarAnchors;
        public List<AvatarAnchors> AvatarAnchors => _avatarAnchors;
        
        private Coroutine _movementRoutine;
        private Coroutine _updateCycleRoutine;
        
        public void SetPos(Vector3 pos)
        {
            transform.position = pos;
        }

        public void SetDirectionProvider(IProvideDirection directionProvider)
        {
            _directionProvider = directionProvider;
        }

        public void StartMovement()
        {
            if (_movementRoutine != null)
            {
                StopCoroutine(_movementRoutine);
            }
            _movementRoutine = StartCoroutine(MovementRoutine());

            if (_updateCycleRoutine != null)
            {
                StopCoroutine(_updateCycleRoutine);
            }

            _updateCycleRoutine = StartCoroutine(UpdateCycleRoutine());
        }

        IEnumerator UpdateCycleRoutine()
        {
            while (true)
            {
                Feature.Update();
                yield return null;
            }
        }
        
        IEnumerator MovementRoutine()
        {
            while (true)
            {
                var direction = _directionProvider.Direction;

                var strength = direction.magnitude;

                var clampedStrength = Mathf.Clamp01(strength);

                if (clampedStrength > 0.03f)
                {
                    var translation = new Vector3(direction.x, 0f, direction.y).normalized * (_speed * clampedStrength * Time.deltaTime);

                    transform.LookAt(transform.position + translation);

                    transform.position += translation;
                }

                var from = _leftFootDownAnchor.position;
                from.y += 50;

                float maxY = 0;


                if (Physics.Raycast(from, Vector3.down, out var hitInfoRight, 100f, LayerMask.GetMask("Floor")))
                {
                    if (hitInfoRight.point.y < maxY)
                    {
                        maxY = hitInfoRight.point.y;
                    }
                }

                if (Physics.Raycast(from, Vector3.down, out var hitInfoLeft, 100f, LayerMask.GetMask("Floor")))
                {
                    if (hitInfoLeft.point.y < maxY)
                    {
                        maxY = hitInfoRight.point.y;
                    }
                }
                
                var pos = transform.position;
                pos.y = maxY;
                transform.position = pos;

                yield return null;
            }
        }
        
        public void StopMovement()
        {
            if (_movementRoutine != null)
            {
                StopCoroutine(_movementRoutine);
            }
            _movementRoutine = null;
        }
    }
}