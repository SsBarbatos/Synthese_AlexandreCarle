using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _prefabEnemy = default;
    [SerializeField] private GameObject _enemyContainer = default;
    private bool _stopSpawn = false;
    private float spawnDelay = 3f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(2f);

        while (!_stopSpawn)
        {
            Vector3 spawnPosition = new Vector3(9f, -3.50f, 0f);
            GameObject newEnemy = Instantiate(_prefabEnemy, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void SetSpawnDelay(float time)
    {
        spawnDelay = time;
    }

    public void StartSpawn()
    {
        _stopSpawn = false;
        StartCoroutine(SpawnEnemyRoutine());
    }
    public void StopSpawn()
    {
        _stopSpawn = true;
    }
}
