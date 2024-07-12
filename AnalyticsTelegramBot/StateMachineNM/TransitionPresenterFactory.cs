using AnalyticsTelegramBot.StateMachineNM.ConditionsNM;
using TelegramBotTemplate.StateMachineNM.State;
using TelegramBotTemplate.StateMachineNM.TransitionNM;
using TelegramBotTemplate.StateMachineNM.TransitionNM.Condition;

namespace AnalyticsTelegramBot.StateMachineNM;

public class TransitionPresenterFactory
{
    public TransitionsPresenter Create(BotStates states, Conditions conditions)
    {
        TransitionsPresenter transitions = new();

        ConnectActiveStatesToCommandsListener(transitions, states, conditions.AnyCommandEntered);
        AddCommonStates(transitions, states, conditions);
        AddAdminStates(transitions, states, conditions);
        AddUserStates(transitions, states, conditions);
        ConnectDeadEndsToRoot(transitions, states);

        return transitions;
    }

    private void AddCommonStates(TransitionsPresenter transitions, BotStates states, Conditions conditions)
    {
        transitions.Add(states.CommandsListener, states.Start, conditions.Start);
        transitions.Add(states.CommandsListener, states.Stop, conditions.Stop);
    }

    private void AddAdminStates(TransitionsPresenter transitions, BotStates states, Conditions conditions)
    {
        transitions.Add(states.CommandsListener, states.Admin, conditions.IsAdmin);
        transitions.Add(states.Admin, states.Messages.YouAreAlreadyAdmin, conditions.Authorize);
        transitions.Add(states.Admin, states.Messages.TelegramUsersDump, conditions.TelegramUsersDump);
        transitions.Add(states.Admin, states.Messages.RecordsDump, conditions.RecordsDump);
        transitions.Add(states.Admin, states.Messages.GameUsersDump, conditions.GameUsersDump);
        transitions.Add(states.Admin, states.GenerateAverageStatistics, conditions.GenerateAverageStatistics);
        transitions.Add(states.Admin, states.Messages.EnterUserId, conditions.GenerateStatisticsByPlayer);
        transitions.Add(states.Messages.EnterUserId, states.PlayerIdInput, new True());
        transitions.Add(states.PlayerIdInput, states.GenerateStatisticsByPlayer, conditions.ValidPlayerId);
        transitions.Add(states.PlayerIdInput, states.Messages.InvalidPlayerId, new True());
        transitions.Add(states.Messages.InvalidPlayerId, states.PlayerIdInput, new True());
    }

    private void AddUserStates(TransitionsPresenter transitions, BotStates states, Conditions conditions)
    {
        transitions.Add(states.CommandsListener, states.User, conditions.IsUser);
        transitions.Add(states.User, states.Messages.YouAreNotAdmin, conditions.AdminCommand);
        transitions.Add(states.User, states.Messages.EnterPassword, conditions.Authorize);
        transitions.Add(states.Messages.EnterPassword, states.Authorization, new True());
        transitions.Add(states.Authorization, states.SetAdminRole, conditions.ValidAdminPassword);
        transitions.Add(states.Authorization, states.Messages.InvalidPassword, new True());
        transitions.Add(states.Messages.InvalidPassword, states.Authorization, new True());
    }

    private void ConnectDeadEndsToRoot(TransitionsPresenter transitions, BotStates states)
    {
        foreach (IState state in states.All)
        {
            if (transitions.IsDeadEnd(state))
            {
                transitions.Add(state, states.Root, new True());
            }
        }
    }

    private void ConnectActiveStatesToCommandsListener(TransitionsPresenter transitions, BotStates states, ICondition anyCommandEntered)
    {
        foreach (IState state in states.All)
        {
            if (state.IsPassive == false)
            {
                transitions.Add(state, states.CommandsListener, anyCommandEntered);
            }
        }
    }
}