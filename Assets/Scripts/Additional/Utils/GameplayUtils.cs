using Additional.Constants;
using Additional.Extensions;
using UnityEngine;

namespace Additional.Utils
{
    public static class GameplayUtils
    {
        public static float DistanceBetween(Transform from, Transform to)
            => (from.position - to.position).sqrMagnitude;

        public static float DistanceBetween(Vector3 from, Vector3 to)
            => (from - to).sqrMagnitude;

        public static bool IsVisible(Transform from, Transform to, float offset = GameConstants.EntitiesPivotOffset)
        {
            Vector3 fromPosition = from.position.AddY(offset);
            Vector3 toPosition = to.position.AddY(offset);
            return !Physics.Linecast(fromPosition, toPosition, out RaycastHit _, GameConstants.ObstacleLayer);
        }

        public static bool IsVisible(Transform from, Transform to, LayerMask blockingLayer, float offset = GameConstants.EntitiesPivotOffset)
        {
            Vector3 fromPosition = from.position.AddY(offset);
            Vector3 toPosition = to.position.AddY(offset);
            return !Physics.Linecast(fromPosition, toPosition, out RaycastHit _, blockingLayer);
        }

        public static Vector3 PredictPosition(Transform target, Rigidbody targetRigidBody, float predictionTime)
            => target.position + targetRigidBody.velocity * predictionTime;
    }
}