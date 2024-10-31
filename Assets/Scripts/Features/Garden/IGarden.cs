using System;
using System.Threading.Tasks;
using Core;
using UnityEngine;

namespace Game
{
    public interface IGarden : IFeature, IDisposable
    {
        Task Load();
        
        Vector3 AvatarStartSpot { get; }
        Transform CameraStartSpot { get; }
    }
}