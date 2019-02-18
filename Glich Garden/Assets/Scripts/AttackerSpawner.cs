using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    // configuration parameters
    [SerializeField] Attacker[] attackerPrefabs;
    [SerializeField] int minSpawnDelay = 1;
    [SerializeField] int maxSpawnDelay = 5;

    // cashed parameters
    GameTimer gameTimer;

    private void Awake()
    {
        gameTimer = FindObjectOfType<GameTimer>();
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (!gameTimer.TimerComplete())
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnAttacker();
        }
    }

    void SpawnAttacker()
    {
        var attacker = attackerPrefabs[UnityEngine.Random.Range(0, attackerPrefabs.Length)];
        Spawn(attacker);
    }

    private void Spawn(Attacker attacker)
    {
        var newAttacker = Instantiate(attacker, transform.position, Quaternion.identity);

        // This makes the instantiated object a child of the object instantiating it
        newAttacker.transform.parent = transform;
    }
}
