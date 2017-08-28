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

    //shorts to check how many of each Coroutines are running. Important for Reset
    private short CR_speed = 0;
    private short CR_mass = 0;
    private bool CR_dash = false;

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
            isSpeedUp = false;
            playerManager.acceleration += addAccelerationSpeedUp;
            playerManager.maxVel += addMaxSpeedUp;
            StartCoroutine(SpeedUp());
        }
        else
        {
            StopCoroutine(SpeedUp());
        }

        if (isMassUp == true)
        {
            isMassUp = false;
            transform.localScale += addValuesForSuperMass;
            GetComponent<Rigidbody>().mass += addMassSuperMass;
            playerManager.acceleration += addAccelerationSuperMass;
            StartCoroutine(SuperMass());
        }
        else
        {
            StopCoroutine(SuperMass());
        }

        if (isDashUp == true)
        {
            isDashUp = false;
            cacheCooldown = playerManager.cooldown;
            playerManager.cooldown = setValueNextDashSuperDash;
            StartCoroutine(MultipleDash());
        }
        else
        {
            StopCoroutine(MultipleDash());
        }
    }

    public IEnumerator SpeedUp()
    {
        CR_speed += 1;
        yield return new WaitForSeconds(durationSpeedUp);
        playerManager.acceleration -= addAccelerationSpeedUp;
        playerManager.maxVel -= addMaxSpeedUp;
        CR_speed -= 1;
    }


    public IEnumerator MultipleDash()
    {
        CR_dash = true;
        yield return new WaitForSeconds(durationSuperDash);
        playerManager.cooldown = cacheCooldown;
        CR_dash = false;
    }

    public IEnumerator SuperMass()
    {
        CR_mass += 1;
        yield return new WaitForSeconds(durationSuperMass);
        transform.localScale -= addValuesForSuperMass;
        GetComponent<Rigidbody>().mass -= addMassSuperMass;
        playerManager.acceleration -= addAccelerationSuperMass;
        CR_mass -= 1;
    }

    public void Reset()
    {
        if(CR_speed != 0)
        {
            playerManager.acceleration -= CR_speed * addAccelerationSpeedUp;
            playerManager.maxVel -= CR_speed * addMaxSpeedUp;
        }
        if(CR_mass != 0)
        {
            transform.localScale -= CR_mass * addValuesForSuperMass;
            GetComponent<Rigidbody>().mass -= CR_mass * addMassSuperMass;
            playerManager.acceleration -= CR_mass * addAccelerationSuperMass;
        }
        if(CR_dash)
        {
            playerManager.cooldown = cacheCooldown;
        }
        CR_speed = 0;
        CR_mass = 0;
        CR_dash = false;
            
    }
}
