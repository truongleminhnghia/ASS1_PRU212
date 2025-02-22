using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    public GameObject particles;

    Score addToScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            callScoreScript();
            
            spawnParticles(collision.transform.position);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    void spawnParticles(Vector2 tempPosition)
    {
        GameObject spawnedParticles = Instantiate(particles, tempPosition, Quaternion.identity);

        Destroy(spawnedParticles, 1f);
    }

    void callScoreScript()
    {
        addToScore = GameObject.Find("ScoreManager").GetComponent<Score>();

        if (addToScore != null)
        {
            addToScore.AddToScore(10);  // Tăng 10 điểm mỗi lần diệt Enemy
            Debug.Log("🔴 Gọi hàm AddToScore! Điểm hiện tại: " + addToScore.GetScore());
        }
        else
        {
            Debug.LogError("⚠ Không tìm thấy ScoreManager!");
        }
    }

}
