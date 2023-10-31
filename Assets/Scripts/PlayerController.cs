using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int maxHealth = 500;      // player's limit health
    private int currentHealth;      // player's current heatlth
    private int totalGold = 0;      //Gold to upgrade weapons in shop
    public int attackDamage = 15;   // 
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Gold")) 
        {           
            totalGold++;           
            Destroy(collider.gameObject);
        }
    }
}
