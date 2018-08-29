using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Properites
{
    public class HealthProperties : MonoBehaviour
    {
        public float health;
        public float fireDamage;
        public float fireTime;

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
            if (other.tag == "Sword")
            {
                SwordProperties sword = other.GetComponent<SwordProperties>();
                health -= sword.FinalDamage();
                fireTime = sword.fireDuration;
                fireDamage = sword.fireDamage;
                Transform opponent = other.transform.parent.parent;
                Vector3 knockbackDirection = opponent.position - transform.position;
                knockbackDirection.Normalize();
                GetComponent<Rigidbody>().AddForce(knockbackDirection);
            }
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
