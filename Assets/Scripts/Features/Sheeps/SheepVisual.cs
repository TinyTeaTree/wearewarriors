using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Game
{
    public class SheepVisual :  BaseVisual<Sheeps>
    {
        public Animator animator;

        [SerializeField] private float _minMoveDuration;
        [SerializeField] private float _maxMoveDuration;

        [SerializeField] private float _minIdleDuration;
        [SerializeField] private float _maxIdleDuration;
        
        [SerializeField] private float _speed = 3.3f;
        
        [SerializeField, Tooltip("Closer Than This, the Sheep will move away from Avatar")] private float _distanceTolerance;
        [SerializeField, Tooltip("Farther Than This, the Sheep will move towards the Avatar")] private float _distanceMagnet;


        [SerializeField] private List<Transform> _footAnchros;
        
        public float DistanceTolerance => _distanceTolerance;
        public float DistanceMagnet => _distanceMagnet;

        private Coroutine _moveRoutine;

        public float GetIdleDuration()
        {
            return Random.Range(_minIdleDuration, _maxIdleDuration);
        }

        public float GetMoveDuration()
        {
            return Random.Range(_minMoveDuration, _maxMoveDuration);
        }

        public void MoveTowardsDirection(Vector2 direction, float speedFactor)
        {
            if (_moveRoutine != null)
            {
                StopCoroutine(_moveRoutine);
            }
            _moveRoutine = StartCoroutine(MoveTowardsDirectionRoutine(direction, speedFactor));
        }

        private IEnumerator MoveTowardsDirectionRoutine(Vector2 direction, float speedFactor)
        {
            animator.SetTrigger(speedFactor > 1.1f ? "Run" : "Walk");

            while (true)
            {
                var newForward = Vector2.Lerp(transform.forward.XZ(), direction, Time.deltaTime * 1.5f);
                
                transform.LookAt(transform.position + newForward.XZ());
                
                transform.Translate(Vector3.forward * Time.deltaTime * _speed * speedFactor, Space.Self);
                
                yield return null;
            }
        }

        void Update()
        {
            float maxY = -100f;

            foreach (var anchor in _footAnchros)
            {
                var from = anchor.position;
                from.y += 50;

                if (Physics.Raycast(from, Vector3.down, out var hitInfoRight, 100f, LayerMask.GetMask("Floor")))
                {
                    if (hitInfoRight.point.y > maxY)
                    {
                        maxY = hitInfoRight.point.y;
                    }
                }
            }

            transform.SetY(maxY);
        }

        public void Idle()
        {
            animator.SetTrigger("Idle");
            
            if (_moveRoutine != null)
            {
                StopCoroutine(_moveRoutine);
                _moveRoutine = null;
            }
        }
    }
}