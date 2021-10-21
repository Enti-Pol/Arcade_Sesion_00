using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [Header("Enemy Spawn")]
    [SerializeField]
    private float EnemyMinSpawnTime;
    [SerializeField]
    private float EnemyMaxSpawnTime;
    [Header("Power Up Spawn")]
    [SerializeField]
    private float PowerUpMinSpawnTime;
    [SerializeField]
    private float PowerUpMaxSpawnTime;
    [Header("Asteroid Spawn")]
    [SerializeField]
    private float AsteroidMinSpawnTime;
    [SerializeField]
    private float AsteroidMaxSpawnTime;

    [Header("Enemy GameObject")]
    [SerializeField]
    private GameObject enemy;

    [Header("Power Up GameObject")]
    [SerializeField]
    private GameObject powerUp;

    [Header("Asteroid GameObject")]
    [SerializeField]
    private GameObject asteroid;

    [Header("Camera")]
    [SerializeField]
    Camera camera;


    //Script Variables

    private Vector2 cameraXBounds;
    private Vector2 cameraYBounds;
    private float enemyWidth;
    private float powerUpWidth;

    private float enemySpawnTime;
    private float deltaEnemySpawnTime = 0f;

    private float powerUpSpawnTime;
    private float deltaPowerUpSpawnTime = 0f;

    private float asteroidSpawnTime;
    private float deltaAsteroidSpawnTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        // Iniciales de spawn
        enemySpawnTime = Random.Range(EnemyMinSpawnTime, EnemyMaxSpawnTime);
        powerUpSpawnTime = Random.Range(PowerUpMinSpawnTime, PowerUpMaxSpawnTime);
        asteroidSpawnTime = Random.Range(AsteroidMinSpawnTime, AsteroidMaxSpawnTime);

        // Update is called once per frame
        cameraXBounds.x = camera.ViewportToWorldPoint(new Vector2(0, 0)).x;
        cameraXBounds.y = camera.ViewportToWorldPoint(new Vector2(1, 0)).x;
        cameraYBounds.x = camera.ViewportToWorldPoint(new Vector2(0, 0)).y;
        cameraYBounds.y = camera.ViewportToWorldPoint(new Vector2(0, 1)).y;

        // Calculamos ancho
        enemyWidth = enemy.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f;
        powerUpWidth = powerUp.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        EnemySpawner();
        PowerUpSpawner();
        AsteroidSpawner();
    }

    /*private void FixedUpdate()
    {
        if (enemy.)
    }*/
    private void EnemySpawner()
    {
        deltaEnemySpawnTime += Time.deltaTime;

        if (deltaEnemySpawnTime >= enemySpawnTime)
        {
            Instantiate(enemy, new Vector2 (Random.Range(cameraXBounds.x + enemyWidth, cameraXBounds.y - enemyWidth), cameraYBounds.y + 1f), Quaternion.identity);
            deltaEnemySpawnTime = 0f;
            enemySpawnTime = Random.Range(EnemyMinSpawnTime, EnemyMaxSpawnTime);
            Debug.Log("Enemy Spawned");
        }
    }

    private void PowerUpSpawner()
    {
        deltaPowerUpSpawnTime += Time.deltaTime;

        if (deltaPowerUpSpawnTime >= powerUpSpawnTime)
        {
            Instantiate(powerUp, new Vector2(Random.Range(cameraXBounds.x + enemyWidth, cameraXBounds.y - enemyWidth), cameraYBounds.y + 1f), Quaternion.identity);
            deltaPowerUpSpawnTime = 0f;
            powerUpSpawnTime = Random.Range(PowerUpMinSpawnTime, PowerUpMaxSpawnTime);
            Debug.Log("PowerUp Spawned");
        }
    }

    private void AsteroidSpawner()
    {
        deltaAsteroidSpawnTime += Time.deltaTime;

        if (deltaAsteroidSpawnTime >= asteroidSpawnTime)
        {
            deltaAsteroidSpawnTime = 0f;
            asteroidSpawnTime = Random.Range(AsteroidMinSpawnTime, AsteroidMaxSpawnTime);
            Debug.Log("Asteroid Spawned");
        }
    }
}
