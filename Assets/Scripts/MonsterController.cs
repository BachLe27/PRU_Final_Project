using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour
{
    public int maxHealth = 20;
    public int currentHealth;
    public float damage = 5;
    [SerializeField] FloatingHealthBar healthBar;
    public GameObject goldPrefab;
    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Instantiate(goldPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
