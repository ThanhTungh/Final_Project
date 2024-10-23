using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private string initialStateID;

    [Header("States")]
    [SerializeField] private FSMState[] states;

    public FSMState CurrentState { get; set; }


    // Start is called before the first frame update
    private void Start()
    {
        ChangeState(initialStateID);
    }

    private void Update() 
    {
        CurrentState.ExecuteState(this);
    }

    public void ChangeState(string newStateID)
    {
        FSMState newState = GetState(newStateID);
        if (newState == null) return;
        CurrentState = newState;
    }

    private FSMState GetState(string stateID)
    {
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].StateID == stateID)
            {
                return states[i];
            }
        }
        return null;
    }
}
