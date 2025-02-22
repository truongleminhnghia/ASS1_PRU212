using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 1f;
    public GameObject explosionEffect; // Hiệu ứng nổ (nếu có)
    public AudioClip hitSound; // Âm thanh khi trúng Enemy
    public AudioClip asteroidHitSound; // Âm thanh khi trúng Asteroid
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        moveProjectileRight();
        projectileGone();
    }

    void moveProjectileRight()
    {
        transform.Translate(projectileSpeed * Time.deltaTime, 0, 0);
    }

    void projectileGone()
    {
        if (gameObject.transform.position.x >= 5)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("🔍 Va chạm với: " + collision.gameObject.name);

        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("💥 Đạn va chạm với Enemy!");

            // Phát âm thanh khi trúng Enemy
            if (hitSound != null)
            {
                AudioSource.PlayClipAtPoint(hitSound, transform.position, 0.2f);
                Debug.Log("🎵 Phát âm thanh trúng Enemy!");
            }

            // Tạo hiệu ứng nổ nếu có
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            // Hủy enemy và đạn
            Destroy(collision.gameObject); // Xóa enemy
            Destroy(gameObject); // Xóa viên đạn
        }
        if (collision.CompareTag("Asteroid")) // Kiểm tra nếu va chạm với thiên thạch
        {
            if (asteroidHitSound != null)
            {
                AudioSource.PlayClipAtPoint(asteroidHitSound, transform.position);
                Debug.Log("🎵 Phát âm thanh trúng Thiên Thạch!");
            }

            // Tạo hiệu ứng nổ nếu có
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            Destroy(collision.gameObject); // Phá hủy thiên thạch
            Destroy(gameObject); // Phá hủy viên đạn
        }
    }
}
