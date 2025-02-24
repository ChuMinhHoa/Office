using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class test : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public AnimController animController;
    private bool onMove = false;
    private void Start()
    {
        DOVirtual.DelayedCall(.25f, SetDestination);
    }

    [Button]
    private void SetDestination()
    {
        animController.PlayAnim(AnimLayer.Move, 0, CallBack);
        onMove = true;
    }

    private void Update()
    {
        if (onMove)
        {
            if (agent.hasPath)
            {
                if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
                {
                    OnAgentMoveDone();
                }
            }
            
        }
       
    }

    private void OnAgentMoveDone()
    {
        Debug.Log("move done");
        animController.PlayAnim(AnimLayer.Idle, 0);
        onMove = false;
    }

    private void CallBack()
    {
        Debug.Log("call back");
        agent.destination = target.position;
    }
}
