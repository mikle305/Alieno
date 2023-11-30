using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;
using UnityEngine.AI;

public class SpiderBossMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    void Update()
    {
        _agent.destination = ObjectsProvider.Instance.Character.transform.position;
    }
}
