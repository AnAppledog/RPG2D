using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : EnemyState
{
    public Skeleton enemy;
    public SkeletonAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _enemyAnimName, Skeleton _enemy) : base(_enemyBase, _stateMachine, _enemyAnimName)
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

        // 攻击时不会移动
        enemy.SetZeroVelocity();

        // 触发器触发 回到战斗状态
        if (triggerCalled)
            stateMachine.stateChange(enemy.BattleState);
    }

    public override void Exit()
    {
        base.Exit();

        // 设置冷却时间
        enemy.attackCooldownTimer = enemy.attackCooldownTime;
    }
}
