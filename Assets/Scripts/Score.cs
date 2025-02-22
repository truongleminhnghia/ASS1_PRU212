using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score instance;
    public Text scoreText;
    private int score = 0;

    private int lastMilestone = 0; // Biến mốc điểm để kiểm tra tăng độ khó
    public EnemySpawner enemySpawner; // Tham chiếu đến EnemySpawner
    public AsteroidSpawner asteroidSpawner; // Tham chiếu đến AsteroidSpawner

    
    private float speedIncrease = 0.5f; // Mức tăng tốc độ mỗi lần đạt mốc

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        UpdateScore();
    }    
    public void AddToScore(int amount)
    {
        score += amount;
        UpdateScore();

        // Kiểm tra nếu đạt đến bội số của 10 và lớn hơn mốc trước đó
        if (score >= lastMilestone + 10)
        {
            lastMilestone = score; // Cập nhật mốc điểm mới
            IncreaseDifficulty();
        }
    }

    public int GetScore()
    {
        return score;
    }

    private void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
            Debug.Log("✅ Điểm số cập nhật: " + score);
        }
        else
        {
            Debug.LogError("⚠ ScoreText chưa được gán trong Inspector!");
        }
    }
    private void IncreaseDifficulty()
    {
        Debug.Log("⚡ Tăng tốc độ Enemy và Asteroid!");

        EnemySpawner.instance?.IncreaseEnemySpeed(0.5f);
        AsteroidSpawner.instance?.IncreaseAsteroidSpeed(0.5f);

        //if (enemySpawner != null)
        //{
        //    enemySpawner.enemySpeed += speedIncrease; // Tăng tốc độ Enemy
        //}
        //else
        //{
        //    Debug.LogError("❌ EnemySpawner chưa được gán trong Score!");
        //}

        //if (asteroidSpawner != null)
        //{
        //    asteroidSpawner.asteroidSpeed += speedIncrease; // Tăng tốc độ Asteroid
        //}
        //else
        //{
        //    Debug.LogError("❌ AsteroidSpawner chưa được gán trong Score!");
        //}
    }
}
