using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*角色移动状态的类*/
public class PlayerMoveState : PlayerGroundState
{   
    public PlayerMoveState(Player _player, PlayerStateMa _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {   // 初始化函数 用父类PlayerState的数据初始化
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();  
        
        // 退出状态后不再更新速度
        if (player.StateMachine.CurrentState == player.MoveState)
            player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        // 如果x轴没有输入或者撞墙 则进入闲置状态
        if (xInput == 0 || player.IsWallDetectedUp() || player.IsWallDetectedDown())        
            stateMachine.StateChange(player.IdleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
    
}
