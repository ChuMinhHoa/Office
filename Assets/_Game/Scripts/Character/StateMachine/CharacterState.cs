namespace _Game.Scripts.Character.StateMachine
{
    #region State Machine
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
    #endregion
}