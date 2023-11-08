using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<Loot> lootList = new List<Loot>();
    // Start is called before the first frame update

    Loot GetDroppedItems()
    {
        int randomNumber = Random.Range(1, 101); // 1-100
        List<Loot> possibleItems = new List<Loot>();
        foreach (Loot item in lootList)
        {
            if (randomNumber <= item.dropChange)
            { 
                possibleItems.Add(item);
            }
        }

        if (possibleItems.Count > 0)
        {
            Loot dropItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return dropItem;
        }
        return null;
    }

    public void InstantiateLoot(Vector3 spawnPosition, int ExpValue)
    {
        Loot droppedItem = GetDroppedItems();
        if (droppedItem != null)
        {
            GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;
            lootGameObject.tag = droppedItem.name;
            float dropForce = 3f;
            Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
            lootGameObject.GetComponent<LootController>().value = ExpValue;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
