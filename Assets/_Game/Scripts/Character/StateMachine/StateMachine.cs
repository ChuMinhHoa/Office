namespace _Game.Scripts.Character.StateMachine
{
    public class StateMachine<T>
    {
        //public StateMachineName stateMachineName;
        private readonly T _mOwner;
        private State<T> _mCurrentState;
        private State<T> _mPreviousState;
        private State<T> _mGlobalState;
        private State<T> _mNextState;

        public StateMachine(T owner)
        {
            _mOwner = owner;
            _mCurrentState = null;
            _mPreviousState = null;
            _mGlobalState = null;
        }

        public void SetCurrentState(State<T> s)
        {
            _mCurrentState = s;
        }

        public void SetGlobalState(State<T> s)
        {
            _mGlobalState = s;
        }

        public void ChangeState(State<T> newState)
        {
            _mPreviousState = _mCurrentState;
            _mCurrentState.Exit(_mOwner);
            _mCurrentState = newState;
            _mCurrentState.Enter(_mOwner);
        }

        public void Update()
        {
            _mGlobalState?.Execute(_mOwner);
            _mCurrentState?.Execute(_mOwner);
        }
    
        public State<T> CurrentState => _mCurrentState;
        public State<T> LastState => _mPreviousState;
    }

    public abstract class State<T>
    {
        public abstract void Enter(T go);
        public abstract void Execute(T go);
        public abstract void Exit(T go);
    }
    
    public class GlobalBotState : State<_Game.Scripts.Character.Character>
    {
        private static GlobalBotState _mInstance;
        public static GlobalBotState Instance
        {
            get
            {
                if (_mInstance == null)
                    _mInstance = new GlobalBotState();
                return _mInstance;
            }
        }

        public override void Enter(_Game.Scripts.Character.Character go)
        {
            
        }

        public override void Execute(_Game.Scripts.Character.Character go)
        {
            
        }

        public override void Exit(_Game.Scripts.Character.Character go)
        {
            
        }
    }
}
