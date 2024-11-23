using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Unity.VisualScripting.YamlDotNet.Core;
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
    }
}