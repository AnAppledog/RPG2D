using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float cooldownTime;  // 技能冷却时间
    protected float cooldownTimer; // 冷却计时器

    protected virtual void Update() 
    {
        if (cooldownTimer >= 0)
            cooldownTimer -= Time.deltaTime;
    } 

    public virtual bool CanUseSkill()
    {
        if (cooldownTimer < 0)
        {
            UseSkill();
            cooldownTimer = cooldownTime;
            return true;
        }
        
        Debug.Log("Skill is on cooldown");
        return false;
        
    }

    public virtual void UseSkill()
    {
        // 使用技能的能力
    }
}
