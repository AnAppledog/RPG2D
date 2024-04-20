using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : SkeletonGroundState
{
    public SkeletonMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _enemyAnimName, Skeleton _enemy) : base(_enemyBase, _stateMachine, _enemyAnimName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.moveTime;
    }

    public override void Update()
    {
        base.Update();

        // 更新速度
        enemy.SetVelocity(enemy.moveSpeed * enemy.faceDir, enemy.Rb.velocity.y);

        #region Basic motion logic
        /* 前面没有地面 */
        if (!enemy.IsGroundDetected())
        {   
            if (stateTimer < 1.5f)        // 如果剩余运动时间小于1.5 则进入闲置状态（退出该状态时翻转）
                stateMachine.stateChange(enemy.IdleState);
            else                          // 否则直接翻转
                enemy.Flip();
        }   
        /* 撞墙翻转 并适当延长移动时间（如果有必要）*/
        if (enemy.IsWallDetectedDown() || enemy.IsWallDetectedUp())
        {
            enemy.Flip();
            
            if (stateTimer < 1)
                stateTimer = 1;
        }
        /* 时间结束 进入闲置状态 */
        if (stateTimer < 0)
            stateMachine.stateChange(enemy.IdleState);
        #endregion
        
    }

    public override void Exit()
    {
        base.Exit();
    }
}
