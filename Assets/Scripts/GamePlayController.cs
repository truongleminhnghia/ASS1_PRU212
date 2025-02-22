using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayController : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePanel;
    public void PauseGameButton()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResumeButton()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void BackToMainMenu() // Hàm quay lại MainMenu
    {
        //Time.timeScale = 1f; // Reset lại tốc độ game trước khi chuyển scene
        SceneManager.LoadScene("MainMenu"); // Đổi "MainMenu" thành tên chính xác của scene
    }
    public void ShowGameHistory()
    {
        if (GameManager.instance != null)
        {
            Debug.Log(GameManager.instance.GetGameHistory());
        }
        else
        {
            Debug.LogError("⚠ GameManager chưa được khởi tạo!");
        }
    }
}
