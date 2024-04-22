using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloneSkillControl : MonoBehaviour
{   
    [SerializeField] private float cloneLoseSpeed;          // 克隆消失的速度
    [SerializeField] private float cloneAttackCheckRadius;  // 克隆体攻击范围
    [SerializeField] private Transform cloneAttackCheck;    // 攻击检测中心
    private float cloneTimer;                       // 克隆计时器
    

    private SpriteRenderer sr;      // 着色器组件
    private Animator anim;          // 动画师组件

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update() 
    {   
        if (cloneTimer > 0)
            cloneTimer -= Time.deltaTime;

        // 跟随玩家进行普通攻击
        if (Input.GetKeyDown(KeyCode.J) && PlayerManager.instance.player.StateMachine.CurrentState == PlayerManager.instance.player.AttackState)  
        {   
            anim.SetBool("Attack", true);

            if (PlayerManager.instance.player.attackCombo > 2)
            {
                anim.SetInteger("AttackCombo", 0);
            }
            else
            {
                anim.SetInteger("AttackCombo", PlayerManager.instance.player.attackCombo);
            }
            
        }

        if (cloneTimer < 0)
        {   
            // 时间到则慢慢消逝（变透明）
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * cloneLoseSpeed));

            // 如果已经消逝了 则删除对象
            if (sr.color.a <= 0)
                Destroy(gameObject);
        }
    }

    public void SetupClone(Transform _newTransform, float _cloneTime)
    {   
        // 设置克隆体的位置
        transform.position = _newTransform.position;

        // 设置克隆体持续时间
        cloneTimer = _cloneTime;

        // 克隆与玩家方向相同
        if (PlayerManager.instance.player.faceDir == -1)
            transform.Rotate(0, 180, 0);
    }

    #region Trigger
    // 触发转变状态
    private void AnimationFinishTrigger() 
    {   // 动画结束退出攻击状态
        anim.SetBool("Attack", false);   
    }

    // 触发造成伤害
    private void AttackDamageTrigger()  
    {   
        // 获取范围内的所有碰撞器对象
        Collider2D[] colliders = Physics2D.OverlapCircleAll(cloneAttackCheck.position, cloneAttackCheckRadius);

        foreach (var hit in colliders)  
        {   // 对于所有击中的目标
            if (hit.GetComponent<Enemy>() != null)
            {   
                // 确定受击的方向
                if (transform.position.x < hit.GetComponent<Enemy>().transform.position.x)
                    hit.GetComponent<Enemy>().knockDir = 1;             // 从左向右攻击
                else
                    hit.GetComponent<Enemy>().knockDir = -1;            // 从右向左攻击

                if (PlayerManager.instance.player.attackCombo == 2 || PlayerManager.instance.player.attackCombo == 3)    // 如果是第三次重攻击
                    hit.GetComponent<Enemy>().willBeKnock = true;

                hit.GetComponent<Enemy>().Damage();
            }
                
        }
    }
    #endregion
}
