using UnityEngine;

public class MonsterController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int health = 200;
    [SerializeField]
    private int maxHealth = 200;
    [SerializeField]
    private GameObject gold;
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
        Debug.Log("Take Dame!");
        health -= damage;
        if (health < 0)
        {
            Destroy(gameObject);
            int currentMonsterDestroyed = PlayerPrefs.GetInt("MonsterDeytroyed", 0);
            Instantiate(gold, transform.position, Quaternion.identity);
            PlayerPrefs.SetInt("MonsterDeytroyed", currentMonsterDestroyed + 1);
        }
    }
}
