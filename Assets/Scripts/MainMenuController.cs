using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGameButton()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void QuitGameButton()
    {
        Application.Quit();
    }    
}
