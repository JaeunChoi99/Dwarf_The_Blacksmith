using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item effect", menuName = "Data/Item effect")]
public class ItemEffect : ScriptableObject
{

    //����Ʈ ���� �Լ�
    public virtual void ExecuteEffect(Transform _enemyPosition)
    {
        //����Ʈ ����
        Debug.Log("����Ʈ �����!");
    }
}
