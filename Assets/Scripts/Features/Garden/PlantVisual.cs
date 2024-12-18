using System.Collections;
using Core;
using Services;
using UnityEngine;

namespace Game
{
    public class PlantVisual : BaseVisual<Garden>
    {
        public TPlant PlantID;
        public string MarkID;
        
        private float _progress = 0f;
        private float _targetProgress = 0f;
        private bool _isComplete = false;
        
        private Vector3 _initScale = Vector3.zero;
        private Vector3 _targetScale = Vector3.one * 3;
        
        public bool IsComplete => _isComplete;
        [SerializeField] private MeshRenderer _renderer;
        
        Coroutine plantGrowthRoutine;

        [SerializeField] private new Animation animation;
        [SerializeField] private GameObject cropPrefab;
        [SerializeField] private Transform cropSpawnPoint;
        
        public void StartGrowing()
        {
            if (plantGrowthRoutine != null)
            {
                StopCoroutine(plantGrowthRoutine);
            }
            plantGrowthRoutine = StartCoroutine(GrowCoroutine());
        }

        private IEnumerator GrowCoroutine()
        {
            while (_progress <= 0.99f)
            {
                _progress = Mathf.Lerp(_progress, _targetProgress, 0.1f);
                transform.localScale = Vector3.Lerp(_initScale ,_targetScale, _progress);
                Feature.Marks.GetMark<MarkPlantProgress>(MarkID).UpdateMarkProgress(_progress);
                
                yield return null;
            }
            transform.localScale = _targetScale;
            Feature.Marks.GetMark<MarkPlantProgress>(MarkID).SelfDestroy();
            Feature.Marks.RemoveMark(MarkID);
            MarkID = null;
            _isComplete = true;
            StartCoroutine(BounceEffect());
        }

        public void SetUp(PlotData data)
        {
            _targetProgress = data.State == TPlotState.PlantRiping ? 1f : data.Progress;
            _progress = _targetProgress;
            transform.localScale = Vector3.Lerp(_initScale ,_targetScale, _progress);

            if (Mathf.Approximately(_targetProgress, 1f))
            {
                _isComplete = true;
            }
            else
            {
                MarkID = Feature.Marks.AddMark(transform, TMark.PlantProgress);
                StartGrowing();

                _isComplete = false;
            }
        }

        public void WaterPlant(PlotData data)
        {
            StartCoroutine(WaterEffectRoutine());
            _targetProgress = data.State == TPlotState.PlantRiping ? 1f : data.Progress;
        }

        private IEnumerator WaterEffectRoutine()
        {
            float passed = 0f;
            float duration = 0.2f;
            while (passed < duration)
            {
                float ratio = passed / duration;
                _renderer.material.SetColor("_Water", Color.Lerp(Color.black, Color.blue, ratio));
                
                yield return null;
                passed += Time.deltaTime;
            }
            
            passed = 0f;
            while (passed < duration)
            {
                float ratio = passed / duration;
                _renderer.material.SetColor("_Water", Color.Lerp(Color.blue, Color.black, ratio));
                
                yield return null;
                passed += Time.deltaTime;
            }
            
            _renderer.material.SetColor("_Water", Color.black);
        }
        
        private IEnumerator BounceEffect()
        {
            Vector3 currentScale = transform.localScale;
            Vector3 bounceScale = currentScale * 1.23f; 

            float bounceSpeed = 0.12f; 
            float progress = 0f;
           
            while (progress < 1f)
            {
                progress += Time.deltaTime / bounceSpeed;
                transform.localScale = Vector3.Lerp(currentScale, bounceScale, progress);
                yield return null;
            }

            progress = 0f;
            
            while (progress < 1f)
            {
                progress += Time.deltaTime / bounceSpeed;
                transform.localScale = Vector3.Lerp(bounceScale, currentScale, progress);
                yield return null;
            }
        }

        public void Shake()
        {
            animation.Play("Shake");
        }

        public void AnimateGather(ToolVisual holdingTool)
        {
            var crop = Summoner.CreateAsset(cropPrefab, cropSpawnPoint);
            crop.transform.localPosition = Vector3.zero;
            crop.transform.localRotation = Quaternion.identity;

            var box = holdingTool as CropBoxVisual;

            box.Gather(crop);
        }
    }
}