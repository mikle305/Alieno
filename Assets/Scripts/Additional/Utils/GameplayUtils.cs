using UnityEngine;

namespace Additional.Utils
{
    public static class GameplayUtils
    {
        public static float DistanceBetween(Transform from, Transform to)
        {
            return (from.position - to.position).sqrMagnitude;
        }
        
        public static float DistanceBetween(Vector3 from, Vector3 to)
        {
            return (from - to).sqrMagnitude;
        }
        
        public static bool IsVisible(Transform start,Transform target)
        {
            Transform character = start;
            RaycastHit hit;
            if (Physics.Linecast(character.position, target.position,out hit,(1 << LayerMask.NameToLayer("Obstacle")),QueryTriggerInteraction.UseGlobal))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        public static Vector3 CalcFuturePos(Transform target,Rigidbody targetRigidBody,float predictionTime){
            var finalPos = target.position;
            var velocity = targetRigidBody.velocity;

            velocity *= predictionTime;
            finalPos += velocity;

            return finalPos;
        }

        public static LayerMask GetEnemyLayerMask()
        {
            return (1 << LayerMask.NameToLayer("Enemy"));
        }
        
        public static LayerMask GetPlayerLayerMask()
        {
            return (1 << LayerMask.NameToLayer("Player"));
        }
    }
}