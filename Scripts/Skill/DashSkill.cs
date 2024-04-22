using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*角色冲刺时所附带的技能*/
/*冷却时间与冲刺一致*/
public class DashSkill : Skill
{
    public override bool CanUseSkill()
    {   
        if (cooldownTimer < 0)
        {
            UseSkill();
            cooldownTimer = cooldownTime;
            return true;
        }
        else if (PlayerManager.instance.player.isDash2) // 二段冲刺
        {   
            Debug.Log("Dash2");
            UseSkill();
            cooldownTimer = cooldownTime;
            return true;
        }
         
        Debug.Log("Skill is on cooldown");
        return false;
        
    }

    public override void UseSkill()
    {
        base.UseSkill();

        // 第一次冲刺 在玩家位置上留一个分身
        if (!PlayerManager.instance.player.isDash2)
            SkillManager.instance.clone.CreateClone(PlayerManager.instance.player.transform);   
    }
}
