using System.Threading.Tasks;
using Core;
using UnityEngine;

namespace Game
{
    public interface IHud : IFeature
    {
        Task Load();
        void SetHudOnCanvas(Canvas canvas);
        
        UnityEngine.Camera HudCamera { get; }
        Transform NavBar { get; }
    }
}