using System.Threading.Tasks;
using Core;
using UnityEngine;

namespace Game
{
    public interface IWorld : IFeature
    {
        Task Load();
        Vector3 AvatarStartSpot { get; }
        Transform CameraStartSpot { get; }
        Transform GetAnimalStartPoint();
    }
}