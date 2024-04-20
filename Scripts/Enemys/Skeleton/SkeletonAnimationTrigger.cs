using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationTrigger : MonoBehaviour
{
    private Skeleton enemy => GetComponentInParent<Skeleton>();

    // 动画结束触发
    private void AnimationFinishTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    // 触发造成伤害
    private void AttackDamageTrigger()  
    {   
        // 获取范围内的所有碰撞器对象
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (var hit in colliders)  
        {   // 对于所有击中的目标
            if (hit.GetComponent<Player>() != null)
                hit.GetComponent<Player>().Damage();
        }
    }

    /*在敌人的攻击的某一帧动画中启动反击检测 在某一帧关闭*/
    /*启动检测时即可被反击（弹反）*/
    // 反击检测的启动
    private void StartCounter()
    {
        enemy.canBeStun = true;
    }
    
    // 反击检测的关闭
    private void CloseCounter()
    {
        enemy.canBeStun = false;
    }
}
