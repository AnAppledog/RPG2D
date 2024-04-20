using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentState {get; private set;}
    
    // 初始化状态
    public void Initialize(EnemyState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    // 状态转移
    public void stateChange(EnemyState _newState)
    {   
        // 先退出上一状态 再进入新状态
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();   
    }
}
