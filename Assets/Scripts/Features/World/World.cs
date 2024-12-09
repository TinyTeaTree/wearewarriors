using System.Threading.Tasks;
using Core;
using UnityEngine;

namespace Game
{
    public class World : BaseVisualFeature<WorldVisual>, IWorld
    {
        public async Task Load()
        {
            await CreateVisual();
            _visual.StartWorldSounds();
        }

        public Vector3 AvatarStartSpot => _visual.AvatarStartSpot.position;
        public Transform CameraStartSpot => _visual.CameraStartSpot;

        public Transform GetAnimalStartPoint()
        {
            return _visual.AnimalStartPoints.GetRandom();
        }

        public Vector3 TopBounds => _visual.TopBounds;
        public Vector3 RightBounds => _visual.RightBounds;
        public Vector3 LeftBounds => _visual.LeftBounds;
        public Vector3 BottomBounds => _visual.BottomBounds;
    }
}