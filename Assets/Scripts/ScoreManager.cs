using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int currentScore = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("SavedScore"))
        {
            currentScore = PlayerPrefs.GetInt("SavedScore");
            Debug.Log("🔄 Điểm số được load lại: " + currentScore);
        }
        else
        {
            Debug.LogWarning("⚠ Không có điểm nào được lưu trước đó.");
        }
    }

    public void AddScore(int points)
    {
        currentScore += points;
        PlayerPrefs.SetInt("SavedScore", currentScore); // Lưu ngay khi cộng điểm
        PlayerPrefs.Save();
        Debug.Log("🔢 Điểm hiện tại (đã lưu vào PlayerPrefs): " + currentScore);
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void LoadScore()
    {
        if (PlayerPrefs.HasKey("SavedScore"))
        {
            currentScore = PlayerPrefs.GetInt("SavedScore");
            Debug.Log("🔄 Điểm số được load lại: " + currentScore);
        }
    }
}
    