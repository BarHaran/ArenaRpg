using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Properites
{
    public enum HitState
    {
        Pending,
        Attacking,
        Hit,
        Blocked
    }

    public class SwordProperties : MonoBehaviour
    {
        #region Class properties
        public const float baseDamage = 25; //Base sword damage
        public const float baseSpeed = 8; //Base speed of sword swing (n swings per 10 seconds)

        public float addedDamage; //Damage added to base damage
        public float addedSpeed; //Speed added to base swing speed
        public float criticalRate; //Rate of critical attacks (%)
        public float criticalDamage; //Critical added damage (%) ((n + 1) * usualDamage)
        public float oneShotRate; //Rate of which the damage becomes Infinite (%)
        public float shockRate; //Rate of which the attack stuns the opponent (%)
        public float shockDuration; //Duration of shock
        public float fireDamage; //Damage over a duration of time (stacks, decreases with time)
        public float fireDuration; //Duration of fire damage
        public float knockbackForce; //Power of sword knockback
        public int comboAttack; //Number of consecutive attacks without missing
        public float comboDamage; //Damage added through the combo;

        public float Speed
        {
            get { return baseSpeed + addedSpeed; }
        }

        public float FinalDamage()
        {
            float damage = baseDamage;
            damage += addedDamage;
            if (comboAttack > 5)
                damage += Mathf.Pow(1.5f, comboAttack - 5) * comboDamage; //Added combo damage, the bigger the combo, the bigger the buff
            bool isCrit;
            if (criticalRate == 1)
                isCrit = true;
            else
                isCrit = Random.value < criticalRate; //Is the attack a critical
            if (isCrit)
                damage *= 1 + criticalDamage;
            bool isOneShot = Random.value < oneShotRate; //Is the attack a oneshot
            if (isOneShot)
                damage = float.PositiveInfinity;
            return damage;

        }

        public bool IsShock()
        {
            return Random.value < shockRate;
        }

        //Combo control
        public void ResetCombo()
        {
            comboAttack = 0;
        }
        public void AddCombo()
        {
            comboAttack++;
        }

        public HitState hit;
        #endregion

        void Start()
        {

        }

        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Sword")
            {
                hit = HitState.Blocked;
            }
            if (collision.transform.tag == "Enemy")
            {
                GetComponent<Collider>().isTrigger = true;
                hit = HitState.Hit;
                AddCombo();
            }
        }
    }
}
