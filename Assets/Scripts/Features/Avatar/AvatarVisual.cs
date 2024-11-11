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

        [Header("Tools Pivot")] 
        [SerializeField] private List<AvatarAnchors> _avatarAnchors;

        [SerializeField] private Animator _animator;
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

        private bool inPlot = false;
        private float lastAnimationTime = 0f;
        IEnumerator MovementRoutine()
        {
            while (true)
            {
                inPlot = false;
                var direction = _directionProvider.Direction;

                var strength = direction.magnitude;

                var clampedStrength = Mathf.Clamp01(strength);

                if (clampedStrength > 0.03f)
                {
                    var translation = new Vector3(direction.x, 0f, direction.y).normalized * (_speed * clampedStrength * Time.deltaTime);

                    transform.LookAt(transform.position + translation);
                    
                    transform.position += translation;
                }
                else
                {
                    if (Physics.CheckSphere(transform.position, 1.5f, LayerMask.GetMask("Plot")))
                    {
                        inPlot = true;
                    }
                }
                
                _animator.SetFloat("Speed", clampedStrength);


                if (inPlot)
                {
                    if (lastAnimationTime + 1f < Time.time)
                    {
                        _animator.SetTrigger("Harvest");
                        lastAnimationTime = Time.time;
                    }
                }

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