namespace Possession 
{
	public class Player
	{
		private bool _available;

        public enum State { SWAPPING, CONTROLLING }

        private State state;

        public Player()
        {
            state = State.CONTROLLING;
            _available = true;
        }

        public State GetState()
        {
            return state;
        }

        public void SetState(State state)
        {
            this.state = state;
        }
    }
}
