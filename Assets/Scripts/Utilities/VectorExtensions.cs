using UnityEngine;

namespace Utilities
{
    public static class VectorExtensions
    {
        public static Vector3 Horizontal(this Vector3 vector)
        {
            return new Vector3(vector.x, 0f, vector.z);
        }
        
        public static Vector3 Vertical(this Vector3 vector)
        {
            return new Vector3(0f, vector.y, 0f);
        }
    }
}
