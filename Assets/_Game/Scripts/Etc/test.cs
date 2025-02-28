using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.AnimationController;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class test : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    [FormerlySerializedAs("animController")] public CharacterAnimController characterAnimController;
    private bool onMove = false;
    private Vector3 lastPosition;
    private bool isAvoiding;

    private void Start()
    {
        DOVirtual.DelayedCall(.25f, SetDestination);
        lastPosition = transform.position;
    }

    [Button]
    private void SetDestination()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > agent.stoppingDistance)
        {
            characterAnimController.PlayAnim(AnimCharacterLayer.Move, 0, CallBack);
            agent.SetDestination(target.position);
        }
        else
            Patrol();
    }

    private void Update()
    {
        if (!agent.enabled) return;
        if (!onMove) return;
        if (agent.pathPending) return;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            OnAgentMoveDone();
        }

    }

    private void OnAgentMoveDone()
    {
        characterAnimController.PlayAnim(AnimCharacterLayer.Idle, 0);
        onMove = false;
        Patrol();
    }

    private void Patrol()
    {
        //DOVirtual.DelayedCall(5f, SetDestination);
      
    }

    private void CallBack()
    {
        Debug.Log("call back");
        
        onMove = true;
    }
}
