using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item effect", menuName = "Data/Item effect")]
public class ItemEffect : ScriptableObject
{

    //¿Ã∆Â∆Æ Ω««‡ «‘ºˆ
    public virtual void ExecuteEffect(Transform _enemyPosition)
    {
        //¿Ã∆Â∆Æ Ω««‡
        Debug.Log("¿Ã∆Â∆Æ Ω««‡µ !");
    }
}
