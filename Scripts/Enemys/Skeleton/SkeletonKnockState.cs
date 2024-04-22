using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonKnockState : EnemyState
{   
    public Skeleton enemy;
    public SkeletonKnockState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _enemyAnimName, Skeleton _enemy) : base(_enemyBase, _stateMachine, _enemyAnimName)
    {
        enemy = _enemy;
    }

    public override void Enter() 
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        // 动画结束 触发器触发进入警戒状态
        if (triggerCalled)
            stateMachine.stateChange(enemy.AlertState);
    }

    public override void Exit()
    {
        base.Exit();

        // 状态结束立刻适当减少攻击冷却时间
        if (enemy.attackCooldownTimer > 0.4f)
            enemy.attackCooldownTimer = 0.4f;
    }
}
