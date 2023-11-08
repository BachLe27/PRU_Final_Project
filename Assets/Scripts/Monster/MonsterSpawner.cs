using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MonsterInfo
{
    public GameObject monsterType;  // The type of monster for this wave
    public int monsterLimit;        // Limit for the number of monsters of this type
}

[System.Serializable]
public class Wave
{
    public List<MonsterInfo> monsters;  // List of monsters in the wave, each with its type and limit
}

public class MonsterSpawner : MonoBehaviour
{
    public List<Wave> waves;            // List of waves, each containing multiple monster types
    public Transform[] spawnPoints;    // Array of spawn points
    public float timeBetweenWaves = 5.0f;
    public TextMeshProUGUI waveClearText; // Reference to the UI Text element
    public TextMeshProUGUI bossIncomingText; // Reference to the UI Text element for boss announcement
    public GameObject bossPrefab; // Reference to the boss prefab

    private int currentWave = 0;
    private List<int> monstersRemainingInWave;  // List to track the number of each monster type remaining
    private bool isBossRound = false;
    public Transform boosSpawnPosition;
    void Start()
    {
        StartNextWave();
    }

    void StartNextWave()
    {
        if (currentWave < waves.Count)
        {
            // Initialize wave settings
            monstersRemainingInWave = new List<int>();

            foreach (var monsterInfo in waves[currentWave].monsters)
            {
                monstersRemainingInWave.Add(monsterInfo.monsterLimit);
            }

            // Hide the wave clear and boss incoming text at the beginning of each wave
            waveClearText.enabled = false;
            bossIncomingText.enabled = false;

            // Start spawning monsters for the current wave
            StartCoroutine(SpawnWave(waves[currentWave].monsters));
        }
        else if (!isBossRound)
        {
            isBossRound = true;
            StartCoroutine(StartBossRound());
        }
        else
        {
            Debug.Log("All waves and the boss round have been completed.");
        }
    }

    IEnumerator SpawnWave(List<MonsterInfo> monsterInfos)
    {
        List<GameObject> spawnedMonsters = new List<GameObject>();

        foreach (var monsterInfo in monsterInfos)
        {
            for (int j = 0; j < monsterInfo.monsterLimit; j++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                // Instantiate the monster at the chosen spawn point
                GameObject monster = Instantiate(monsterInfo.monsterType, spawnPoint.position, Quaternion.identity);
                spawnedMonsters.Add(monster);

                // Wait for a short time before spawning the next monster
                yield return new WaitForSeconds(1.0f);
            }
        }

        // Check if all monsters for the wave have been destroyed
        while (spawnedMonsters.Count > 0)
        {
            for (int i = spawnedMonsters.Count - 1; i >= 0; i--)
            {
                if (spawnedMonsters[i] == null)
                {
                    spawnedMonsters.RemoveAt(i);
                }
            }
            yield return null;
        }

        // Show the wave clear text when the wave is cleared
        waveClearText.enabled = true;

        // Wait for the specified time between waves
        yield return new WaitForSeconds(2.0f);

        // Hide the wave clear text
        waveClearText.enabled = false;

        currentWave++;
        StartNextWave();
    }

    IEnumerator StartBossRound()
    {
        bossIncomingText.text = "Boss is coming...";
        bossIncomingText.color = Color.red; 
        bossIncomingText.enabled = true;

        yield return new WaitForSeconds(5.0f);

        bossIncomingText.enabled = false;

        // Spawn the boss
        Instantiate(bossPrefab, boosSpawnPosition.position, Quaternion.identity);
    }
}
