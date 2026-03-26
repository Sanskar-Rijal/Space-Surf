using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayFinalScore : MonoBehaviour
{
    // Start is called before the first frame update
    TMP_Text FinalScoreText;
    void Start()
    {
        FinalScoreText = GetComponent<TMP_Text>();
        FinalScoreText.text = "Your Final Score is: " + ScoreManager.Instance.Score.ToString();
    }

     private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("spaceShips");
            ScoreManager.Instance.ResetScore();
        }
    }

   
}
