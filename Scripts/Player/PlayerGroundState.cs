using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*玩家在地面上的状态（二级状态）*/
public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player _player, PlayerStateMa _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        // 空格进入跳跃状态
        if (Input.GetKeyDown(KeyCode.Space))
            stateMachine.StateChange(player.JumpState);

        // 坠落
        if (!player.IsGroundDetected())
            stateMachine.StateChange(player.AirState);

        // 攻击
        if (Input.GetKeyDown(KeyCode.J))  
            stateMachine.StateChange(player.AttackState);

        // 反击
        if (Input.GetKeyDown(KeyCode.K) && player.counterCooldownTimer < 0)  
            stateMachine.StateChange(player.CounterState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
