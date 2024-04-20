using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : SkeletonGroundState
{
    public SkeletonIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _enemyAnimName, Skeleton _enemy) : base(_enemyBase, _stateMachine, _enemyAnimName, _enemy)
    {
    }

    public override void Enter() 
    {
        base.Enter();

        // 速度置为0
        enemy.SetZeroVelocity();
        // 记录时间 时间一到立即移动
        stateTimer = enemy.idleTime;
    }

    public override void Update()
    {
        base.Update();

        // 时间到自动移动
        if (stateTimer < 0)
            stateMachine.stateChange(enemy.MoveState);
    }

    public override void Exit()
    {
        base.Exit();

        if (!enemy.IsGroundDetected())
            enemy.Flip();
    }
}
