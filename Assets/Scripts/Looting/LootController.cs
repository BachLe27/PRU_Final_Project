using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{
    // Start is called before the first frame update
    public int value = 50;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log(gameObject.tag);
            var player = collision.gameObject.GetComponent<Player>();
            if (gameObject.CompareTag("XP"))
            {
                player.EarnXP(value);
            } else
            {
                player.EarnHP(200);
            }
            Destroy(gameObject);
        }
    }
}
