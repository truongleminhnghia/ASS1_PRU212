using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float minSize = 0.5f;
    public float maxSize = 2f;
    public float minSpeed = 2f;
    public float maxSpeed = 5f;
    private float speed;
    private Vector2 direction;

    void Start()
    {
        // Random kích thước thiên thạch
        float randomSize = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(randomSize, randomSize, 1);

        // Random hướng di chuyển: từ trái sang phải hoặc ngược lại
        bool moveRight = Random.value > 0.5f; // 50% tỉ lệ đi trái -> phải hoặc phải -> trái
        direction = moveRight ? Vector2.right : Vector2.left;

        // Random tốc độ
        speed = Random.Range(minSpeed, maxSpeed);

        // Lật hình nếu đi từ phải qua trái
        if (!moveRight)
        {
            transform.localScale = new Vector3(-randomSize, randomSize, 1);
        }
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        Debug.Log("Asteroid is moving at speed: " + speed + " in direction: " + direction);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
