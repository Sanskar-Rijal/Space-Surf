using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    float delay = 1f;

    [SerializeField] ParticleSystem crashParticles;

    MeshRenderer playerShip;

    AudioSource crashedSound;
      //Get Movement Script from Player 
    PlayerController playerController;
    BoxCollider playerCollider;

      void Start()
    {
        playerShip = GetComponent<MeshRenderer>();
        playerController = GetComponent<PlayerController>();
        playerCollider = GetComponent<BoxCollider>();
        crashedSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
       // Debug.Log("triggered with " + other.gameObject.name);
       // hide player ship
       playerShip.enabled = false; 
       // disable player box collider
       playerCollider.enabled = false; 
       //show boom boom particles
        crashParticles.Play();
        crashedSound.Play();
        ReloadGame();
    }



//    private void OnCollisionEnter(Collision other) {
//         Debug.Log("Collided with " + other.gameObject.name);
//     }

    private void ReloadGame()
    {
        playerController.enabled = false; // disable player movement
        Invoke("Respawn",delay);
    }

    private void Respawn()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentSceneIndex);
    }


}
