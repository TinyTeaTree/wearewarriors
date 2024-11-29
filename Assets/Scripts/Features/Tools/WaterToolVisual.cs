using System.Collections;
using UnityEngine;

namespace Game
{
    public class WaterToolVisual : ToolVisual
    {
        public Transform origin;
        public Stream streamPrefab;
        
        private Coroutine routine;
        
        public override void StartWorking()
        {
            routine = StartCoroutine(CreateStreams());
        }

        private IEnumerator CreateStreams()
        {
            yield return new WaitForSeconds(1f);
            
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(0f, 0.2f));

                var currentStream = Create();
                currentStream.Begin(origin.forward);
            }
        }
        
        public override void EndWorking()
        {
            StopCoroutine(routine);
        }

        private Stream Create()
        {
            var pos = origin.position + origin.right * Random.Range(-0.1f, 0.1f) + origin.up * Random.Range(-0.1f, 0.1f);
            return Instantiate(streamPrefab, pos, origin.rotation, null);
        }
    }
}