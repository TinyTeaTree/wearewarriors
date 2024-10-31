using System.Threading.Tasks;
using Core;
using UnityEngine;

namespace Game
{
    public interface IAvatar : IFeature
    {
        Task Load();

        void Activate();
        
        Transform HandTransform { get; }
    }
}