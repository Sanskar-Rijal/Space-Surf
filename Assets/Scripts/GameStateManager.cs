using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public bool playerCrasehed {get; private set;}

    private void Awake() {
        
        if(Instance !=null)
        {
            //means there is already an instance so 
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetPlayerCrashed()
    {
        playerCrasehed = true;
    }

    public void ResetGameState()
    {
        //make the Score zero as well 
        ScoreManager.Instance.ResetScore();
        playerCrasehed = false;
    }
}
