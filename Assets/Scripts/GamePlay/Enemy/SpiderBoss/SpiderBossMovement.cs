using Services;
using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.Enemy
{
    public class SpiderBossMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
    
        void Update()
        {
            GameObject character = ObjectsProvider.Instance.Character;
            if (character != null)
                _agent.destination = character.transform.position;
        }
    }
}