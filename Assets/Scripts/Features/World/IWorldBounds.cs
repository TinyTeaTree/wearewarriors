using UnityEngine;

namespace Game
{
    public interface IWorldBounds
    {
        Vector3 TopBounds { get; }
        Vector3 RightBounds { get; }
        Vector3 LeftBounds { get; }
        Vector3 BottomBounds { get; }
    }
}