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
        
        public static void SetX(this Transform transform, float x)
        {
            var pos = transform.position;
            pos.x = x;
            transform.position = pos;
        }
        
        public static void SetZ(this Transform transform, float z)
        {
            var pos = transform.position;
            pos.z = z;
            transform.position = pos;
        }
        
        public static void AddY(this Transform transform, float y)
        {
            var pos = transform.position;
            pos.y += y;
            transform.position = pos;
        }
        
        public static void AddX(this Transform transform, float x)
        {
            var pos = transform.position;
            pos.x += x;
            transform.position = pos;
        }
        
        public static void AddZ(this Transform transform, float z)
        {
            var pos = transform.position;
            pos.z += z;
            transform.position = pos;
        }
        
    }
}