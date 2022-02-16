using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartMenuUIHandler : MonoBehaviour
{
    public TextMeshProUGUI playerNameInput;
    public TextMeshProUGUI bestScoreText;
    public string highScoreName;
    public int highScore;

    // Start is called before the first frame update
    void Start()
    {
        if (GameDataManager.Instance.highScore!= 0)
        {
            highScoreName = GameDataManager.Instance.highScoreName;
            highScore = GameDataManager.Instance.highScore;
            bestScoreText.text = "High Score: " + highScoreName + " : " + highScore;

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        GameDataManager.Instance.playerName = playerNameInput.text;
        
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }
}
