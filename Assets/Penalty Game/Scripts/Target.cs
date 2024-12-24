using System.Collections;
using System.Collections.Generic; 
using UnityEngine; 

public class Target : MonoBehaviour
{
    // Public variable to control the movement speed of the target
    public float speed = 5f;

    // Reference to the Joystick script to control target movement
    private Joystick joystick;

    
    void Start()
    {
        // Find the "Joystick" GameObject in the scene and get its Joystick component
        joystick = GameObject.Find("JoystickButton").GetComponent<Joystick>();
    }

   
    void Update()
    {
        // Get the horizontal and vertical input values from the joystick
        float moveX = joystick.GetInputVector().x;
        float moveY = joystick.GetInputVector().y;

        // Translate (move) the target's position based on joystick input and speed
        // The movement is scaled by deltaTime to ensure smooth movement regardless of frame rate
        transform.Translate(moveX * speed * Time.deltaTime, moveY * speed * Time.deltaTime, 0f);
    }
}
