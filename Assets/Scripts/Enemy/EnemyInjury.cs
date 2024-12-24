using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyInjury : MonoBehaviour
{
    public GameObject floatingTextPrefab;
    public float bounceForce = 10f; // Adjust the force as needed

    private HealthSys getHealth;

    private GameObject target;

    void Start() {
        getHealth = gameObject.GetComponent<HealthSys>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the tag "Weapon"
        if (collision.gameObject.tag == "Weapon")
        {
            target = collision.gameObject;
            if (floatingTextPrefab != null) {
                ShowFloatingText();
            }

            // Calculate the bounce direction (opposite to the collision normal)
            Vector3 bounceDirection = collision.gameObject.transform.up;

            // Apply the bounce force
            GetComponent<Rigidbody>().AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        }
    }

    public void ShowFloatingText() 
    {
        Transform cameraTransform = Camera.main.transform;

        // Calculate the position a short distance away from the camera
        Vector3 spawnPosition = cameraTransform.position + cameraTransform.forward; // Adjust the distance as needed

        // Instantiate the floating text prefab at the calculated position and facing the camera
        var go = Instantiate(floatingTextPrefab, spawnPosition, Quaternion.LookRotation(-cameraTransform.forward, cameraTransform.up));

        //var go = Instantiate(floatingTextPrefab, transform.position, target.transform.rotation);
        go.GetComponent<TextMesh>().text = getHealth.currentHealth.ToString();
        if (getHealth.currentHealth >= 80) {
            go.GetComponent<TextMesh>().color = Color.green;
        }
        else if (getHealth.currentHealth >= 40) {
            go.GetComponent<TextMesh>().color = Color.yellow;
        }
        else if (getHealth.currentHealth >= 20) {
            go.GetComponent<TextMesh>().color = Color.red;
        }
        else {
            go.GetComponent<TextMesh>().color = Color.black;
        }
    }
}
