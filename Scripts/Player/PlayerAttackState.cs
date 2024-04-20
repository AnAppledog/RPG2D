using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{   
    public PlayerAttackState(Player _player, PlayerStateMa _stateMachine, string _animName) : base(_player, _stateMachine, _animName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 0.15f;
        
        // 三连击之后重置     连击时间超时重置
        if (player.attackCombo > 2 || player.comboCheck < 0)
            player.attackCombo = 0;

        player.Anim.SetInteger("AttackCombo", player.attackCombo);

        player.SetVelocity(player.attackMovement[player.attackCombo].x * player.faceDir, player.attackMovement[player.attackCombo].y);
    }

    public override void Update()
    {
        base.Update();

        // 攻击时不能左右移动 
        // TODO（上下待定）
        if (stateTimer < 0)
            player.SetVelocity(0, rb.velocity.y);

        // 动画结束则退出状态
        if (triggerCalled)
            stateMachine.StateChange(player.IdleState);
    }

    public override void Exit()
    {
        base.Exit();

        player.attackCombo++;

        player.comboCheck = player.comboTime;
    }
}
