using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int health = 200;
    [SerializeField]
    private int maxHealth = 200;
    [SerializeField]
    public int damage = 20;
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Take Damage!");
        health -= damage;
        if (health < 0)
        {
            Destroy(gameObject);
            int currentMonsterDestroyed = PlayerPrefs.GetInt("MonsterDeytroyed", 0);
            PlayerPrefs.SetInt("MonsterDeytroyed", currentMonsterDestroyed + 1);
        }
    }
}
