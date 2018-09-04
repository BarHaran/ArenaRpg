using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float maxHealth; //Amount of health.
    public float currentHealth;
    public float healthRegen; //Amount of health restored every second.
    public float maxShieldSize; //Amount of shield.
    public float currentShield;
    public float shieldRegen; //Amount of shield restored every second.
    public int shieldDownTime; //Time before shield restores.
    public int currentShieldDownTime;
    public float damageResistence; //How much of the damage is neglected (%).
    public float fireResistence; //How much of the fire damage is neglected and it's duration shortened (%).
    public float knockbackResistence; //How much of the knockback power is lessend (%).
    public float reflect; //The amount of damage that is reflected back on the attacker (%).
    public float movementSpeed;

    public float fireDamage;
    public float fireDuration;
    public float stunDuration;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator HealthAndShieldRegen()
    {
        if (currentHealth < maxHealth)
            currentHealth += healthRegen;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        if (currentShieldDownTime > 0)
            currentShieldDownTime--;
        else if (currentShield < maxShieldSize)
            currentShield += shieldRegen;
        if (currentShield > maxShieldSize)
            currentShield = maxShieldSize;
        yield return new WaitForSeconds(1);
        yield return HealthAndShieldRegen();
    }
}
