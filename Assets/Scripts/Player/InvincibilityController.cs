using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityController : MonoBehaviour
{
    private PlayerHealth _healthController;
    private void Awake() {
        _healthController = GetComponent<PlayerHealth>();
    }

    public void StartInvincibililty(float invincibilityDuration) {
        StartCoroutine(InvincibilityCoroutine(invincibilityDuration));
    }

    private IEnumerator InvincibilityCoroutine(float invinciblityDuration) {
        _healthController.isInvincible = true;
        yield return new WaitForSeconds(invinciblityDuration);
        _healthController.isInvincible = false;
    }
}
