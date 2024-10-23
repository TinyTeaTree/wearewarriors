using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Game
{
    public class AvatarVisual : BaseVisual<Avatar>
    {
        private IProvideDirection _directionProvider;
        [SerializeField] private float _speed;

        private Coroutine _movementRoutine;
        
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