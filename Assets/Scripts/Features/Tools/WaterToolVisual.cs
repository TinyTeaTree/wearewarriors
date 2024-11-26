using UnityEngine;

namespace Game
{
    public class WaterToolVisual : ToolVisual
    {
        public Transform origin;
        public Stream streamPrefab;

        private bool isPouring;
        private Stream currentStream;
        
        
        public override void StartWorking()
        {
            currentStream = Create();
            currentStream.Begin();
        }

        public override void EndWorking()
        {
            currentStream.End();
            currentStream = null;
        }

        private Stream Create()
        {
            return Instantiate(streamPrefab, origin.position, origin.rotation, transform);
        }
    }
}