using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*玩家冲刺状态*/
public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMa _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        /* 冲刺持续时间 一般为0.2秒 */
        stateTimer = player.dashDuration;

        /* 如果是一段冲刺 设置二段跳检测时间为0.5秒 */
        if (!player.isDash2)
            player.dash2CheckTimer = player.dash2CheckTime;   

        /* 打断连击 */
        player.attackCombo = 0;
    }

    public override void Update()
    {
        base.Update();

        /* 更新冲刺速度 */
        player.SetVelocity(player.dashSpeed * player.dashDir, 0);

        /* 空中冲刺遇到墙体 进入滑墙状态*/
        if (!player.IsGroundDetected() && player.IsWallDetectedUp() && player.IsWallDetectedDown())
            stateMachine.StateChange(player.WallSlideState);
            
        /* 冲刺时间结束 */
        if (stateTimer < 0)             
            stateMachine.StateChange(player.IdleState);

    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0, rb.velocity.y);
    }
}
