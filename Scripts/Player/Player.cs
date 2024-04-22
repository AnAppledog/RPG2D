using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{      
    public bool isBusy {get; private set;}  // 玩家是否处于忙碌状态
    public SkillManager skill {get; private set;}   // 技能管理

    #region Attack
    [Header("Attack info")]
    public float comboTime = 0.5f;      // 连击判定时间
    public float comboCheck;
    public int attackCombo = 0;         // 连击计数器
    public Vector2[] attackMovement;    // 不同攻击中的移动细节
    public float counterTime;           // 反击持续时间（短） 相当于按一下反击一下
    public float counterCooldownTime;   // 反击的冷却时间
    public float counterCooldownTimer = -1;  // 反击的计时器
    #endregion
    #region Move
    /*玩家移动信息*/
    [Header("Move info")]
    public float moveSpeed;     // 玩家移动速度
    public float wallSlideSpeed;    // 玩家滑墙时下降速度
    public float fastWallSlideSpeed;    // 玩家加速下滑后的下降速度
    public float jumpForce;     // 玩家的跳跃能力
    public bool afterJump2 = false;    // 是否经历过二段跳
    #endregion
    #region Dash
    /*玩家冲刺信息*/
    [Header("Dash info")]
    public float dashDuration;          // 玩家冲刺的持续时间
    public float dashSpeed;             // 玩家冲刺的速度   
    public float dash2CheckTime;        // 二段冲刺冷却时间
    public float dash2CheckTimer;       // 二段冲刺检测计时器
    public bool isDash2;                // 是否为二段冲刺
    public float dashDir {get; private set;}        // 冲刺方向
    #endregion

    #region PlayerState
    // 创建一个只读的状态机
    public PlayerStateMa StateMachine {get; private set; }  

    // 创建玩家的状态
    public PlayerIdleState IdleState {get; private set; }
    public PlayerMoveState MoveState {get; private set; }
    public PlayerJumpState JumpState {get; private set; }
    public PlayerJump2State Jump2State {get; private set; }
    public PlayerAirState  AirState  {get; private set; }
    public PlayerDashState DashState {get; private set; }
    public PlayerWallSlideState WallSlideState {get; private set; }
    public PlayerWallJumpState WallJumpState {get; private set; }
    public PlayerAttackState AttackState {get; private set; }   
    public PlayerCounterState CounterState {get; private set; }
    #endregion
   
    // 给类初始化 
    protected override void Awake() 
    {
        base.Awake();

        StateMachine =  new PlayerStateMa();

        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, "Jump");
        AirState  = new PlayerAirState (this, StateMachine, "Jump");
        Jump2State = new PlayerJump2State(this, StateMachine, "Jump");
        DashState = new PlayerDashState(this, StateMachine, "Dash");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, "WallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, "Jump");
        AttackState = new PlayerAttackState(this, StateMachine, "Attack");
        CounterState = new PlayerCounterState(this, StateMachine, "Counter");
    }

    // 初始化函数
    protected override void Start() 
    {   
        base.Start();  
        skill = SkillManager.instance;          // 获取技能管理组件
        StateMachine.Initialize(IdleState);     // 初始化为闲置状态
    }

    // 更新函数
    protected override void Update() 
    {   
        base.Update();
        StateMachine.CurrentState.Update();

        DashCheck();

        // 连击计时
        if (comboCheck >= 0)
            comboCheck -= Time.deltaTime;

        // 反击冷却计时
        if (counterCooldownTimer >= 0)
           counterCooldownTimer -= Time.deltaTime;
    }

    // 用于部分状态（如攻击）动画结束时终止状态
    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    
    // 冲刺检测
    private void DashCheck()
    {      
        // 前面有墙无法冲刺     攻击中无法冲刺
        if (IsWallDetectedDown() || StateMachine.CurrentState == AttackState)   
            return;

        // 二段冲刺判定
        if (dash2CheckTimer >= 0)
            dash2CheckTimer -= Time.deltaTime;

        /* 判断二段冲刺 */
        if (Input.GetKeyDown(KeyCode.LeftShift) && dash2CheckTimer > 0)
        {   
            /*进行二段冲刺*/
            isDash2 = true;
            dash2CheckTimer = -1.0f;

            /* 使用冲刺技能 并更新时间*/
            SkillManager.instance.dash.CanUseSkill();

            /* 获取冲刺方向 如果没输入则为角色朝向 */
            dashDir = Input.GetAxis("Horizontal");
            if (dashDir == 0)
                dashDir = faceDir;
            else if (dashDir < 0)
                dashDir = -1;
            else    
                dashDir = 1;

            StateMachine.StateChange(DashState);

            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill())
        {   
            /* 进行一段冲刺 */
            isDash2 = false;
            
            /* 获取冲刺方向 如果没输入则为角色朝向 */
            dashDir = Input.GetAxis("Horizontal");
            if (dashDir == 0)
                dashDir = faceDir;
            else if (dashDir < 0)
                dashDir = -1;
            else    
                dashDir = 1;

            StateMachine.StateChange(DashState);

            return;
        }
    }

    // 控制玩家Busy状态的集成器 作用是让玩家处于忙碌状态若干秒  调用player.StartCoroutine("BusyFor", 0.1f)
    public IEnumerable BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }

}
