using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunState : EnemyState
{
    Skeleton enemy;
    public SkeletonStunState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _enemyAnimName, Skeleton _enemy) : base(_enemyBase, _stateMachine, _enemyAnimName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        
        // 更新眩晕时间
        stateTimer = enemy.stunTime;

        // 更新眩晕速度
        Rb.velocity = new Vector2(enemy.stunVelo.x * -enemy.faceDir, enemy.stunVelo.y);

    }

    public override void Update()
    {
        base.Update();

        // 眩晕结束进入闲置状态
        if (stateTimer < 0)
            stateMachine.stateChange(enemy.IdleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
