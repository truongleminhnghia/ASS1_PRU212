using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Singleton để gọi từ script khác
    public AudioSource audioSource; // Kéo AudioSource vào đây từ Inspector
    AudioManager audioManager;
    void Start()
    {
        audioManager = GetComponent<AudioManager>(); // Tìm AudioManager trong Scene
        if (audioManager == null)
        {
            Debug.LogError("❌ Không tìm thấy AudioManager trong Scene!");
        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("❌ Lỗi: AudioSource hoặc AudioClip chưa được gán!");
        }
    }
}
