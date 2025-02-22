using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // Giá trị của Coin
    private Score scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<Score>(); // Tìm đối tượng chứa script Score
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player đã nhặt coin!");
            Score.instance.AddToScore(coinValue);
            Destroy(gameObject); // Xóa coin sau khi nhặt
        }
    }
}