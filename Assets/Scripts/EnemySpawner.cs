using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;
    public GameObject enemyToSpawn;

    public bool canSpawn = true;
    public float enemySpawnTime = 1.5f;
    public float enemyMaxY = 1.82f;
    public float enemyStartingX = 5.41f;
    public float enemySpeed = 2f; // Giá trị tốc độ mặc định

    float enemyRandomY = 0f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(enemySpawn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Debug.Log("✅ EnemySpawner đã được khởi tạo!"); // Kiểm tra xem instance có hoạt động không
    }
    IEnumerator enemySpawn()
    {
        while (canSpawn == true)
        {
            spawnEnemy();
            yield return new WaitForSeconds(enemySpawnTime);
        }
    }

    void spawnEnemy()
    {
        // Use Camera.main to determine the actual boundaries
        float cameraHeight = Camera.main.orthographicSize;
        float minY = -cameraHeight + 0.5f; // Add buffer for enemy size
        float maxY = cameraHeight - 0.5f;

        enemyRandomY = Random.Range(minY, maxY);

        Instantiate(enemyToSpawn, new Vector3(enemyStartingX, enemyRandomY, 0), Quaternion.identity);
    }
    public void IncreaseEnemySpeed(float amount)
    {
        enemySpeed += amount;
        Debug.Log("⚡ Enemy speed tăng lên: " + enemySpeed);

        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Include, FindObjectsSortMode.None);


        foreach (Enemy enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.UpdateSpeed(enemySpeed);
            }
        }
    }
}
