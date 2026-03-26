using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    [Header("General Setup Settings")]
    [Tooltip("How fast ship lerps to target rotation")] [SerializeField] float lerpSpeed = 15f;
    [Tooltip("Input action for movement")] [SerializeField] InputAction movement;

    //for laster
    [Header("Laser gun array")]
   [Tooltip("Add all lasers in this array")] [SerializeField] GameObject[] lasers;

    [SerializeField] InputAction fire;
    [Tooltip("How fast ship moves up and down")] [SerializeField] float controlSpeed = 10f;

    [Header("Screen position limits")]
    [Tooltip("How much horizontal movement is allowed")] [SerializeField] float xRange = 9f; //go from  -value to +value
    [Tooltip("How much vertical movement is allowed")] [SerializeField] float yRange = 4f; // go from - value to + value

    //Field for rotation 
    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -15f;
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
        ExitGame();
    }

    void ExitGame()
    {
     if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }   
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
        if(fire.IsPressed())
        {
            ActivateOrDeactivateLasers(isActive: true);
        }
        else
        {
            ActivateOrDeactivateLasers(isActive: false);
        }
    }

    void ActivateOrDeactivateLasers(bool isActive)
    {
        foreach (GameObject i in lasers)
        {
            var emissionModule = i.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

  

}
