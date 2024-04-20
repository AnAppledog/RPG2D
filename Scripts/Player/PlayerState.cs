using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState
{   
    protected float xInput;     // 记录x轴输入
    protected float stateTimer; // 记录时间

    protected Player player;
    protected PlayerStateMa stateMachine;
    private string playerAnimName;
    protected Rigidbody2D rb;
    protected bool triggerCalled;

    public PlayerState(Player _player, PlayerStateMa _stateMachine, string _animName)   // 构造函数
    {
        this.player = _player;                  // 初始化玩家对象
        this.stateMachine = _stateMachine;      // 初始化状态机对象
        this.playerAnimName = _animName;        // 初始化动画师名称
    }

    public virtual void Enter()
    {
        player.Anim.SetBool(playerAnimName, true);  // 进入状态 将该状态动画师名称设置为true
        rb = player.Rb;                             // 获取玩家的刚体

        triggerCalled = false;
    }

    public virtual void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        player.Anim.SetFloat("yVelocity", rb.velocity.y);

        if (stateTimer > 0)                         // 每一帧都更新时间 
            stateTimer -= Time.deltaTime;
    }

    public virtual void Exit() 
    {
        player.Anim.SetBool(playerAnimName, false);  // 退出状态 将该状态动画师名称设置为false
    }

    public virtual void AnimationFinishTrigger()    // 动画结束时调用
    {
        triggerCalled = true;
    }
}
