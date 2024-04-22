using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{   
    private Transform player;
    private float moveDir;      // 移动方向

    Skeleton enemy;
    public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _enemyAnimName, Skeleton _enemy) : base(_enemyBase, _stateMachine, _enemyAnimName)
    {
        enemy = _enemy;
    }


    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;
    }

    public override void Update()
    {
        base.Update();

        // 超出侦测距离则进入闲置状态
        if (Vector2.Distance(player.position, enemy.transform.position) > enemy.findPlayerDis + 1.0f)
        {
            stateMachine.stateChange(enemy.IdleState);
            return;
        }
            

        if (player.position.x > enemy.transform.position.x)  // 玩家在敌人右侧
            moveDir = 1;
        else if (player.position.x < enemy.transform.position.x) //玩家在敌人左侧
            moveDir = -1;

        // 适当提高速度向玩家走去
        enemy.SetVelocity(moveDir * enemy.moveSpeed * 1.2f, Rb.velocity.y);

        // 前面没有地面或者有墙壁 则停止
        if (!enemy.IsGroundDetected())
            stateMachine.stateChange(enemy.AlertState);
        if (enemy.IsWallDetectedDown() || enemy.IsWallDetectedUp())
            stateMachine.stateChange(enemy.AlertState);
            
        // 进入攻击距离进行攻击 且攻击不在冷却中
        if (enemy.IsPlayerDectected() || enemy.IsPlayerDectectedBack())
        {   
            // 进入攻击距离
            if (enemy.IsPlayerDectected().distance < enemy.attackDistance)
            {   
                if (enemy.attackCooldownTimer <= 0)
                {
                    stateMachine.stateChange(enemy.AttackState);
                    return;
                }
                else
                {
                    stateMachine.stateChange(enemy.AlertState);
                    return;
                }
            }
        }

    }

    public override void Exit()
    {
        base.Exit();
    }

}
