using Services;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

namespace GamePlay.Enemy
{
    public class SpiderBossMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        private ObjectsProvider _objectsProvider;


        [Inject]
        public void Construct(ObjectsProvider objectsProvider)
        {
            _objectsProvider = objectsProvider;
        }
        
        private void Update()
        {
            GameObject character = _objectsProvider.Character;
            if (character != null)
                _agent.destination = character.transform.position;
        }
    }
}
