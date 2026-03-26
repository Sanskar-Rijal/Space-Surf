using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int ScorePerHit=15;

    [SerializeField] int hitPoints = 20;

    GameObject parentGameObject;
    ScoreBoard scoreBoard;

    private void Start() {
        //Look through all GameObjects in the scene and find the one that has the ScoreBoard script attached.
       scoreBoard = FindObjectOfType<ScoreBoard>();
       parentGameObject = GameObject.FindWithTag("SpawnAtRunTime");
       AddRigidbody();
    }

    private void AddRigidbody()
    {
        Rigidbody rb= gameObject.AddComponent<Rigidbody>(); //Adding rigidbody to the enemy so that it can collide with the particles and trigger the OnParticleCollision method
       //GetComponent<Rigidbody>().useGravity = false; 
       //Turn off gravity
       rb.useGravity = false; 
        
    }
    
    private void OnParticleCollision(GameObject other)
    {
        enemyHit();
        if(hitPoints < 1)
        {
        EnemyKilled();    
        }
    }

    private void enemyHit()
    {
        //Create VFX for hit effect
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform; //putting the vfx in the parentGameObject object to keep the hierarchy clean
        //Decrease enemy health
        hitPoints--;
    }

    private void EnemyKilled()
    {
        //Increase Score when enemy is killed;
        //scoreBoard.IncreaseScore(ScorePerHit);
        ScoreManager.Instance.AddScore(ScorePerHit);
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity); //Quaternion.identiy menas we dont need rotation
        fx.transform.parent = parentGameObject.transform; //putting the vfx in the parentGameObject object to keep the hierarchy clean
        Destroy(gameObject);
    }
}
