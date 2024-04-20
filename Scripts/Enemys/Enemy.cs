using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{   
    /*攻击信息*/
    #region Attack
    [Header("Attack info")]
    [SerializeField] private Transform playerCheck;
    [SerializeField] private LayerMask whatIsPlayer;
    public float findPlayerDis;
    public float attackDistance;        // 攻击距离
    public float attackCooldownTime;    // 攻击冷却时间
    public float attackCooldownTimer;        // 攻击实时冷却时间
    public float AlertTime;             // 警戒时间
    #endregion
    
    /*眩晕信息*/
    #region Stun
    [Header("Stun info")]
    public float stunTime;      // 眩晕时长
    public Vector2 stunVelo;    // 被眩晕时的速度（眩晕会被击飞一段距离）
    public bool canBeStun;      // 是否可以被反击眩晕
    #endregion

    // 一个只读的状态机
    public EnemyStateMachine StateMachine {get; private set;}

    protected override void Awake() 
    {   
        base.Awake();
        StateMachine = new EnemyStateMachine();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {   
        base.Update();

        // 当前状态的更新函数
        StateMachine.currentState.Update();

        // 攻击冷却
        if (attackCooldownTimer > 0)
            attackCooldownTimer -= Time.deltaTime;
    }

    #region PlayerCheck
    public virtual RaycastHit2D IsPlayerDectected() => Physics2D.Raycast(playerCheck.position, Vector2.right * faceDir, findPlayerDis, whatIsPlayer);
    public virtual RaycastHit2D IsPlayerDectectedBack() => Physics2D.Raycast(playerCheck.position, Vector2.left * faceDir, 2.0f, whatIsPlayer);
    #endregion

    /*眩晕检测*/
    public virtual bool CanBeStun()
    {
        if (canBeStun)
        {
            canBeStun = false;
            return true;
        }
        return false;
    }
    public virtual void AnimationFinishTrigger() => StateMachine.currentState.AnimationFinishTrigger();

    protected override void OnDrawGizmos() 
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * faceDir, transform.position.y));
    }
}
