using _Game.Scripts.AnimationController;
using _Game.Scripts.Character.StateMachine;
using _Game.Scripts.Objects;
using _Game.Scripts.Objects.ActiveObject;
using _Game.Scripts.ScriptableObject;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Character
{
    public class Character : MonoBehaviour
    {
        public StateMachine<Character> StateMachine => _mStateMachine;
        private StateMachine<Character> _mStateMachine;

        public NavMeshAgent cAgent;
        public Transform cTarget;
        public CharacterAnimController cAnimController;

        private UnityAction actionMoveCallback;
        
        private void Awake()
        {
            _mStateMachine = new StateMachine<Character>(this);
            _mStateMachine.SetGlobalState(GlobalBotState.Instance);
            _mStateMachine.SetCurrentState(GlobalBotState.Instance);
            OnIdle();
        }

        public void Update()
        {
            StateMachine.Update();
        }

        public void ChangeState(State<Character> newState)
        {
            StateMachine.ChangeState(newState);
        }
        
        [SerializeField] private float timeAnimSetting = 5f;
        [SerializeField] private float timeAnim;
        
        #region Character Idle
        public void CharIdleEnter()
        {
            Debug.Log("Idle Enter");
            cAnimController.PlayAnim(AnimCharacterLayer.Idle, Random.Range(0, 5));
            timeAnimSetting = cAnimController.GetCurrentTimeAnimation();
            timeAnim = 0;
        }
        
        public void CharIdleExecute()
        {
            if (timeAnim < timeAnimSetting)
                timeAnim += Time.deltaTime;
            else
            {
                ChangeState(CharIdleState.Instance);
            }
        }

        public void CharIdleEnd()
        {
            
        }

        #endregion
        
        #region Character Move
        public void CharMoveEnter()
        {
            if (!cTarget)
            {
                OnIdle();
                return;
            }
            var distance = Vector3.Distance(transform.position, cTarget.position);
            if (distance > cAgent.stoppingDistance)
            {
                cAnimController.PlayAnim(AnimCharacterLayer.Move, 0);
                cAgent.enabled = true;
                cAgent.SetDestination(cTarget.position);
            }else OnIdle();
        }
        
        public void CharMoveExecute()
        {
            if (CheckMoveDone())
            {
                actionMoveCallback?.Invoke();
                if (actionMoveCallback == null)
                    OnIdle();
            }
        }

        public void CharMoveEnd()
        {
            
        }
        #endregion
        
        #region  Work on Object
        
        [BoxGroup("Work on Object")] public NoneStaticObj currentObjWorkOn; 
        [BoxGroup("Work on Object")] public AnimCharBObjConfig animData;
        
        public virtual void OnObjWorkEnter()
        {
            Debug.Log("OnObjWorkEnter");
            if (!currentObjWorkOn)
            {
                OnIdle();
                return;
            }
            animData = AnimCharBObj.Instance.GetData(currentObjWorkOn.GetObjData().animWorkID);
            cAnimController.PlayAnim(AnimCharacterLayer.Idle, 0);
            currentObjWorkOn.PlayAnim(animData);
            transform.DORotate(currentObjWorkOn.transform.eulerAngles, 1f);
            DOVirtual.DelayedCall(0.5f, () =>
            {
                cAnimController.PlayAnim(animData.cLayer, animData.animID);
            });
            
            timeAnimSetting = cAnimController.GetCurrentTimeAnimation();
            timeAnim = 0;
        }

        public virtual void OnObjWorkExecute()
        {
            if (timeAnim < timeAnimSetting)
                timeAnim += Time.deltaTime;
            else
            {
                cAnimController.PlayAnim(animData.cLayer, Random.Range(0, 2));
                timeAnimSetting = cAnimController.GetCurrentTimeAnimation();
                timeAnim = 0f;
            }
        }

        public virtual void OnObjWorkExit()
        {
            currentObjWorkOn = null;
        }

        #endregion

        private bool CheckMoveDone()
        {
            if (!cAgent.enabled) return false;
            return cAgent.remainingDistance <= cAgent.stoppingDistance;
        }
        
        private void SetActionCallback(UnityAction callback)
        {
            actionMoveCallback = callback;
        }

        [Button]
        public void MoveToObjAndWork(NoneStaticObj obj)
        {
            currentObjWorkOn = obj;
            MoveTarget(currentObjWorkOn.trsPointCharStand, OnObjWork);
        }

        private void OnObjWork()
        {
            timeAnim = 0;
            ChangeState(CharWorkOnObjState.Instance);
        }

        private void MoveTarget(Transform target, UnityAction actionCallBack)
        {
            cTarget = target;
            if (actionCallBack != null)
                SetActionCallback(actionCallBack);
            ChangeState(CharMoveState.Instance);
        }

        private void OnIdle()
        {
            timeAnim = 0;
            ChangeState(CharIdleState.Instance);
        }
    
    }
    
    public class CharIdleState : State<Character>
    {
        private static CharIdleState mInstance;
        public static CharIdleState Instance
        {
            get
            {
                if (mInstance == null)
                    mInstance = new CharIdleState();
                return mInstance;
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

    public class CharMoveState : State<Character>
    {
        private static CharMoveState mInstance;
        public static CharMoveState Instance
        {
            get
            {
                if (mInstance == null)
                    mInstance = new CharMoveState();
                return mInstance;
            }
        }

        public override void Enter(Character go)
        {
            go.CharMoveEnter();
        }

        public override void Execute(Character go)
        {
            go.CharMoveExecute();
        }

        public override void Exit(Character go)
        {
            go.CharMoveEnd();
        }
    }
    
    public class CharWorkOnObjState : State<Character>
    {
        private static CharWorkOnObjState mInstance;
        public static CharWorkOnObjState Instance
        {
            get
            {
                if (mInstance == null)
                    mInstance = new CharWorkOnObjState();
                return mInstance;
            }
        }

        public override void Enter(Character go)
        {
            go.OnObjWorkEnter();
        }

        public override void Execute(Character go)
        {
            go.OnObjWorkExecute();
        }

        public override void Exit(Character go)
        {
            go.OnObjWorkExit();
        }
    }
}
