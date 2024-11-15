using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Game
{
    public class SheepVisual :  BaseVisual<Sheeps>
    {
        public Animator animator;
        private string walkForwardAnimation = "walk_forward";
        private string idle = "idle";
        private string runForwardAnimation = "run_forward";
        private string turn90LAnimation = "turn_90_L";
        private string turn90RAnimation = "turn_90_R";
        private string trotAnimation = "trot_forward";
        private string sittostandAnimation = "sit_to_stand";
        private string standtositAnimation = "stand_to_sit";

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

        void Start()
        {
            animator = GetComponent<Animator>();
        }


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
            animator.SetTrigger("Run");

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

            var pos = transform.position;
            pos.y = maxY;
            transform.position = pos;
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