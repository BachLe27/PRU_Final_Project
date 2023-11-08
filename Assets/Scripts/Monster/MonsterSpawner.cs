using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject[] monsterReference;

    private GameObject spawnedMonster;
    private GameObject Boss;

    public int[] monsterCount;
    public int currentMonsterIndex = 0;


    void Start()
    {
        StartCoroutine(SpawnMonsters());
        PlayerPrefs.SetInt("MonsterDeytroyed", 0);
        PlayerPrefs.SetInt("CurrentMonsterNumber", 0);
    }

    IEnumerator SpawnMonsters()
    {
        while (true)
        {
            int currentMonsterNumber = PlayerPrefs.GetInt("CurrentMonsterNumber", 0);
            Debug.Log(currentMonsterNumber);
            if (currentMonsterNumber < monsterCount[currentMonsterIndex])
            {
                yield return new WaitForSeconds(2f);
                spawnedMonster = Instantiate(monsterReference[currentMonsterIndex]);
                Vector3 spawnerPos = gameObject.transform.position;
                spawnedMonster.transform.position = spawnerPos;
                PlayerPrefs.SetInt("CurrentMonsterNumber", currentMonsterNumber + 1);
            } else
            {
                yield return 0;
            }
        }
    }

    IEnumerator nextWave()
    {
        yield return new WaitForSeconds(15f);
        PlayerPrefs.SetInt("MonsterDeytroyed", 0);
        PlayerPrefs.SetInt("CurrentMonsterNumber", 0);
        currentMonsterIndex += 1;
    }

    // Update is called once per frame
    void Update()
    {
        int currentMonsterDestroyed = PlayerPrefs.GetInt("MonsterDeytroyed", 0);
        if (currentMonsterDestroyed == monsterCount[currentMonsterIndex])
        {
            PlayerPrefs.SetInt("MonsterDeytroyed", 0);
            PlayerPrefs.SetInt("CurrentMonsterNumber", 0);
            //if (currentMonsterIndex < monsterReference.Length - 1)
                currentMonsterIndex += 1;
        }
    }

    public void Reset()
    {
    }
}
