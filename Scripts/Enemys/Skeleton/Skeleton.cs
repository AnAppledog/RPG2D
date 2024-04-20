using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{   
    [Header("Move")]
    public float moveSpeed;     // 移动速度
    public float moveTime;      // 移动时间
    public float idleTime;      // 闲置时间

    #region SkeletonState
    public SkeletonIdleState IdleState {get; private set;}
    public SkeletonMoveState MoveState {get; private set;}
    public SkeletonBattleState BattleState {get; private set;}
    public SkeletonAlertState AlertState {get; private set;}
    public SkeletonAttackState AttackState {get; private set;}
    public SkeletonKnockState KnockState {get; private set;}    
    public SkeletonStunState StunState {get; private set;}
    #endregion

    protected override void Awake() 
    {
        base.Awake();

        // 初始化状态
        IdleState = new SkeletonIdleState(this, StateMachine, "Idle", this);
        MoveState = new SkeletonMoveState(this, StateMachine, "Move", this);
        BattleState = new SkeletonBattleState(this, StateMachine, "Move", this);
        AttackState = new SkeletonAttackState(this, StateMachine, "Attack", this);
        AlertState = new SkeletonAlertState(this, StateMachine, "Idle", this);
        KnockState = new SkeletonKnockState(this, StateMachine, "Knock", this);
        StunState = new SkeletonStunState(this, StateMachine, "Stun", this);
    }

    protected override void Start()
    {
        base.Start();
        // 初始化为闲置状态
        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    /*受击*/
    public override void Damage()
    {
        base.Damage();

        // 如果受重攻击击 进入硬直状态
        if (willBeKnock)
        {
            StateMachine.stateChange(KnockState);
            willBeKnock = false;
        }
    }

    /*眩晕检测*/
    public override bool CanBeStun()
    {   // 如果能被眩晕 则进入眩晕状态 并返回真
        if (base.CanBeStun())
        {
            StateMachine.stateChange(StunState);
            return true;
        }
        return false;
    }
}
