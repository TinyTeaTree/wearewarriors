using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Game
{
    public class PlantVisual : BaseVisual<Garden>
    {
        public TPlant PlantID;

        private float _progress = 0f;
        private float _targetProgress = 0f;
        
        private Vector3 _initScale = Vector3.zero;
        private Vector3 _targetScale = Vector3.one * 3;
        private void Start()
        {
            StartCoroutine(GrowCoroutine());
        }

        private IEnumerator GrowCoroutine()
        {
            while (_progress < 1f)
            {
                _progress = Mathf.Lerp(_progress, _targetProgress, 0.5f * Time.deltaTime);
                
                transform.localScale = Vector3.Lerp(_initScale ,_targetScale, _progress);
                
                yield return null;
            }
            transform.localScale = _targetScale;
        }

        public void WaterPlant(float amount)
        {
            _targetProgress = Mathf.Clamp01(_targetProgress + amount);
        }
    }
}