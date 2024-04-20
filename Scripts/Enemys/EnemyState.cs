using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{   
    public Enemy enemyBase;
    public EnemyStateMachine stateMachine;
    private string enemyAnimName;
    public Rigidbody2D Rb;

    // 记录时间
    protected float stateTimer;
    // 状态触发器
    protected bool triggerCalled;

    // 初始化函数
    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _enemyAnimName)
    {
        enemyBase = _enemyBase;
        stateMachine = _stateMachine;
        enemyAnimName = _enemyAnimName;
    }

    public virtual void Enter()
    {
        triggerCalled = false;
        enemyBase.Anim.SetBool(enemyAnimName, true);  // 进入状态 将该状态动画师名称设置为true

        Rb = enemyBase.Rb;
    }

    public virtual void Update()
    {   
        // 不断更新时间 直到为0
        if (stateTimer > 0)
            stateTimer -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        triggerCalled = true;
        enemyBase.Anim.SetBool(enemyAnimName, false);  // 退出状态 将该状态动画师名称设置为false
    }

    // 状态动画及结束的触发器
    public void AnimationFinishTrigger() 
    {
        triggerCalled = true;
    }
}
