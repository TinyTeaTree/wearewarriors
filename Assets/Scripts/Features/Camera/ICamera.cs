using System.Threading.Tasks;
using Core;
using UnityEngine;

namespace Game
{
    public interface ICamera : IFeature
    {
        UnityEngine.Camera WorldCamera { get; }
        
        Task Load();

        void Start();

        void ActivateAnimation();
    }
}