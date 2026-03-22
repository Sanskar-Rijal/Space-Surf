using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] float lerpSpeed = 15f;
    [SerializeField] InputAction movement;

    [SerializeField] InputAction fire;
    [SerializeField] float controlSpeed = 10f;
    [SerializeField] float xRange = 9f; //go from  -value to +value
    [SerializeField] float yRange = 4f; // go from - value to + value

    //Field for rotation 
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -15f;

    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlThrowFactor = -20f;
    float horizontalThrow, verticalThrow;



   

    //We have to enable the new system for input 
    private void OnEnable() {
        movement.Enable();
        fire.Enable();
    }

    private void OnDisable() {
        movement.Disable();
        fire.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
       // SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayer();
        ProcessFiring();
    }



    //move rocket 
    void MovePlayer()
    {
            //    float horizontalThrow =  Input.GetAxis("Horizontal");
    //    float vorizontalThrow = Input.GetAxis("Vertical");
    //    Debug.Log(horizontalThrow);
    //    Debug.Log(vorizontalThrow);
     horizontalThrow = movement.ReadValue<Vector2>().x;
     verticalThrow = movement.ReadValue<Vector2>().y;

     float xOffset = horizontalThrow * Time.deltaTime * controlSpeed;
     float yOffset = verticalThrow * Time.deltaTime * controlSpeed;

    float newXPosition = transform.localPosition.x + xOffset ;
    float newYPostion = transform.localPosition.y + yOffset;

    //Clamping the position so ship cant go out of the screen
    float clampedXPosition = Mathf.Clamp(newXPosition,-xRange,xRange);
    float clampedYPosition = Mathf.Clamp(newYPostion,-yRange,yRange);

    //move the ship 
    transform.localPosition = new Vector3(clampedXPosition, clampedYPosition, transform.localPosition.z);

    }

    void RotatePlayer()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = verticalThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;

       

        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = horizontalThrow * controlThrowFactor;

        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, roll);
        
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * lerpSpeed);
    }

    void ProcessFiring()
    {
        // if the player pushes fire button then print shooting else don't print shooting
        //for old control system
        //if (Input.GetButton("Fire1"))
        //{
        //    Debug.Log("Shooting");
        //}
        if(fire.WasPressedThisFrame())
        {
            Debug.Log("Shooting");
        }
    }

}
