using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Game
{
    public class CropBoxVisual : ToolVisual
    {
        [SerializeField] private Transform fillPoint;
        [SerializeField] private AnimationCurve heightGatherCurve;
        [SerializeField] private List<Transform> cropPlaces;
        public Transform FillPoint => fillPoint;
        

        public void Gather(GameObject crop)
        {
            StartCoroutine(GatherRoutine(crop));
        }
        
        private IEnumerator GatherRoutine(GameObject crop)
        {
            float timePassed = 0f;
            float duration = 0.6f;
            Vector3 from = crop.transform.position;

            while (timePassed < duration)
            {
                float ratio = timePassed / duration;
                crop.transform.position = Vector3.Lerp(from, FillPoint.position, ratio);
                crop.transform.AddY(heightGatherCurve.Evaluate(ratio));
                
                yield return null;
                timePassed += Time.deltaTime;
            }
            
            crop.transform.SetParent(FillPoint);

            var fromPlace = cropPlaces.GetRandom();
            var toPlace = cropPlaces.GetRandom();
            
            var restPos = Vector3.Lerp(fromPlace.position, toPlace.position, Random.value);
            var restRot = Quaternion.Lerp(fromPlace.rotation, toPlace.rotation, Random.value);

            crop.transform.position = restPos;
            crop.transform.rotation = restRot;
        }
    }
}