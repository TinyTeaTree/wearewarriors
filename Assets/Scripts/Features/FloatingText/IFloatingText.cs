using System.Threading.Tasks;
using Core;
using UnityEngine;

namespace Game
{
    public interface IFloatingText : IFeature
    {
        Task Load();
        void PopText(Vector3 position, string text, float duration);
    }
}