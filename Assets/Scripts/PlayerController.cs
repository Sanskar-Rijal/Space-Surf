using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] InputAction movement;
    [SerializeField] float controlSpeed = 10f;
    [SerializeField] float xRange = 9f; //go from  -value to +value
    [SerializeField] float yRange = 4f; // go from - value to + value


    //We have to enable the new system for input 
    private void OnEnable() {
        movement.Enable();
    }

    private void OnDisable() {
        movement.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    //move rocket 
    void MovePlayer()
    {
            //    float horizontalThrow =  Input.GetAxis("Horizontal");
    //    float vorizontalThrow = Input.GetAxis("Vertical");
    //    Debug.Log(horizontalThrow);
    //    Debug.Log(vorizontalThrow);
    float horizontalThrow = movement.ReadValue<Vector2>().x;
    float verticalThrow = movement.ReadValue<Vector2>().y;

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
        transform.localRotation = Quaternion.Euler(-30f,30f,0f);
        
    }

}
