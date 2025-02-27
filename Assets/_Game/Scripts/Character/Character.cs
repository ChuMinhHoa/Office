using System;
using AnimationController;
using UnityEngine;
using Character.StateMachine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Character
{
    public class Character : MonoBehaviour
    {
        public StateMachine<Character> StateMachine => _mStateMachine;
        private StateMachine<Character> _mStateMachine;

        public NavMeshAgent cAgent;
        public Transform cTarget;
        public CharacterAnimController cAnimController;
        
        private void Awake()
        {
            _mStateMachine = new StateMachine<Character>(this);
            _mStateMachine.SetGlobalState(GlobalBotState.Instance);
            _mStateMachine.SetCurrentState(GlobalBotState.Instance);
            ChangeState(CharIdleState.Instance);
        }

        public void Update()
        {
            StateMachine.Update();
        }

        public void ChangeState(State<Character> newState)
        {
            StateMachine.ChangeState(newState);
        }
        
        #region Character Idle

        [SerializeField] private float timeIdleSetting = 5f;
        [SerializeField] private float timeIdle;
        public void CharIdleEnter()
        {
            cAnimController.PlayAnim(AnimPlayerLayer.Idle, Random.Range(0, 5));
            timeIdleSetting = cAnimController.GetCurrentTimeAnimation();
            timeIdle = 0;
        }
        
        public void CharIdleExecute()
        {
            if (timeIdle < timeIdleSetting)
                timeIdle += Time.deltaTime;
            else
            {
                ChangeState(CharIdleState.Instance);
            }
        }

        public void CharIdleEnd()
        {
            
        }

        #endregion
        
    }
    
    public class CharIdleState : State<Character>
    {
        private static CharIdleState _mInstance;
        public static CharIdleState Instance
        {
            get
            {
                if (_mInstance == null)
                    _mInstance = new CharIdleState();
                return _mInstance;
            }
        }

        public override void Enter(Character go)
        {
            go.CharIdleEnter();
        }

        public override void Execute(Character go)
        {
            go.CharIdleExecute();
        }

        public override void Exit(Character go)
        {
            go.CharIdleEnd();
        }
    }
}
