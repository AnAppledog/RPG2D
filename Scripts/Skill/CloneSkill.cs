using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkill : Skill
{   
    [Header("Clone info")]
    [SerializeField] private GameObject clonePrefab;    // 克隆的预制体
    [SerializeField] private float cloneTime;           // 克隆持续时间

    public void CreateClone(Transform _clonePosition)
    {   // 利用预制的克隆体创建一个临时克隆对象
        GameObject newclone = Instantiate(clonePrefab);

        // 设置克隆的位置 
        newclone.GetComponent<CloneSkillControl>().SetupClone(_clonePosition, cloneTime);
    }
}
