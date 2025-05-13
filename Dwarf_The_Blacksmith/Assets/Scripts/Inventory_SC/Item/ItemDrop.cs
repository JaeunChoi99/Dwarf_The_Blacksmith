using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{

    [SerializeField] private int maxItemToDrop;
    [SerializeField] private ItemData[] itemPool;
    private List<ItemData> possibleDrop = new List<ItemData>();

    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private ItemData item;



    public void GenerateDrop()
    {
       if(itemPool.Length == 0)
        {
            Debug.Log("������ Ǯ�� ����ִ�!!!!! ������ ��� �Ұ�!!!!");
            return;
        }

       foreach(ItemData item in itemPool)
        {
            if(item != null && Random.Range(0,100) < item.dropChance)
            {
                possibleDrop.Add(item);
            }
        }

       for(int i = 0; i < maxItemToDrop; i++) 
       {
            if(possibleDrop.Count > 0)
            {
                int randomIndex = Random.Range(0, possibleDrop.Count);
                ItemData itemToDrop = possibleDrop[randomIndex];

                DropItem(itemToDrop);
                possibleDrop.Remove(itemToDrop);
            }
       }
    }

    public void DropItem(ItemData _itemData)
    {
        AudioManager.instance.PlaySFX(2, null);
        GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);

        Vector2 randomVelocity = new Vector2(Random.Range(-6, 6), Random.Range(13, 23));

        newDrop.GetComponent<ItemObject>().SetupItem(_itemData, randomVelocity);
    }
}
