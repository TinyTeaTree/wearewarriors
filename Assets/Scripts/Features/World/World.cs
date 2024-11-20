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
        }

        public Vector3 AvatarStartSpot => _visual.AvatarStartSpot.position;
        public Transform CameraStartSpot => _visual.CameraStartSpot;
    }
}