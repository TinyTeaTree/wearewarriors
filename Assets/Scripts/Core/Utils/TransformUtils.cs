using UnityEngine;

namespace Core
{
    public static class TransformUtils
    {
        public static void SetY(this Transform transform, float y)
        {
            var pos = transform.position;
            pos.y = y;
            transform.position = pos;
        }
        
    }
}