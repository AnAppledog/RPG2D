using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*用于获取Player组件的脚本*/
/*用于替代GameObject.Find()*/
/*优化性能*/
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;   // 单一实例 即该脚本只会作用于唯一一个组件
    public Player player;
    
    private void Awake() 
    {   // 仅保留第一个使用该脚本的实例（组件）
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }
}
