using GamePlay.Projectile;
using GamePlay.Statuses;
using UnityEngine;

namespace Services.Statuses
{
    public class BouncyWallHandler : StatusHandler<BouncyWallStatus>
    {
        private LayerMask _obstacleLayer;

        
        public BouncyWallHandler(LayerMask obstacleLayer)
        {
            _obstacleLayer = obstacleLayer;
        }

        protected override bool OnHandle(DamageData damageData, BouncyWallStatus status)
        {
            if (damageData.Receiver != null)
                return true;

            if (status.CountLeft == 0)
                return true;

            status.CountLeft--;
            ReflectDirection(damageData);
            return false;
        }

        private void ReflectDirection(DamageData damageData)
        {
            var projectileMovement = damageData.Projectile.GetComponent<ProjectileMovement>();
            var ray = new Ray(projectileMovement.transform.position - projectileMovement.Direction, projectileMovement.Direction);

            if (Physics.Raycast(ray, out RaycastHit hit, _obstacleLayer))
                projectileMovement.Direction = Vector3.Reflect(projectileMovement.Direction, hit.normal);
        }
    }
}