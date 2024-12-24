using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _damageAmount;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Target") {
            var healthController = collision.gameObject.GetComponent<PlayerHealth>();
            healthController.TakeDamage(_damageAmount);
        }
    }
}
