using UnityEngine;

public class Spawner : MonoBehaviour
{
    float _elapsedTime = 0;
    float _spawnTime = 0.2f;
    int _spawnCount = 0;
    int _objectsToSpawn = 20;
    public GameObject CoinPrefab;
    void Update()
    {
        if(_spawnCount >= _objectsToSpawn) { return; }

        _elapsedTime += Time.deltaTime;

        if(_elapsedTime > _spawnTime)
        {
            _elapsedTime = 0;
            _spawnCount++;

            GameObject coin = Instantiate(CoinPrefab);

            coin.GetComponent<Rigidbody2D>().position = transform.position;
        }
    }
}
