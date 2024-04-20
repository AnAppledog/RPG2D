using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Flash FX")]
    [SerializeField] private float flashDur;    // 受击特效持续时间
    [SerializeField] private Material hitMat;   // 受击材质
    private Material startMat;                  // 起始材质

    private void Start() 
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        startMat = sr.material;
    }
    
    private IEnumerator FlashFX()
    {
        sr.material = hitMat;

        yield return new WaitForSeconds(flashDur);

        sr.material = startMat;
    }
}
