using System.Threading.Tasks;
using Core;
using UnityEngine;

namespace Game
{
    public interface ICamera : IFeature
    {
        Task Load();
        
        void LookAt(Vector3 position);
        
        void Follow(Vector3 position);
        
        void StopFollowing();

        void ActivateCameraAnimation();
    }
}