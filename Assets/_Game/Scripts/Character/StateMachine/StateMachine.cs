namespace _Game.Scripts.Character.StateMachine
{
    public class StateMachine<T>
    {
        //public StateMachineName stateMachineName;
        private readonly T mOwner;
        private State<T> mCurrentState;
        private State<T> mPreviousState;
        private State<T> mGlobalState;
        private State<T> mNextState;

        public StateMachine(T owner)
        {
            mOwner = owner;
            mCurrentState = null;
            mPreviousState = null;
            mGlobalState = null;
        }

        public void SetCurrentState(State<T> s)
        {
            mCurrentState = s;
        }

        public void SetGlobalState(State<T> s)
        {
            mGlobalState = s;
        }

        public void ChangeState(State<T> newState)
        {
            mPreviousState = mCurrentState;
            mCurrentState.Exit(mOwner);
            mCurrentState = newState;
            mCurrentState.Enter(mOwner);
        }

        public void Update()
        {
            mGlobalState?.Execute(mOwner);
            mCurrentState?.Execute(mOwner);
        }
    
        public State<T> CurrentState => mCurrentState;
        public State<T> LastState => mPreviousState;
    }

    public abstract class State<T>
    {
        public abstract void Enter(T go);
        public abstract void Execute(T go);
        public abstract void Exit(T go);
    }
    
    public class GlobalBotState : State<Character>
    {
        private static GlobalBotState mInstance;
        public static GlobalBotState Instance => mInstance ??= new GlobalBotState();

        public override void Enter(Character go)
        {
            
        }

        public override void Execute(Character go)
        {
            
        }

        public override void Exit(Character go)
        {
            
        }
    }
}
