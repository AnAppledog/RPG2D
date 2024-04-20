using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Entity : MonoBehaviour
{   
    /*组件*/
    #region Compents
    public Animator Anim {get; private set; }   // 动画师组件
    public Rigidbody2D Rb {get; private set; }  // 刚体组件
    public EntityFX Fx {get; private set; }     
    #endregion
    /*受击反馈*/
    #region KnockBack
    [Header("KnockBack")]
    [SerializeField] private Vector2 knockBackVelo;  // 受击时移动的速度
    [SerializeField] private float knockBackTime;   // 持续时间
    public float knockDir;     // 受击方向 1代表被从左向右攻击 -1代表被从右向左攻击 （与面向方向相同时 则为背后攻击）
    public bool willBeKnock;   // 是否会硬直（打断攻击）
    private bool isKnocked;
    #endregion
    /*碰撞检测信息*/
    #region Collision
    [Header("Collision info")]
    public Transform attackCheck;   // 攻击检测
    public float attackCheckRadius; // 攻击检测的范围（半径）
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDis;
    [SerializeField] private Transform wallCheckUp;
    [SerializeField] private Transform wallCheckDown;
    [SerializeField] private float wallCheckDis;
    [SerializeField] private LayerMask whatIsGround;
    #endregion
    /*朝向信息*/
    #region Toward
    public float faceDir = 1;           // 初始化为1 代表朝向右侧   为-1则代表朝向左侧
    [SerializeField]private bool isFacingRight = true;  // 是否朝向右侧
    #endregion

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        Anim = GetComponentInChildren<Animator>();  // 获取动画师组件
        Rb = GetComponent<Rigidbody2D>();           // 获取刚体组件
        Fx = GetComponent<EntityFX>();
    }

    protected virtual void Update()
    {
        
    }

    #region Attack
    // 攻击造成伤害
    public virtual void Damage()
    {   
        Fx.StartCoroutine("FlashFX");   // 受击模型变化反馈
        StartCoroutine("KnockBack");    // 受击击退反馈
        Debug.Log(gameObject.name + " was damaged.");
    }
    // 受击会被击退
    protected virtual IEnumerator KnockBack()
    {   
        isKnocked = true;
        Rb.velocity = new Vector2(knockBackVelo.x * knockDir, knockBackVelo.y); // 设置速度

        yield return new WaitForSeconds(knockBackTime);         // 持续时间

        isKnocked = false;
        SetZeroVelocity();
    }

    #endregion
    #region Velo
    // 速度置0
    public void SetZeroVelocity() 
    {   // 受击时不受影响
        if (isKnocked)
            return;

        SetVelocity(0, 0);
    }
    // 更新速度
    public void SetVelocity(float xVelo, float yVelo) 
    {   // 受击时不受影响
        if (isKnocked)
            return;

        Rb.velocity = new Vector2(xVelo, yVelo);

        // 判断是否需要翻转
        ControlFlip(xVelo);
    }
    #endregion
    #region Flip
    public virtual void Flip() 
    {
        faceDir *= -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);        // 翻转y轴即可
    }
    // 控制翻转的函数
    public virtual void ControlFlip(float _x) 
    {
        if (_x > 0 && !isFacingRight)       // 速度向右但朝向左
            Flip();
        else if (_x < 0 && isFacingRight)   // 速度向左但朝向右
            Flip();
    }
    #endregion 
    #region Collision
    // 地面检测
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDis, whatIsGround);
    public bool IsWallDetectedUp() => Physics2D.Raycast(wallCheckUp.position, Vector2.right * faceDir, wallCheckDis, whatIsGround);
    public bool IsWallDetectedDown() => Physics2D.Raycast(wallCheckDown.position, Vector2.right * faceDir, wallCheckDis, whatIsGround);
    
    // 绘制碰撞检测线 便于调试
    protected virtual void OnDrawGizmos() 
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDis));
        Gizmos.DrawLine(wallCheckUp.position, new Vector3(wallCheckUp.position.x + wallCheckDis * faceDir, wallCheckUp.position.y));
        Gizmos.DrawLine(wallCheckDown.position, new Vector3(wallCheckDown.position.x + wallCheckDis * faceDir, wallCheckDown.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion

}
