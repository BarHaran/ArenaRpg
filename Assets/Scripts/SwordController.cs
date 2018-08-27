using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProperties
{
    float damage;
    float critrate;
    float critdamage;
    float oneshot;

    public float GetPoison { get; private set; }

    public float GetSpeed { get; private set; }

    public SwordProperties(float damage, float speed, float critrate, float critdamage, float oneshot, float poison)
    {
        this.damage = damage;
        GetSpeed = speed;
        this.critrate = critrate;
        this.critdamage = critdamage;
        this.oneshot = oneshot;
        GetPoison = poison;
    }

    public float GetDamage
    {
        get
        {
            float final = 0;
            if (Random.value < oneshot)
            {
                final = float.PositiveInfinity;
                Debug.Log("One shot");
            }
            else if (Random.value < critrate)
            {
                final = damage * (1 + critdamage);
                Debug.Log("Crit");
            }
            else final = damage;
            Debug.Log(final);
            return final;
        }
    }


}

public class SwordController : MonoBehaviour
{
    bool attpending;
    public bool attacking;
    public SwordProperties properties;
    float speed;

    // Use this for initialization
    void Start()
    {
        attacking = false;
        attpending = true;
        properties = new SwordProperties(20, 5, 0.2f, 0.5f, 0.01f, 5);
        speed = properties.GetSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacking && Input.GetKey(KeyCode.Mouse0) && attpending)
            attacking = true;
        AttackControl();
    }

    void AttackControl()
    {
        if (attacking)
        {
            attpending = false;
            transform.localEulerAngles = new Vector3(Mathf.LerpAngle(transform.localEulerAngles.x, 90, speed / 10), 0, 0);
            if (transform.localEulerAngles.x > 89)
                attacking = false;
        }
        else if (!attpending)
        {
            if (transform.localEulerAngles.x > 1)
                transform.localEulerAngles = new Vector3(Mathf.LerpAngle(transform.localEulerAngles.x, 0, speed / 15), 0, 0);
            else
                attpending = true;
        }
    }

}
