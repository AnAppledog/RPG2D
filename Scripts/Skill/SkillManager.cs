using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*与PlayerManager相同*/
public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    
    #region skill
    public DashSkill dash {get; private set;}
    public CloneSkill clone {get; private set;}
    #endregion

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        dash = GetComponent<DashSkill>();
        clone = GetComponent<CloneSkill>();
    }
}
