using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class BaseVisual : MonoBehaviour, IAmDestructible
    {
        protected BaseFeature _baseFeature;

        private bool isDestroyed;
        
        public void SetFeature<TFeature>(TFeature feature)
            where TFeature : BaseFeature
        {
            _baseFeature = feature;
        }
        
        public void SelfDestroy()
        {
            Destroy(gameObject);
            isDestroyed = true;
        }

        public bool IsDestroyed()
        {
            return this == null || isDestroyed;
        }

        public void InvokeAfter(System.Action action, float time)
        {
            StartCoroutine(InvokeAfterRoutine(action, time));
        }

        private IEnumerator InvokeAfterRoutine(System.Action action, float time)
        {
            yield return new WaitForSeconds(time);
            action();
        }
    }
    public class BaseVisual<TFeature> : BaseVisual
        where TFeature : BaseFeature
    {
        protected TFeature Feature => _baseFeature as TFeature;
    }
}