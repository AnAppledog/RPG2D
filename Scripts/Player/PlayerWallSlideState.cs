using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{

    private float slideSpeed;

    public PlayerWallSlideState(Player _player, PlayerStateMa _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // 初始化下滑速度
        slideSpeed = player.wallSlideSpeed;

        // 打断连击
        player.attackCombo = 0;
    }

    public override void Update()
    {
        base.Update();

        // 按下方向键 加快下落
        if (Input.GetKeyDown(KeyCode.S))
            slideSpeed = player.fastWallSlideSpeed;

        // 下落速度
        player.SetVelocity(0, -slideSpeed);

        // 到地面退出滑墙状态
        if (player.IsGroundDetected())
        {
            stateMachine.StateChange(player.IdleState);
            return;
        }
            
        
        // 反方向移动键 退出滑墙状态 并且无法进行二段跳
        // 滑到不能再抓墙 也退出滑墙状态
        if ((xInput != 0 && xInput != player.faceDir) || !player.IsWallDetectedDown()) {
            player.afterJump2 = true;
            stateMachine.StateChange(player.AirState);
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
            stateMachine.StateChange(player.WallJumpState);
        
    }

    public override void Exit()
    {
        base.Exit();
    }
}
