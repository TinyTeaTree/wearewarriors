using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using UnityEngine;

namespace Game
{
    public interface IAvatar : IFeature
    {
        Task Load();
        void Activate();
        List<Transform> ToolsPivot { get; }
        Transform AvatarTransform { get; }
    }
}