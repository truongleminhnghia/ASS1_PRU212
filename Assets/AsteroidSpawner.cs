using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public static AsteroidSpawner instance;
    public GameObject asteroidPrefab;
    public float spawnRate = 2f;
    public float spawnHeight = 4f; // Điều chỉnh theo camera
    public float asteroidSpeed = 2f; // Tốc độ bay từ phải sang trái

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        InvokeRepeating("SpawnAsteroid", 2f, spawnRate);
    }
    void Awake()
    {
        instance = this;
    }

    void SpawnAsteroid()
    {
        // Lấy tọa độ mép phải của màn hình
        float screenRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        // Lấy tọa độ mép trên & dưới để spawn hợp lý
        float screenTop = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        float screenBottom = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

        // Spawn trong khoảng từ giữa màn hình trở lên
        float spawnX = screenRight;
        float spawnY = Random.Range(screenBottom + 1, screenTop - 1); // Giới hạn để không spawn quá gần mép trên/dưới

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

        // Tạo thiên thạch
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

        // Điều chỉnh kích thước ngẫu nhiên (nhỏ như enemy)
        float randomSize = Random.Range(0.1f, 0.25f); // Giới hạn để nhỏ hơn hiện tại
        asteroid.transform.localScale = new Vector3(randomSize, randomSize, 1);

        // Thêm vận tốc để thiên thạch bay từ phải sang trái
        Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(-asteroidSpeed, 0);
        }

        Debug.Log("Spawning Asteroid at: " + spawnPosition);
    }
    //void Update()
    //{
    //    transform.Translate(Vector2.left * asteroidSpeed * Time.deltaTime);
    //}
    public void IncreaseAsteroidSpeed(float amount)
    {
        asteroidSpeed += amount;
        Debug.Log("🌠 Asteroid speed tăng lên: " + asteroidSpeed);
    }
}
