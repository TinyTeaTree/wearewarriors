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
        Transform AvatarTransform { get; }
        void DropTool(ToolVisual tool);
        
        List<TPlant> PlantsGathered { get; }
        void SellPlants(Transform[] sellPoint);
    }
}