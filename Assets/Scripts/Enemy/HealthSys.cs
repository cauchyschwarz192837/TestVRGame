using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSys : MonoBehaviour
{
    public GameObject powerUp;
    [SerializeField] MeshRenderer meshRendererToUse;
    [SerializeField] Material materialToUse;
    
    public float maxHealth;
    public float recoveryTime;
    public float baseDamage;
    public float currentHealth;
    //private Animator animator;
    private bool invincible = false;
    private Rigidbody _rigidBody;
    private GameObject _child;
    private Transform damageSource;

    void ChangeMaterialOnMesh() {
        meshRendererToUse.material = materialToUse;
    }

    void Start() {
        //animator = gameObject.GetComponent<Animator>();
        _rigidBody = gameObject.GetComponent<Rigidbody>();
    }
    
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Weapon") {
            TakeDamage();
        }
    }

    public void TakeDamage() {
        if (!invincible) {
            currentHealth -= baseDamage;
            if (powerUp.GetComponent<ParticleSystem>().isPlaying) {
                currentHealth -= 20;
                if (currentHealth < 0) {
                    currentHealth = 0;
                }
            }
            if (currentHealth == 0.0f) {
                Die();
            }
            else {
                Damage();
            }
        }
    }

    private void Damage() {
        //animator.SetTrigger("damage");
        StartCoroutine(FlashSprite()); // whatever hit enemy, player or not
    }

    IEnumerator FlashSprite() {
        invincible = true;
        if (currentHealth >= 80) {
            GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else if (currentHealth >= 40) {
            GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
        else if (currentHealth >= 20) {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
        
        yield return new WaitForSeconds(recoveryTime);
        GetComponent<MeshRenderer>().material = materialToUse;
        invincible = false;
    }

    private void Die() {
        //animator.SetTrigger("die");
        StartCoroutine(VanishSprite());
    }

    IEnumerator VanishSprite() {
        invincible = true;
        GetComponent<MeshRenderer>().material.color = Color.black;
        
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}
