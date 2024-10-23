using UnityEngine;
using System;

[Serializable]
public class FSMTransition
{
    public FSMDecision Decision;
    public string TrueState;
    public string FalseState;
    
}