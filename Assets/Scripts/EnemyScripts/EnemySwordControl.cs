using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Properites
{
    public class EnemySwordControl : MonoBehaviour
    {
        #region Class properties
        public const float baseDamage = 15; //Base sword damage
        public const float baseSpeed = 6; //Base speed of sword swing (n swings per 10 seconds)

        public float addedDamage; //Damage added to base damage
        public float addedSpeed; //Speed added to base swing speed
        public float criticalRate; //Rate of critical attacks (%)
        public float criticalDamage; //Critical added damage (%) ((n + 1) * usualDamage)
        public float fireDamage; //Damage over a duration of time (stacks, decreases with time)
        public float fireDuration; //Duration of fire damage
        public float knockbackForce; //Power of sword knockback

        public float Speed
        {
            get { return baseSpeed + addedSpeed; }
        }

        public float FinalDamage()
        {
            float damage = baseDamage;
            damage += addedDamage;
            bool isCrit;
            if (criticalRate == 1)
                isCrit = true;
            else
                isCrit = Random.value < criticalRate; //Is the attack a critical
            if (isCrit)
                damage *= 1 + criticalDamage;
            return damage;

        }

        public HitState hitState;
        #endregion

        void Start()
        {

        }

        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "EnemySword" || collision.transform.tag == "PlayerSword")
            {
                hitState = HitState.Blocked;
            }
            if (collision.transform.tag == "Player")
            {
                GetComponent<Collider>().isTrigger = true;
                hitState = HitState.Hit;
            }
        }
    }
}
