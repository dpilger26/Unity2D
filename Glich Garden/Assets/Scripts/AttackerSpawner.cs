using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    // configuration parameters
    [SerializeField] GameObject attackerPrefab;
    [SerializeField] int minSpawnDelay = 1;
    [SerializeField] int maxSpawnDelay = 5;

    // state parameters
    bool spawn = true;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay));
            spawnEnemy();
        }
    }

    void spawnEnemy()
    {
        var newAttacker = Instantiate(attackerPrefab, transform.position, Quaternion.identity);

        // This makes the instantiated object a child of the object instantiating it
        newAttacker.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
