public class StateMachine
{
   public State state { get; private set; }
   
   public void Set(State newState, bool forceReset = false)
   {
      if (state == newState && !forceReset)
         return;

      if (state != null)
         state.ExitBranch();
      
      state = newState;
      state.Initialize();
      state.Enter();
   }

   public void Do()
   {
      if (state != null)
         state.DoBranch();
   }

   public void FixedDo()
   {
      if (state != null)
         state.FixedDoBranch();
   }

   public void Exit()
   {
      if (state != null)
         state.ExitBranch();

      state = null;
   }
}
