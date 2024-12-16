using System;
using System.Collections;
using Core;
using Services;
using TMPro;
using UnityEngine;

namespace Game
{
    public class FloatingTextVisual : BaseVisual<FloatingText>
    {
        [SerializeField] TMP_Text floatingTextPrefab;

        private Coroutine popCoroutine;

        public void TurnOffPrefab()
        {
            floatingTextPrefab.gameObject.SetActive(false);
        }

        public void PopText(Vector3 position, string text, float duration)
        {
           popCoroutine = StartCoroutine(PopTextRoutine(position, text, duration));
        }
        
        IEnumerator PopTextRoutine(Vector3 pos, string text, float duration)
        {
            var popText = Summoner.CreateAsset(floatingTextPrefab, transform);
            var worldPos = UnityEngine.Camera.main.WorldToScreenPoint(pos);

            popText.transform.position = worldPos;
            popText.alpha = 0;
            popText.text = $"+{text} coins";
            popText.gameObject.SetActive(true);

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float progress = elapsedTime / duration;
                
                popText.transform.position = Vector3.Lerp(worldPos, worldPos + Vector3.up * 100, progress);
                
                if (progress < 0.5f)
                    popText.alpha = progress * 2; 
                else
                    popText.alpha = 1 - (progress - 0.5f) * 2;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            popText.alpha = 0;
            Destroy(popText.gameObject);
        }

    }
}