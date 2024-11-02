using System.Threading.Tasks;
using Core;
using UnityEngine;

namespace Game
{
    public interface ICamera : IFeature
    {
        Task Load();

        void ActivateAnimation();
    }
}