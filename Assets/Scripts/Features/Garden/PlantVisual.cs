using System.Collections;
using Core;
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
        
        private void Start()
        {
            StartCoroutine(GrowCoroutine());
        }

        private IEnumerator GrowCoroutine()
        {
            while (_progress <= 0.99f)
            {
                _progress = Mathf.Lerp(_progress, _targetProgress, 0.1f);
                
                transform.localScale = Vector3.Lerp(_initScale ,_targetScale, _progress);

                StartCoroutine(BounceEffect());
                
                Feature.Marks.GetMark<MarkPlantProgress>(MarkID).UpdateMarkProgress(_progress);
                
                yield return null;
            }
            transform.localScale = _targetScale;
            Feature.Marks.GetMark<MarkPlantProgress>(MarkID).SelfDestroy();
            Feature.Marks.RemoveMark(MarkID);
            MarkID = null;
            _isComplete = true;
        }

        public void WaterPlant(float amount)
        {
            _targetProgress = Mathf.Clamp01(_targetProgress + amount);
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
    }
}