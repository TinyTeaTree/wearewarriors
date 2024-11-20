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

        [SerializeField] private BaseSoundDesign _walkSounds;

        private bool _isMoving = false;
        private SoundPlayer _movingSoundPlayer;
        
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
                    
                    Feature.ProcessMove();

                    if (!_isMoving)
                    {
                        _isMoving = true;
                        DJ.Play(_walkSounds);
                        _movingSoundPlayer = DJ.GetPlayer(_walkSounds);
                    }

                    if (_movingSoundPlayer != null)
                    {
                        _movingSoundPlayer.SetPitch(clampedStrength);
                        _movingSoundPlayer.SetVolume(clampedStrength);
                    }
                }
                else
                {
                    Feature.ProcessIdle();

                    if (_isMoving)
                    {
                        _isMoving = false;
                        DJ.Stop(_walkSounds);
                    }
                }
                
                _animator.SetFloat("Speed", clampedStrength);

                var from = _leftFootDownAnchor.position;
                from.y += 50;

                float maxY = 0;


                if (Physics.Raycast(from, Vector3.down, out var hitInfoRight, 100f, LayerMask.GetMask("Floor")))
                {
                    if (hitInfoRight.point.y > maxY)
                    {
                        maxY = hitInfoRight.point.y;
                    }
                }
                
                from = _rightFootDownAnchor.position;
                from.y += 50;

                if (Physics.Raycast(from, Vector3.down, out var hitInfoLeft, 100f, LayerMask.GetMask("Floor")))
                {
                    if (hitInfoLeft.point.y > maxY)
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

        public GardenPlotVisual DetectPlot()
        {
            if (Physics.Raycast(
                    transform.position,
                    Vector3.down, 
                    out RaycastHit hitInfo,
                    50f, 
                    LayerMask.GetMask("GardenPlot"))
                )
            {
                return hitInfo.collider.gameObject.GetComponent<GardenPlotVisual>();
            }
            
            return null;
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
                    break;
                case TTools.PesticideSpray:
                    break;
                case TTools.ScareCrow:
                    break;
                case TTools.CropBox:
                    break;
                case TTools.StrawberrySeed:
                    break;
                case TTools.TomatoSeed:
                    break;
                case TTools.CornSeed:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(toolID), toolID, null);
            }
        }
    }
}