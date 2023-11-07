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


    private int randomIndex;
    private int randomSide;

    [SerializeField]
    private int CheckPointTime = 10;

    private int totalTime = 0;
    float time = 0;
    bool fast = true;

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
        time += Time.deltaTime;
        if (time > 2)
        {
            time = 0;
            totalTime += 1;
        }

        if (totalTime == CheckPointTime)
        {
            fast = true;
        }
    }

    public void Reset()
    {
        fast = false;
        time = 0;
        //Destroy(spawnedMonster);
    }
}
