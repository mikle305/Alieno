using UnityEngine;

namespace Additional.Utils
{
    public static class GameplayUtils
    {
        public static float DistanceBetween(Transform from, Transform to)
        {
            return (from.position - to.position).sqrMagnitude;
        }
    }
}