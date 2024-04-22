using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*敌人发现玩家 但不能向玩家走去的状态*/
/*
    1. 前面有墙或者前面没有地面
       这种情况下，如果持续时长超过一段时间，会转换为闲置状态
    2. 玩家在攻击范围内，攻击正在冷却
        攻击冷却好 -> 攻击
        玩家超出攻击范围 -> 进入战斗移动状态
*/
public class SkeletonAlertState : EnemyState
{
    private Transform player;
    private float moveDir;      // 移动方向

    Skeleton enemy;
    public SkeletonAlertState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _enemyAnimName, Skeleton _enemy) : base(_enemyBase, _stateMachine, _enemyAnimName)
    {
        enemy = _enemy;
    }


    public override void Enter()
    {
        base.Enter();

        // 速度设置为0
        enemy.SetZeroVelocity();
        // 记录时间
        stateTimer = enemy.AlertTime;

        player = PlayerManager.instance.player.transform;
    }

    public override void Update()
    {
        base.Update();
        
        // 超出侦测距离则进入闲置状态
        if (Vector2.Distance(player.position, enemy.transform.position) > enemy.findPlayerDis + 3.0f)
        {
            stateMachine.stateChange(enemy.IdleState);
            return;
        }  
            
        // 情况1：
        if (!enemy.IsGroundDetected() || enemy.IsWallDetectedDown() || enemy.IsWallDetectedUp())
        {
            if (stateTimer < 0)
            {
                enemy.Flip();
                stateMachine.stateChange(enemy.MoveState);
                return;
            }
        }
        else    // 情况2：
        {   
            // 冷却时间到 攻击
            if (enemy.IsPlayerDectected() || enemy.IsPlayerDectectedBack())
            {
                if (enemy.IsPlayerDectected().distance < enemy.attackDistance && enemy.attackCooldownTimer <= 0)
                {   
                    stateMachine.stateChange(enemy.AttackState);
                    return;
                }    
                
            }
            // 超出攻击范围 追
            if (enemy.IsPlayerDectected().distance > enemy.attackDistance)
            {
                stateMachine.stateChange(enemy.BattleState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
