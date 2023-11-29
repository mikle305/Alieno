using UnityEngine;

namespace Additional.Utils
{
    public static class GameplayUtils
    {
        public static float DistanceBetween(Transform from, Transform to)
        {
            return (from.position - to.position).sqrMagnitude;
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
    }
}