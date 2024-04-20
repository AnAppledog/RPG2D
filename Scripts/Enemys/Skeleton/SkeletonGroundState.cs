using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundState : EnemyState
{   
    public Skeleton enemy;
    public SkeletonGroundState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _enemyAnimName, Skeleton _enemy) : base(_enemyBase, _stateMachine, _enemyAnimName)
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

        // 检测到玩家
        if (enemy.IsPlayerDectected() || enemy.IsPlayerDectectedBack())
        {
            stateMachine.stateChange(enemy.BattleState);
        }
            
    }

    public override void Exit()
    {
        base.Exit();
    }
    
}
