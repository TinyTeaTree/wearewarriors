using UnityEngine;

namespace Core
{
    public static class VectorUtils
    {
        public static Vector2 XY(this Vector3 vec)
        {
            return new Vector2(vec.x, vec.y);
        }
        
        public static Vector2 XZ(this Vector3 vec)
        {
            return new Vector2(vec.x, vec.z);
        }
        
        public static Vector3 XY(this Vector2 vec)
        {
            return new Vector3(vec.x, vec.y, 0f);
        }
        
        public static Vector3 XZ(this Vector2 vec)
        {
            return new Vector3(vec.x, 0f, vec.y);
        }
    }
}