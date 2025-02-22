using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    private string filePath;
    public List<int> scoreHistory = new List<int>();

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

        string customFolder = "D:/PRU212";
        if (!Directory.Exists(customFolder))
        {
            Directory.CreateDirectory(customFolder);
        }

        filePath = Path.Combine(customFolder, "scoreHistory.json");
        LoadGameHistory();
    }

    public void SaveScore(int score)
    {
        if (score <= 0)
        {
            Debug.LogWarning("⚠ Không lưu điểm vì giá trị bằng 0.");
            return;
        }

        Debug.Log("✅ Lưu điểm: " + score);

        scoreHistory.Add(score);
        ScoreData data = new ScoreData(scoreHistory);
        string json = JsonUtility.ToJson(data, true); // Chỉ lưu danh sách điểm

        File.WriteAllText(filePath, json);
        Debug.Log("📂 Điểm đã được lưu vào file: " + filePath);
    }

    public void SaveGameHistory()
    {
        string json = JsonUtility.ToJson(new ScoreData(scoreHistory), true);
        File.WriteAllText(filePath, json);
        Debug.Log("✅ Điểm đã được lưu vào file: " + filePath);
    }

    public void LoadGameHistory()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            ScoreData data = JsonUtility.FromJson<ScoreData>(json);

            if (data != null && data.scores != null)
            {
                scoreHistory = data.scores;
                Debug.Log("📂 Đã tải lịch sử điểm từ file.");
            }
            else
            {
                Debug.LogWarning("⚠ File JSON không hợp lệ, khởi tạo danh sách trống.");
                scoreHistory = new List<int>();
            }
        }
        else
        {
            Debug.LogWarning("⚠ Không tìm thấy file lưu điểm, tạo file mới.");
            scoreHistory = new List<int>();
        }
    }

    public string GetGameHistory()
    {
        return string.Join("\n", scoreHistory);
    }
    // Cấu trúc dữ liệu để lưu file JSON
    [System.Serializable]
    private class ScoreData
    {
        public List<int> scores;

        public ScoreData(List<int> scores)
        {
            this.scores = scores;
        }
    }
}
