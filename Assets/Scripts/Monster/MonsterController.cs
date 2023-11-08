using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int currentHealth = 200;
    [SerializeField]
    private int maxHealth = 200;

    [SerializeField]
    public int monsterDamage = 50;

    [SerializeField]
    private int monsterExp = 50;

    [SerializeField]
    private BarController healthBar;

    public GameObject destroyEffect;
    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.setMaxValue(maxHealth);
            healthBar.setValue(currentHealth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (healthBar != null)
        {
            healthBar.setValue(currentHealth);
        }
        if (currentHealth <= 0)
        {
            Destroy(gameObject);

            // Update the "MonsterDestroyed" count for all MonsterSpawner scripts
            //MonsterSpawner[] spawners = FindObjectsOfType<MonsterSpawner>();
            //foreach (var spawner in spawners)
            //{
            //    int currentMonsterDestroyed = PlayerPrefs.GetInt("MonsterDestroyed", 0);
            //    PlayerPrefs.SetInt("MonsterDestroyed", currentMonsterDestroyed + 1);
            //}
            int currentMonsterDestroyed = PlayerPrefs.GetInt("MonsterDestroyed", 0);
            PlayerPrefs.SetInt("MonsterDestroyed", currentMonsterDestroyed + 1);

            GameObject effect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.35f); 
            GetComponent<LootBag>().InstantiateLoot(transform.position, monsterExp);
            int totalMonsterDeytroyed = PlayerPrefs.GetInt("TotalMonsterDestroyed", 0);
            PlayerPrefs.SetInt("TotalMonsterDestroyed", totalMonsterDeytroyed + 1);
        }
    }
}
