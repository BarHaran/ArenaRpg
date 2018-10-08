using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Properites
{

    public class EnemyHealth : MonoBehaviour
    {
        public float health;
        public float fireDamage;
        public float fireTime;
        public float shieldAmount;
        public float shieldRegenRate;
        public float healthRegenRate;
        public float damageResistence;
        public float fireResistence;
        public float stunDuration;
        public NavMeshAgent agent;

        // Use this for initialization
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            Fire();
            Death();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "PlayerSword")
            {
                PlayerSwordControl sword = other.GetComponent<PlayerSwordControl>();
                health -= sword.FinalDamage();
                fireTime = sword.fireDuration;
                fireDamage = sword.fireDamage;
                Transform opponent = other.transform.parent.parent;
                Vector3 knockbackDirection = opponent.position - transform.position;
                knockbackDirection.Normalize();
                GetComponent<Rigidbody>().AddForce(knockbackDirection);
                if (sword.IsShock()) stunDuration = sword.shockDuration;
            }
            else if (other.tag == "EnemySword")
            {
                EnemySwordControl sword = other.GetComponent<EnemySwordControl>();
                health -= sword.FinalDamage();
                Transform opponent = other.transform.parent.parent;
                Vector3 knockbackDirection = opponent.position - transform.position;
                knockbackDirection.Normalize();
                GetComponent<Rigidbody>().AddForce(knockbackDirection);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "PlayerSword" || other.tag == "EnemySword")
                other.isTrigger = false;
        }

        void Stun()
        {
            if (stunDuration > 0)
            {
                agent.enabled = false;
                stunDuration -= Time.deltaTime;
            }
            else agent.enabled = true;
        }

        void Fire()
        {
            if (fireTime > 0)
            {
                health -= fireDamage * Time.deltaTime;
                fireTime -= Time.deltaTime;
            }
        }

        void Death()
        {
            if (health <= 0)
                Destroy(gameObject);
        }
    }
}
