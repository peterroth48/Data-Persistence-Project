using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartMenuUIHandler : MonoBehaviour
{
    public TextMeshProUGUI playerNameInput;

    // Start is called before the first frame update
    void Start()
    {
        
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
