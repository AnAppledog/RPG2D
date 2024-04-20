using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*玩家闲置状态*/
public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player _player, PlayerStateMa _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {   // 初始化函数 用父类PlayerState的数据初始化
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        // 如果x轴有输入并且没撞墙 则向前走 进入运动状态
        if (!player.IsWallDetectedUp() && !player.IsWallDetectedDown() && xInput == player.faceDir)        
            stateMachine.StateChange(player.MoveState);

        // 向后走 进入运动状态
        if (xInput == -player.faceDir)
            stateMachine.StateChange(player.MoveState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
