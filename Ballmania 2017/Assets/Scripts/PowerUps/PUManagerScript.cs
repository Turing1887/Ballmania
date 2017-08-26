using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUManagerScript : MonoBehaviour {

    // Bools für PowerUps
    [Header("Power Ups")]
    public bool isSpeedUp;
    public bool isMassUp;
    public bool isDashUp;
    // SpeedUp
    [Space]
    [Header("Speed")]
    public float durationSpeedUp;
    public float addAccelerationSpeedUp;
    public float addMaxSpeedUp;
    // DashUp
    [Space]
    [Header("Dash")]
    public float setValueNextDashSuperDash;
    public float durationSuperDash;
    // MassUp
    [Space]
    [Header("Mass")]
    public float durationSuperMass;
    public Vector3 addValuesForSuperMass;
    public float addMassSuperMass;
    public float addAccelerationSuperMass;

    float cacheCooldown;
    // Temp Vars
    int tempSpeed = 0;
    int tempMass = 0;
    int tempDash = 0;

    MovementManagerScript playerManager;

    void Awake()
    {
        isSpeedUp = false;
        isMassUp = false;
        isDashUp = false;
        playerManager = gameObject.GetComponent<MovementManagerScript>();
    }

    void Update()
    {
        if (isSpeedUp == true)
        {
            if (tempSpeed == 0)
            {
                playerManager.acceleration += addAccelerationSpeedUp;
                playerManager.maxVel += addMaxSpeedUp;
                tempSpeed = 1;
                StartCoroutine(SpeedUp());
            }
        }
        else
        {
            StopCoroutine(SpeedUp());
        }

        if (isMassUp == true)
        {
            if (tempMass == 0)
            {
                transform.localScale += addValuesForSuperMass;
                GetComponent<Rigidbody>().mass += addMassSuperMass;
                playerManager.acceleration += addAccelerationSuperMass;
                tempMass = 1;
                StartCoroutine(SuperMass());
            }
        }
        else
        {
            StopCoroutine(SuperMass());
        }

        if (isDashUp == true)
        {
            if (tempDash == 0)
            {
                cacheCooldown = playerManager.cooldown;
                playerManager.cooldown = setValueNextDashSuperDash;
                tempDash = 1;
                StartCoroutine(MultipleDash());
            }
        }
        else
        {
            StopCoroutine(MultipleDash());
        }
    }

    public IEnumerator SpeedUp()
    {
        yield return new WaitForSeconds(durationSpeedUp);
        playerManager.acceleration -= addAccelerationSpeedUp;
        playerManager.maxVel -= addMaxSpeedUp;
        isSpeedUp = false;
        tempSpeed = 0;
    }


    public IEnumerator MultipleDash()
    {
        yield return new WaitForSeconds(durationSuperDash);
        playerManager.cooldown = cacheCooldown;
        isDashUp = false;
        tempDash = 0;
    }

    public IEnumerator SuperMass()
    {
        yield return new WaitForSeconds(durationSuperMass);
        transform.localScale -= addValuesForSuperMass;
        GetComponent<Rigidbody>().mass -= addMassSuperMass;
        playerManager.acceleration -= addAccelerationSuperMass;
        isMassUp = false;
        tempMass = 0;
    }
}
