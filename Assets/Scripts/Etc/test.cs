using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class test : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;

    [Button]
    void SetDestination()
    {
        agent.destination = target.position;
    }
}
