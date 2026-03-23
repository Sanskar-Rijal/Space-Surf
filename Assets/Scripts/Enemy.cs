using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject deathVFX;
    [SerializeField] Transform parent;
    
    private void OnParticleCollision(GameObject other) {
       GameObject vfx =  Instantiate(deathVFX, transform.position, Quaternion.identity); //Quaternion.identiy menas we dont need rotation
       vfx.transform.parent = parent; //putting the vfx in the parent object to keep the hierarchy clean
        Destroy(gameObject);
    }
}
