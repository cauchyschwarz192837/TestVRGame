using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
	[Header("Target")]
    public Transform target;  // The target towards which the ball will be shot

	[Header("Shoot")]
    public float Force;       // Current shooting force of the ball
    public float maxForce;    // Maximum allowed shooting force
	public float distance;    // Shoot force to target when curve

	[Header("UI")]
    public Slider forceUI;    // UI slider to display the shooting force
    public Toggle curveToggle;
    public Toggle powerShot;

	[Header("Goal Keeper")]
    GoalKeeper goal;         // Reference to the goal keeper (not shown in the provided code)
    public GameObject GoalKeeper;  // The goal keeper game object

	[Header("Ball Position")]
    Vector3 StartPos;         // Initial position of the ball

	[Header("Goal Keeper Position")]
    Vector3 GoalPos;          // Initial position of the goal keeper

	[Header("Curve")]
    public float curveStrength;  // Strength of the curve force applied to the ball
	Vector3 lateralDirection; 

    void Start()
    {
        StartPos = transform.position;  // Store the initial ball position
        GoalPos = GoalKeeper.transform.position;  // Store the initial goal keeper position
        forceUI.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))  // While holding space key, increase the shooting force
        {
            forceUI.gameObject.SetActive(true);
            Force++;
            slider();  // Update the UI slider to show the current force
        }

        if (Input.GetKeyUp(KeyCode.Space))  // When space key is released, shoot the ball
        {
            StartCoroutine(Wait());  // Start a coroutine to handle the shooting process
        }

        if (Force > maxForce)
            Force = maxForce;  // Ensure shooting force doesn't exceed maximum
    }

    void FixedUpdate()
    {

        if(curveToggle != null && curveToggle.isOn)
        {
            powerShot.isOn = false;
            distance = 5f;
            curve();
        }

        if(powerShot != null && powerShot.isOn)
        {
           curveToggle.isOn = false;
            distance = 1f;
        }

    }

    void shoot()
    {
        Vector3 Shoot = ((target.position * distance)- this.transform.position).normalized;  // Calculate shooting direction
        GetComponent<Rigidbody>().angularDrag = 1;  // Set angular drag for ball behavior
        GetComponent<Rigidbody>().AddForce(Shoot * Force + new Vector3(lateralDirection.x, 3f, lateralDirection.z), ForceMode.Impulse);  // Shoot the ball with calculated force
    }

    void curve()
    {
        // Get the current velocity of the ball's rigidbody
		Vector3 velocity = GetComponent<Rigidbody>().velocity;

		// Isolate the horizontal movement of the ball by setting the y-component to 0
		Vector3 lateralVelocity = new Vector3(velocity.x, 0, velocity.z);

		// Calculate the speed of the ball's lateral movement
		float speed = lateralVelocity.magnitude;

		// Calculate the normalized direction of the ball's lateral movement
		lateralDirection = lateralVelocity.normalized;

		// Calculate a curve force to influence the ball's trajectory
		Vector3 curveForce = Vector3.Cross(lateralDirection * 0.6f, Physics.gravity.normalized) * curveStrength * speed;

		// Apply the calculated curve force based on the position of the target	
		if (target.position.x > 3f)
		{
 	   // If the target is to the right of the ball, apply a curve force to the left
    	GetComponent<Rigidbody>().AddForce(-curveForce, ForceMode.Force);
		}
		if (target.position.x < -3f)
		{
    	// If the target is to the left of the ball, apply a curve force to the right
    	GetComponent<Rigidbody>().AddForce(curveForce, ForceMode.Force);
		}
    }

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.CompareTag("Pole"))
		{
            curveToggle.isOn = false;
		}

         if (col.gameObject.CompareTag("Net"))
        {
            curveToggle.isOn = false;
        }
	}

    public void slider()
    {
        forceUI.value = Force;  // Update the UI slider with the current shooting force
    }

    public void ResetGauge()
    {
        Force = 0;  // Reset the shooting force
        forceUI.value = 0;  // Reset the UI slider value
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);  // Wait for a moment before shooting the ball
        forceUI.gameObject.SetActive(false);
        shoot();  // Shoot the ball
        yield return new WaitForSeconds(0.05f);
        FindObjectOfType<GoalKeeper>().GoalMove();  // Move the goal keeper
        yield return new WaitForSeconds(1.5f);
        ResetGauge();  // Reset the shooting force and UI slider

        GetComponent<Rigidbody>().angularDrag = 40;  // Adjust angular drag after the shot
        yield return new WaitForSeconds(3f);

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = StartPos;  // Reset the ball position
        GoalKeeper.transform.position = GoalPos;  // Reset the goal keeper position
        curveStrength = 1.5f;  // Reset the curve strength

        FindObjectOfType<GoalKeeper>().Reset();
        FindObjectOfType<GoalKeeper>().Move = 0;  // Reset the goal keeper's movement index
    }
}
