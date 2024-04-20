using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 蹬墙跳 */
public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMa _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // 进入跳跃状态 初始化给y轴方向一个速度（玩家跳跃能力） 
        player.SetVelocity(5 * -player.faceDir, 1.0f * player.jumpForce);
    }

    public override void Update()
    {
        base.Update();
        
        // 跳跃过程中前方检测到墙壁 进入滑墙状态
        if (player.IsWallDetectedUp() && player.IsWallDetectedDown()) 
            stateMachine.StateChange(player.WallSlideState);

        // 跳跃到最高点后 y轴速度小于0 进入滞空下落状态
        if (rb.velocity.y < 0)
            stateMachine.StateChange(player.AirState);
    }

    public override void Exit()
    {
        base.Exit();

        player.afterJump2 = true;
    }
}
