using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemySpeed = 2f;
    public GameObject coinPrefab; // Kéo Prefab Coin vào đây trong Unity
    public AudioClip destroySound; // Âm thanh khi Enemy bị phá hủy

    private AudioSource audioSource; // Lưu trữ AudioSource

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        enemySpeed = EnemySpawner.instance.enemySpeed;
    }

    void Update()
    {
        moveEnemyLeft();
        enemyGone();
    }

    void moveEnemyLeft()
    {
        transform.Translate(-enemySpeed * Time.deltaTime, 0, 0);
    }

    void enemyGone()
    {
        if (gameObject.transform.position.x <= -5.48)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile")) // Đạn va chạm với Enemy
        {
            Debug.Log("💥 Enemy bị bắn!");

            // Tạo Coin
            SpawnCoin();

            // Xóa viên đạn
            Destroy(other.gameObject);

            // Phát âm thanh từ chính vị trí Enemy
            if (destroySound != null)
            {
                audioSource.volume = 0.3f; // Giảm âm lượng xuống 30%
                audioSource.pitch = 1.2f; // Tăng cao độ lên 1.2 lần
                AudioSource.PlayClipAtPoint(destroySound, transform.position);
            }
            else
            {
                Debug.LogWarning("⚠️ Không tìm thấy destroySound!");
            }

            // Xóa enemy sau khi phát âm thanh
            Destroy(gameObject);
        }
    }

    void SpawnCoin()
    {
        if (coinPrefab != null)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, 0, 0); // Spawn coin cao hơn enemy một chút
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("💰 Coin đã được spawn tại: " + spawnPosition);
        }
        else
        {
            Debug.LogWarning("⚠️ coinPrefab chưa được gán trong Inspector!");
        }
    }
    public void UpdateSpeed(float newSpeed)
    {
        enemySpeed = newSpeed;
        Debug.Log("⚡ Enemy speed được cập nhật: " + enemySpeed);
    }

}
