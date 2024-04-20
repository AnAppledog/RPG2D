using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*玩家在空中的状态（下落状态）*/
public class PlayerAirState : PlayerState
{   
    public PlayerAirState(Player _player, PlayerStateMa _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // 打断连击
        player.attackCombo = 0;
    }

    public override void Update()
    {
        base.Update();
        
        // 在空中仍可以左右运动 但不影响动画
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        // 下落过程中前方检测到墙壁 进入滑墙状态
        if (player.IsWallDetectedUp() && player.IsWallDetectedDown())
            stateMachine.StateChange(player.WallSlideState);

        // 如果未进行过二段跳 则按下空格二段跳
        if (!player.afterJump2 && Input.GetKeyDown(KeyCode.Space))      
            stateMachine.StateChange(player.Jump2State);

        // 落地
        if (player.IsGroundDetected()) {
            player.afterJump2 = false;      // 落地更新二段跳记录
            stateMachine.StateChange(player.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
