using System.Linq;
using Additional.Constants;
using GamePlay.Enemy;
using GamePlay.Other.Ids;
using StaticData;
using StaticData.Prefabs;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(EnemySpawn))]
    public class EnemySpawnerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.NotInSelectionHierarchy | GizmoType.Active | GizmoType.Selected)]
        public static void RenderCustomGizmo(EnemySpawn enemySpawner, GizmoType gizmo)
        {
            Gizmos.color = Color.red;

            if (enemySpawner == null)
            {
                Gizmos.DrawSphere(enemySpawner.transform.position, 0.5f);
            }
            else
            {
               GameObject prefab = GetEnemyPrefab(enemySpawner.Id);
               MeshFilter[] meshFilters = prefab.GetComponentsInChildren<MeshFilter>();
               SkinnedMeshRenderer[] skinnedMeshRenderers = prefab.GetComponentsInChildren<SkinnedMeshRenderer>();
               
               foreach (var filter in meshFilters)
               {
                   DrawMeshGizmo(enemySpawner,filter);
               }
               foreach (var renderer in skinnedMeshRenderers)
               {
                   DrawMeshGizmoSkinned(enemySpawner,renderer);
               }
            }
        }

        private static GameObject GetEnemyPrefab(EnemyId id)
            => Resources
                .Load<PrefabsConfig>(StaticDataPaths.PrefabsConfig)
                .GetEnemy(id);

        private static void DrawMeshGizmo(EnemySpawn enemySpawner,MeshFilter meshFilter)
        {
            Gizmos.DrawMesh(meshFilter.sharedMesh,enemySpawner.transform.position,enemySpawner.transform.rotation);
        }
        
        private static void DrawMeshGizmoSkinned(EnemySpawn enemySpawner,SkinnedMeshRenderer skinnedMeshRenderer)
        {
            Gizmos.DrawMesh(skinnedMeshRenderer.sharedMesh,enemySpawner.transform.position,enemySpawner.transform.rotation);
        }
    }
}