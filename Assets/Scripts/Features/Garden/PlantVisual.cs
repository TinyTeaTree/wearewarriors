using System.Collections;
using System.Collections.Generic;
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
        
        private Vector3 _initScale = Vector3.zero;
        private Vector3 _targetScale = Vector3.one * 3;
        
        [SerializeField] private MeshRenderer _renderer;
        
        Coroutine plantGrowthRoutine;

        [SerializeField] private new Animation animation;
        [SerializeField] private GameObject cropPrefab;
        [SerializeField] private Transform cropSpawnPoint;

        [SerializeField] private List<Transform> crops;
        
        public void GrowRoutine(PlotData plotData)
        {
            if (plantGrowthRoutine != null)
            {
                StopCoroutine(plantGrowthRoutine);
            }
            plantGrowthRoutine = StartCoroutine(GrowCoroutine(plotData));
        }

        private IEnumerator GrowCoroutine(PlotData plotData)
        {
            while (true)
            {
                _progress = Mathf.Lerp(_progress, _targetProgress, 0.1f);
                SetCropProgress();
                transform.localScale = Vector3.Lerp(_initScale ,_targetScale, plotData.State == TPlotState.PlantRiping ? 1f : _progress);
                
                yield return null;
            }
        }

        private void SetCropProgress()
        {
            float cropAmount = 1f / crops.Count;

            float cropProgress = _progress;
            int index = 0;
            for (int i = 0; i < crops.Count; ++i)
            {
                var crop = crops[i];
                if (cropProgress > cropAmount)
                {
                    crop.localScale = Vector3.one;
                    cropProgress -= cropAmount;
                }
                else
                {
                    crop.localScale = Vector3.one * (cropProgress / cropAmount);
                    cropProgress = 0f;
                }
            }
        }

        public void SetUp(PlotData data)
        {
            _targetProgress = data.Progress;
            _progress = _targetProgress;
            transform.localScale = Vector3.Lerp(_initScale ,_targetScale, _progress);
            SetCropProgress();
            
            GrowRoutine(data);

        }

        public void SetPlantProgress(PlotData data)
        {
            StartCoroutine(WaterEffectRoutine());
            if (data.Progress > _targetProgress + 0.01f)
            {
                StartCoroutine(BounceEffect());
            }

            _targetProgress = data.Progress;
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