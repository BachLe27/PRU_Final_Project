using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject[] monsterReference;
    private GameObject spawnedMonster;

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
    }

    IEnumerator SpawnMonsters()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, fast ? 2 : 4));

            randomIndex = Random.Range(0, monsterReference.Length);

            Debug.Log(randomIndex);
            spawnedMonster = Instantiate(monsterReference[randomIndex]);

            Vector3 spawnerPos = GameObject.FindGameObjectWithTag("Spawner").transform.position;

            spawnedMonster.transform.position = spawnerPos;
        }

    }

    // Update is called once per frame
    void Update()
    {
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
        Destroy(spawnedMonster);
    }
}
