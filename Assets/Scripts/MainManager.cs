using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public TextMeshProUGUI bestScoreText;
    public GameObject GameOverText;

    private string playerName;
    public int highScore;
    public string highScoreName;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        playerName = GameDataManager.Instance.playerName;
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        if (GameDataManager.Instance.highScore != 0)
        {
            highScoreName = GameDataManager.Instance.highScoreName;
            highScore = GameDataManager.Instance.highScore;
            bestScoreText.text = "High Score: " + highScoreName + ": " + highScore;

        }

        else
        {
            highScoreName = playerName;
            highScore = 0;
            GameDataManager.Instance.highScoreName = playerName;
            GameDataManager.Instance.highScore = 0;
            bestScoreText.text = "High Score: " + highScoreName + ": " + highScore;
        }
    

    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (m_Points > highScore)
        {
            highScore = m_Points;
            highScoreName = playerName;

        }

        ScoreText.text = $"{playerName} Score : {m_Points}";
    }

    void AddPoint(int point)
    {
        m_Points += point;
        
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        GameDataManager.Instance.highScore = highScore;
        GameDataManager.Instance.highScoreName = highScoreName;
        GameDataManager.Instance.SaveHighScore();
    }
}
