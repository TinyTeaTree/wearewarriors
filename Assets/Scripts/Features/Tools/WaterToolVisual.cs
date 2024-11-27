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
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(0f, 0.2f));

                var currentStream = Create();
                currentStream.Begin();
            }
        }
        
        public override void EndWorking()
        {
            StopCoroutine(routine);
        }

        private Stream Create()
        {
            return Instantiate(streamPrefab, origin.position, origin.rotation, null);
        }
    }
}