using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    // 触发转变状态
    private void AnimationFinishTrigger() 
    {
        player.AnimationFinishTrigger();
    }

    // 触发造成伤害
    private void AttackDamageTrigger()  
    {   
        // 获取范围内的所有碰撞器对象
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)  
        {   // 对于所有击中的目标
            if (hit.GetComponent<Enemy>() != null)
            {   
                // 确定受击的方向
                if (player.transform.position.x < hit.GetComponent<Enemy>().transform.position.x)
                    hit.GetComponent<Enemy>().knockDir = 1;             // 从左向右攻击
                else
                    hit.GetComponent<Enemy>().knockDir = -1;            // 从右向左攻击

                if (player.attackCombo == 2 || player.attackCombo == 3)    // 如果是第三次重攻击
                    hit.GetComponent<Enemy>().willBeKnock = true;

                hit.GetComponent<Enemy>().Damage();
            }
                
        }
    }
}
