using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;

    public float RemainingHealthPercentage {
        get{
            return _currentHealth / _maxHealth;
        }
    }

    public bool isInvincible {get; set;}

    public UnityEvent OnDamaged;
    public UnityEvent OnDied;

    public void TakeDamage(float damageAmount) {
        if (_currentHealth == 0) {
            return;
        }
        if (isInvincible) {
            return;
        }
        _currentHealth -= damageAmount;
        if (_currentHealth < 0) {
            _currentHealth = 0;
        }
        if (_currentHealth == 0) {
            OnDied.Invoke();
        }
        else {
            OnDamaged.Invoke();
        }
    }

    public void AddHealth(float amountToAdd) {
        if (_currentHealth == _maxHealth) {
            return;
        }
        _currentHealth += amountToAdd;
        if (_currentHealth > _maxHealth) {
            _currentHealth = _maxHealth;
        }
    }
}
