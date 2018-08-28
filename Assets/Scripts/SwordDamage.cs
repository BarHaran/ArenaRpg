using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Properties;

namespace Properties
{
    /// <summary>
    /// Class for sword properties (damage, speed, crits, etc.)
    /// </summary>
    public class SwordProperties
    {
        public const float baseDamage = 25; //Base sword damage
        public const float baseSpeed = 8; //Base speed of sword swing (n swings per 10 seconds)

        public float addedDamage; //Damage added to base damage
        public float addedSpeed; //Speed added to base swing speed
        public float criticalRate; //Rate of critical attacks (%)
        public float criticalDamage; //Critical added damage (%) ((n + 1) * usualDamage)
        public float oneShotRate; //Rate of which the damage becomes Infinite (%)
        public float shockRate; //Rate of which the attack stuns the opponent (%)
        public float fireDamage; //Damage over a duration of time (stacks, decreases with time)
        public float knockbackForce; //Power of sword knockback
        public int comboAttack; //Number of consecutive attacks without missing
        public float comboDamage; //Damage added through the combo;

        public SwordProperties()
        {
            addedDamage = 40;
            addedSpeed = 2;
            criticalRate = 0.2f;
            criticalDamage = 1.5f;
            oneShotRate = 0.05f;
            shockRate = 0.1f;
            fireDamage = 5;
            knockbackForce = 10;
            comboAttack = 0;
            comboDamage = 20;
        }

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

        public override string ToString()
        {
            return addedDamage + ", " + addedSpeed + ", " + baseDamage + ", " + baseSpeed;
        }
    }
}

public class SwordDamage : MonoBehaviour
{
    enum HitState
    {
        Pending,
        Attacking,
        Hit
    }

    SwordProperties properties;
    bool attacking;
    bool attackPending;
    HitState hit;
    bool blocking;

    Transform arm;

    // Use this for initialization
    void Awake()
    {
        attacking = false;
        attackPending = true;
        hit = HitState.Pending;
        blocking = false;
        properties = new SwordProperties();
        arm = transform.GetChild(0);
    }

    void Block()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            blocking = true;
            if (attacking)
            {
                if (hit == HitState.Attacking)
                {
                    hit = HitState.Pending;
                    properties.ResetCombo();
                }
                attacking = false;
            }
            transform.localEulerAngles = new Vector3(Mathf.LerpAngle(transform.localEulerAngles.x, -30, properties.Speed / 30), 0, 0);
            arm.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(arm.localEulerAngles.z, 90, 0.2f));
        }
        else
        {
            blocking = false;
            transform.localEulerAngles = new Vector3(Mathf.LerpAngle(transform.localEulerAngles.x, -10, properties.Speed / 30), 0, 0);
            arm.localEulerAngles = new Vector3(0, 0, Mathf.LerpAngle(arm.localEulerAngles.z, 0, 0.2f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Block();
    }

    public void OnSwordHit()
    {
        hit = HitState.Hit;
        
    }
}
