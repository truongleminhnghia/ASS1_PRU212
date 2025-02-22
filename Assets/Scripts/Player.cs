using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject projectile;
    public float playerSpeed = 3f;
    public Sprite straightSprite, upSprite, downSprite;
    public GameObject flame;
    public AudioSource audioSource;
    public AudioClip shootingAudioClip;

    private SpriteRenderer spriteRenderer;
    private float maxPlayerX = 4.6f;
    private float maxPlayerY = 1.82f;
    private float playerCollisionOffset = 0.4f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("❌ Player chưa có AudioSource!");
        }
    }

    void Update()
    {
        PlayerMovement();
        SpawnProjectileOnKey();
    }

    void PlayerMovement()
    {
        bool isMoving = false;
        float moveX = 0, moveY = 0;

        if (Input.GetKey(KeyCode.UpArrow) && transform.position.y <= maxPlayerY)
        {
            moveY = playerSpeed * Time.deltaTime;
            spriteRenderer.sprite = upSprite;
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && transform.position.y >= -maxPlayerY)
        {
            moveY = -playerSpeed * Time.deltaTime;
            spriteRenderer.sprite = downSprite;
            isMoving = true;
        }
        else
        {
            spriteRenderer.sprite = straightSprite;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x >= -maxPlayerX)
        {
            moveX = -playerSpeed * Time.deltaTime;
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x <= maxPlayerX)
        {
            moveX = playerSpeed * Time.deltaTime;
            isMoving = true;
        }
        flame.SetActive(isMoving);
        transform.Translate(moveX, moveY, 0);
    }

    void SpawnProjectileOnKey()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.P))
        {
            SpawnProjectile();
        }
    }

    void SpawnProjectile()
    {
        Instantiate(projectile, new Vector3(transform.position.x + playerCollisionOffset, transform.position.y, 0), Quaternion.identity);
        PlayShootSound();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Asteroid"))
        {
            int currentScore = PlayerPrefs.GetInt("SavedScore", 0); // Lấy từ PlayerPrefs
            Debug.Log("📌 Điểm cuối cùng trước khi lưu: " + currentScore);

            if (GameManager.instance != null && currentScore > 0)
            {
                GameManager.instance.SaveScore(currentScore);
            }
            else
            {
                Debug.LogWarning("⚠ Không thể lưu điểm, giá trị bằng 0 hoặc GameManager chưa khởi tạo.");
            }

            SceneManager.LoadScene("SampleScene");
        }
    }

    void PlayShootSound()
    {
        if (audioSource != null && shootingAudioClip != null)
        {
            audioSource.PlayOneShot(shootingAudioClip, 0.1f);
        }
    }
}
