using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameFinished : MonoBehaviour
{
    PlayableDirector playDir;
    // Start is called before the first frame update
    void Start()
    {
        playDir = GetComponent<PlayableDirector>();
        playDir.stopped += GameFinish;
    }

    private void GameFinish(PlayableDirector obj)
    {
       SceneManager.LoadScene("spaceShips"); 
    }

}
