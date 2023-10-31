using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public int maxHealth = 30;
    private int currentHealth;
    public GameObject goldPrefab;
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
            Instantiate(goldPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }



}
