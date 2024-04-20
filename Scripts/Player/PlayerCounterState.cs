using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterState : PlayerState
{
    public PlayerCounterState(Player _player, PlayerStateMa _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {   // 初始化函数 用父类PlayerState的数据初始化
    }

    public override void Enter()
    {
        base.Enter();

        // 初始化持续时间
        stateTimer = player.counterTime;
        // 初始化 "反击成功" 标志为假
        player.Anim.SetBool("SuccessfulCounter", false);
    }

    public override void Update()
    {
        base.Update();  

        // 反击过程不能移动
        player.SetZeroVelocity();
        
        // 获取范围内的所有碰撞器对象 判断是否可以反击
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)  
        {   // 对于所有击中的目标
            if (hit.GetComponent<Enemy>() != null)
            {   
                if (hit.GetComponent<Enemy>().CanBeStun())
                {   // 如果可以被反击
                    stateTimer = 10;    // 重置计时器 确保不会提前退出状态
                    player.Anim.SetBool("SuccessfulCounter", true);     // 更新动画 进入反击成功动画
                }
            }
        }

        // 反击时间结束 或者反击成功动画结束 进入闲置状态
        if (stateTimer < 0 || triggerCalled)
            stateMachine.StateChange(player.IdleState);
    }

    public override void Exit()
    {
        base.Exit();

        player.counterCooldownTimer = player.counterCooldownTime;
    }
}
