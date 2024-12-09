using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Services;
using UnityEngine;

namespace Game
{
    public class AvatarVisual : BaseVisual<Avatar>
    {
        private IProvideDirection _directionProvider;
        [SerializeField] private float _speed;

        [SerializeField] private Animator _animator;
        
        [SerializeField, Tooltip("This Anchor should be at the bottom of the Sole and will Raycast to Elevate from Floor properly")] 
        private Transform _leftFootDownAnchor;
        [SerializeField, Tooltip("This Anchor should be at the bottom of the Sole and will Raycast to Elevate from Floor properly")] 
        private Transform _rightFootDownAnchor;

        [Header("Tools Pivot")] 
        [SerializeField] private List<AvatarAnchors> _avatarAnchors;
        public List<AvatarAnchors> AvatarAnchors => _avatarAnchors;
        
        private Coroutine _movementRoutine;
        private Coroutine _updateCycleRoutine;

        private IWorldBounds worldBounds;
        
        [SerializeField] private List<BaseSoundDesign> footsteps;
        
        public void SetPos(Vector3 pos)
        {
            transform.position = pos;
        }

        public void SetDirectionProvider(IProvideDirection directionProvider)
        {
            _directionProvider = directionProvider;
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        public void StartMovement(IWorldBounds worldBounds)
        {
            this.worldBounds = worldBounds;
            
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
                if (!Feature.Shop.Visual.OnDisplay())
                {
                    var direction = _directionProvider.Direction;

                    var strength = direction.magnitude;

                    var clampedStrength = Mathf.Clamp01(strength);

                    if (clampedStrength > 0.03f)
                    {
                        var translation = new Vector3(direction.x, 0f, direction.y).normalized *
                                          (_speed * clampedStrength * Time.deltaTime);

                        transform.LookAt(transform.position + translation);

                        transform.position += translation;

                        Clamp();

                        Feature.ProcessMove();
                    }
                    else
                    {
                        Feature.UpdateIdle();
                    }

                    _animator.SetFloat("Speed", clampedStrength);

                    var from = _leftFootDownAnchor.position;
                    from.y += 50;

                    float maxY = 0;


                    if (Physics.Raycast(from, Vector3.down, out var hitInfoRight, 100f,
                            LayerMask.GetMask("Floor", "GardenPlot")))
                    {
                        if (hitInfoRight.point.y > maxY)
                        {
                            maxY = hitInfoRight.point.y;
                        }
                    }

                    from = _rightFootDownAnchor.position;
                    from.y += 50;

                    if (Physics.Raycast(from, Vector3.down, out var hitInfoLeft, 100f,
                            LayerMask.GetMask("Floor", "GardenPlot")))
                    {
                        if (hitInfoLeft.point.y > maxY)
                        {
                            maxY = hitInfoRight.point.y;
                        }
                    }

                    var pos = transform.position;
                    pos.y = maxY;
                    transform.position = pos;
                }
                yield return null;
            }
        }

        public void Clamp()
        {
            if (transform.position.z > worldBounds.TopBounds.z)
            {
                transform.SetZ(worldBounds.TopBounds.z);
            }
            
            if (transform.position.x > worldBounds.RightBounds.x)
            {
                transform.SetX(worldBounds.RightBounds.x);
            }
            
            if (transform.position.z < worldBounds.BottomBounds.z)
            {
                transform.SetZ(worldBounds.BottomBounds.z);
            }
            
            if (transform.position.x < worldBounds.LeftBounds.x)
            {
                transform.SetX(worldBounds.LeftBounds.x);
            }
        }

        public void Step_Event() //This is called from Animation Event
        {
            DJ.Play(footsteps.GetRandom());
        }

        public void StopMovement()
        {
            if (_movementRoutine != null)
            {
                StopCoroutine(_movementRoutine);
            }
            _movementRoutine = null;
        }

        public GardenPlotVisual TryGetPlot()
        {
            if (Physics.Raycast(
                    transform.position + Vector3.up * 5,
                    Vector3.down, 
                    out RaycastHit hitInfo,
                    15, 
                    LayerMask.GetMask("GardenPlot"))
                )
            {
                return hitInfo.collider.gameObject.GetComponent<GardenPlotVisual>();
            }
            
            return null;
        }
        
        public bool IsNearStore()
        {
            if (Physics.Raycast(
                    transform.position + Vector3.up * 5,
                    Vector3.down, 
                    out RaycastHit hitInfo,
                    15, 
                    LayerMask.GetMask("Store"))
               )
            {
                return true;
            }
            
            return false;
        }

        public void AnimateTool(TTools toolID, bool state)
        {
            switch (toolID)
            {
                case TTools.Hands:
                    break;
                case TTools.Shovel:
                    break;
                case TTools.Rake:
                    _animator.SetBool("Sweep", state);
                    break;
                case TTools.WateringCan:
                    _animator.SetBool("Water", state);
                    break;
                case TTools.PesticideSpray:
                    break;
                case TTools.ScareCrow:
                    break;
                case TTools.CropBox:
                    break;
                case TTools.GrainBag:
                    _animator.SetBool("Plant", state);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(toolID), toolID, null);
            }
        }

        public void SetRot(float rot)
        {
            transform.rotation = Quaternion.Euler(0f, rot, 0f);
        }

        public void SetStatus(bool status)
        {
           
        }
    }
}