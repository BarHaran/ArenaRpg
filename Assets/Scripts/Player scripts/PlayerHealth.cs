using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Properites
{
    public enum HealthBonusType
    {
        Regen,
        Damage,
        Resistance
    }

    public class PlayerHealth : MonoBehaviour
    {
        public PlayerSwordControl sword;
        public float currentHealth;
        public float maxHealth;
        public float fireDamage;
        public float fireTime;
        public float currentShield;
        public float maxShield;
        public float shieldRegenRate;
        public float healthRegenRate;
        public float damageResistence;
        public float fireResistence;
        public Dictionary<HealthBonusType, float> bonusAtHealth;
        public Dictionary<HealthBonusType, float> bonusStrength;
        public Dictionary<HealthBonusType, bool> activeBonuses;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Fire();
            Death();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "EnemySword")
            {
                EnemySwordControl sword = other.GetComponent<EnemySwordControl>();
                if (currentShield > 0)
                    currentShield -= sword.FinalDamage();
                else
                    currentHealth -= sword.FinalDamage();
                fireTime = sword.fireDuration;
                fireDamage = sword.fireDamage;
                Transform opponent = other.transform.parent.parent;
                Vector3 knockbackDirection = opponent.position - transform.position;
                knockbackDirection.Normalize();
                GetComponent<Rigidbody>().AddForce(knockbackDirection);
                sword.hitState = HitState.Hit;
            }
            //Add multiplayer?
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Sword")
                other.isTrigger = false;
        }

        void Fire()
        {
            if (fireTime > 0)
            {
                if (currentShield > 0)
                    currentShield -= fireDamage * Time.deltaTime / 2;
                else
                    currentHealth -= fireDamage * Time.deltaTime;

                fireTime -= Time.deltaTime;
            }
        }

        void Death()
        {
            if (currentHealth <= 0)
                Destroy(gameObject);
        }

        void BonusControl()
        {
            foreach (var key in bonusAtHealth)
            {
                if (currentHealth / maxHealth < key.Value)
                {
                    switch (key.Key)
                    {
                        case HealthBonusType.Regen:
                            healthRegenRate += bonusStrength[HealthBonusType.Regen];
                            break;
                        case HealthBonusType.Damage:
                            sword.addedDamage += bonusStrength[HealthBonusType.Damage];
                            break;
                        case HealthBonusType.Resistance:
                            damageResistence += bonusStrength[HealthBonusType.Resistance];
                            break;
                        default:
                            break;
                    }
                    activeBonuses[key.Key] = true;
                }
                else if (activeBonuses[key.Key])
                {
                    activeBonuses[key.Key] = false;
                    switch (key.Key)
                    {
                        case HealthBonusType.Regen:
                            healthRegenRate -= bonusStrength[HealthBonusType.Regen];
                            break;
                        case HealthBonusType.Damage:
                            sword.addedDamage -= bonusStrength[HealthBonusType.Damage];
                            break;
                        case HealthBonusType.Resistance:
                            damageResistence -= bonusStrength[HealthBonusType.Resistance];
                            break;
                        default:
                            break;
                    }
                }
            }
        }

    }
}
