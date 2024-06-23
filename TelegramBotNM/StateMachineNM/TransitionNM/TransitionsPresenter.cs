using TelegramBotNM.StateMachineNM.TransitionNM.Condition;

namespace TelegramBotNM.StateMachineNM.TransitionNM
{
    public class TransitionsPresenter
    {
        private readonly Dictionary<IState, List<Transition>> _transitions = new();

        public void Add(IState from, IState to, ICondition condition)
        {
            if (_transitions.ContainsKey(from) == false)
            {
                _transitions.Add(from, new List<Transition>());
            }

            _transitions[from].Add(new Transition(from, to, condition));
        }

        public IState Transit(IState state)
        {
            IState newState = state;

            if (_transitions.TryGetValue(newState, out List<Transition>? transitions))
            {
                foreach (Transition transition in transitions)
                {
                    if (transition.Condition.IsTrue())
                    {
                        newState = transition.StateTo;
                        break;
                    }
                }
            }

            return newState;
        }
    }
}