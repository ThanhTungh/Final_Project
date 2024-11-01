using System;

[Serializable]
public class FSMState
{
    public string StateID;
    public FSMAction[] Actions;
    public FSMTransition[] Transitions;

    public void ExecuteState(EnemyBrain enemy)
    {
        ExecuteActions();
        ExecuteTransitions(enemy);
    }

    private void ExecuteActions()
    {
        if (Actions.Length <= 0) return;
        for (int i = 0; i < Actions.Length; i++)
        {
            Actions[i].Act();
        }
    }

    private void ExecuteTransitions(EnemyBrain enemy)
    {
        if (Transitions.Length <= 0) return;
        for (int i = 0; i < Transitions.Length; i++)
        {
            bool value = Transitions[i].Decision.Decide();
            if (value)
            {
                if (string.IsNullOrEmpty(Transitions[i].TrueState) == false)
                {
                    enemy.ChangeState(Transitions[i].TrueState);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(Transitions[i].FalseState) == false)
                {
                    enemy.ChangeState(Transitions[i].FalseState);
                }
            }
        }
    }
}