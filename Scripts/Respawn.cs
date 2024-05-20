using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public Transform _respawnPoint;

    public float _respawnDelay = 10f;

    private GameObject _enemyObject;

    void Start()
    {
        _enemyObject = gameObject;
        _enemyObject.SetActive(false);
    }

    public void RespawnEnemy()
    {    
        _enemyObject.SetActive(true);
        _enemyObject.transform.position = _respawnPoint.position;
    }

    public void StartRespawnTimer()
    {
        Invoke("RespawnEnemy", _respawnDelay);
    }
}