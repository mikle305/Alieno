using UnityEngine;

namespace Additional.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 AddX(this Vector3 vector, float offset)
        {
            vector.x += offset;
            return vector;
        }
        
        public static Vector3 AddY(this Vector3 vector, float offset)
        {
            vector.y += offset;
            return vector;
        }
        
        public static Vector3 AddZ(this Vector3 vector, float offset)
        {
            vector.z += offset;
            return vector;
        }
    }
}