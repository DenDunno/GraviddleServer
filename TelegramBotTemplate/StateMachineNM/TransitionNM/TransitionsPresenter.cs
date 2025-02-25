using TelegramBotTemplate.StateMachineNM.State;
using TelegramBotTemplate.StateMachineNM.TransitionNM.Condition;

namespace TelegramBotTemplate.StateMachineNM.TransitionNM
{
    public class TransitionsPresenter
    {
        private readonly Dictionary<IState, List<Transition>> _transitions = new();

        public bool IsDeadEnd(IState state)
        {
            return _transitions.ContainsKey(state) == false;
        }
        
        public void Add(IState from, IState to, ICondition condition)
        {
            if (_transitions.ContainsKey(from) == false)
            {
                _transitions.Add(from, new List<Transition>());
            }

            _transitions[from].Add(new Transition(from, to, condition));
        }

        public async Task<IState> Transit(IState state)
        {
            IState newState = state;

            if (_transitions.TryGetValue(newState, out List<Transition>? transitions))
            {
                foreach (Transition transition in transitions)
                {
                    if (await transition.Condition.IsTrue())
                    {
                        newState = transition.End;
                        break;
                    }
                }
            }

            return newState;
        }
    }
}