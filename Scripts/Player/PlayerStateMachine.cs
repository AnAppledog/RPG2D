using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateMa
{   
    public PlayerState CurrentState { get; private set;}      // 当前的状态对象 （后面大括号代表只读）
    public PlayerState LastState {get; private set; }         // 上一个状态对象

    public void Initialize(PlayerState _startState)      // 初始化函数
    {   
        this.CurrentState = _startState;                // 进入初始状态
        this.CurrentState.Enter();
    }

    public void StateChange(PlayerState _newstate)       // 状态转换函数
    {  
        this.LastState = this.CurrentState;             // 更新上一状态
        this.CurrentState.Exit();                       // 退出上一状态
        this.CurrentState = _newstate;                  // 更新当前状态
        this.CurrentState.Enter();                      // 绑定新的状态并进入
    }
}
