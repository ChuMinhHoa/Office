using _Game.Scripts.AnimationController;
using _Game.Scripts.Character.StateMachine;
using _Game.Scripts.Objects;
using _Game.Scripts.ScriptableObject;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Character
{
    public class Character : MonoBehaviour
    {
        public StateMachine<Character> StateMachine => _mStateMachine;
        private StateMachine<Character> _mStateMachine;

        [BoxGroup("CONTROLLER")] public NavMeshAgent cAgent;
        [BoxGroup("CONTROLLER")] public Transform cTarget;
        [BoxGroup("CONTROLLER")] public CharacterAnimController cAnimController;
        [BoxGroup("STATUS")] public CharState charState;

        private UnityAction _actionMoveCallback;

        private void Awake()
        {
            _mStateMachine = new StateMachine<Character>(this);
            _mStateMachine.SetGlobalState(GlobalBotState.Instance);
            _mStateMachine.SetCurrentState(GlobalBotState.Instance);
            Relax();
        }

        public void Update()
        {
            StateMachine.Update();
        }

        public void ChangeState(State<Character> newState)
        {
            StateMachine.ChangeState(newState);
        }

        [BoxGroup("STATUS")] [SerializeField] private float timeAnimSetting = 5f;
        [BoxGroup("STATUS")] [SerializeField] private float timeAnim;
        [BoxGroup("STATUS")] [SerializeField] private int animLoopCount = 0;
        [BoxGroup("STATUS")] [SerializeField] private int animLoopMax = 0;
        [BoxGroup("Work on Object")] public NoneStaticObj currentObjWorkOn;
        [BoxGroup("Work on Object")] public AnimCharBObjConfig animData;
        
        #region Character Idle

        public void CharIdleEnter()
        {
            cAnimController.PlayAnim(AnimCharacterLayer.Idle, GetAnimId(cAnimController.currentAnimLayer, charState));
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
            }
            else OnIdle();
        }

        public void CharMoveExecute()
        {
            if (CheckMoveDone())
            {
                _actionMoveCallback?.Invoke();
                if (_actionMoveCallback == null)
                    OnIdle();
            }
        }

        public void CharMoveEnd()
        {

        }
        
        private bool CheckMoveDone()
        {
            if (!cAgent.enabled) return false;
            return cAgent.remainingDistance <= cAgent.stoppingDistance;
        }
        
        [Button]
        public void MoveToObjAndWork(NoneStaticObj obj)
        {
            currentObjWorkOn = obj;
            MoveTarget(currentObjWorkOn.trsPointCharStand, OnObjWork);
        }
        
        private void MoveTarget(Transform target, UnityAction actionCallBack = null)
        {
            cTarget = target;
            if (actionCallBack != null)
                SetActionCallback(actionCallBack);
            ChangeState(CharMoveState.Instance);
        }
        
        private void SetActionCallback(UnityAction callback)
        {
            _actionMoveCallback = callback;
        }
        #endregion
       
        #region Work on Object

        

        public virtual void OnObjWorkEnter()
        {
            if (!currentObjWorkOn)
            {
                OnIdle();
                return;
            }

            animData = AnimCharBObj.Instance.GetData(currentObjWorkOn.GetObjData().animWorkID);
            cAnimController.PlayAnim(AnimCharacterLayer.Idle, 0);
            currentObjWorkOn.OnWork(animData);
           
            transform.DORotate(currentObjWorkOn.transform.eulerAngles, 1f);
            DOVirtual.DelayedCall(0.5f, () =>
            {
                cAnimController.PlayAnim(animData.cLayer, animData.animID);
                ChangeCharState(CharState.Work);
                timeAnimSetting = cAnimController.GetCurrentTimeAnimation();
            });
            
            timeAnim = 0;
        }

        public virtual void OnObjWorkExecute()
        {
            if (timeAnim < timeAnimSetting)
                timeAnim += Time.deltaTime;
            else
            {
                cAnimController.PlayAnim(animData.cLayer, GetAnimId(animData.cLayer, charState));
                timeAnimSetting = cAnimController.GetCurrentTimeAnimation();
                timeAnim = 0f;
            }
        }

        public virtual void OnObjWorkExit()
        {
            currentObjWorkOn.OutWork();
            currentObjWorkOn = null;
        }

        #endregion
        
        private void ChangeCharState(CharState newState)
        {
            charState = newState;
            animLoopMax =
                AnimationTimeLineGlobalConfig.Instance.GetAnimLoopMax(cAnimController.currentAnimLayer, charState);
        }

        private int GetAnimId(AnimCharacterLayer layer, CharState state)
        {
            animLoopCount++;
            var isOtherBase = animLoopCount > animLoopMax;
            animLoopCount = animLoopCount > animLoopMax ? 0 : animLoopCount;
            Debug.Log("Get other");
            return AnimationTimeLineGlobalConfig.Instance.GetAnimID(layer, state, isOtherBase);
        }

        private void OnObjWork()
        {
            timeAnim = 0;
            ChangeState(CharWorkOnObjState.Instance);
        }
        
        private void OnIdle()
        {
            timeAnim = 0;
            ChangeState(CharIdleState.Instance);
        }

        [Button]
        public void Relax()
        {
            ChangeCharState(CharState.Relax);
            OnIdle();
        }
    }
}
