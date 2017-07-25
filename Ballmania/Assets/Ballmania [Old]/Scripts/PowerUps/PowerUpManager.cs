using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {
    public bool isSpeedUp;
    public bool isMassUp;
    public bool isDashUp;
    PlayerMovementRigid playerManager;
    public float durationSpeedUp;
    public float addAccelerationSpeedUp;
    public float addMaxSpeedUp;
    public float setValueNextDashSuperDash;
    public float durationSuperDash;
    public float durationSuperMass;
    public Vector3 addValuesForSuperMass;
    public float addMassSuperMass;
    public float addAccelerationSuperMass;

    float cacheCooldown;

    int tempSpeed = 0;
    int tempMass = 0;
    int tempDash = 0;

    // Use this for initialization
    void Awake()
    {
        isSpeedUp = false;
        isMassUp = false;
        isDashUp = false;
        playerManager = gameObject.GetComponent<PlayerMovementRigid>();
    }
   
    // Update is called once per frame
    void Update () {
        if (isSpeedUp == true)
        {
            if (tempSpeed == 0)
            {
                playerManager.acceleration += addAccelerationSpeedUp;
                playerManager.maxVel += addMaxSpeedUp;
                tempSpeed = 1;
            }
            StartCoroutine(SpeedUp());
        }

        else if(isMassUp == true)
        {
            if (tempMass == 0)
            {
                transform.localScale += addValuesForSuperMass;
                GetComponent<Rigidbody>().mass += addMassSuperMass;
                playerManager.acceleration += addAccelerationSuperMass;
                tempMass = 1;
            }
            StartCoroutine(SuperMass());
        }

        else if (isDashUp == true)
        {
            if (tempDash == 0)
            {
                cacheCooldown = playerManager.cooldown;
                playerManager.cooldown = setValueNextDashSuperDash;
                tempDash = 1;
            }
            StartCoroutine(MultipleDash());
        }

        else {
            StopAllCoroutines();
        }
	}

    public IEnumerator SpeedUp()
    {
        yield return new WaitForSeconds(durationSpeedUp);
        playerManager.acceleration -= addAccelerationSpeedUp;
        playerManager.maxVel -= addMaxSpeedUp;
        isSpeedUp = false;
        tempSpeed = 0;
        //GameObject.FindGameObjectWithTag("SpeedUpCached").transform.Translate(100, 100, 100);
        //Destroy(GameObject.FindWithTag("SpeedUpCached"));

    }
    

    public IEnumerator MultipleDash()
    {
        yield return new WaitForSeconds(durationSuperDash);
        playerManager.cooldown = cacheCooldown;
        isDashUp = false;
        tempDash = 0;
        //GameObject.FindGameObjectWithTag("MultipleDash").transform.Translate(100, 100, 100);
        //Destroy(GameObject.FindWithTag("MultipleDash"));

    }

    public IEnumerator SuperMass()
    {
        yield return new WaitForSeconds(durationSuperMass);
        transform.localScale -= addValuesForSuperMass;
        GetComponent<Rigidbody>().mass -= addMassSuperMass;
        playerManager.acceleration -= addAccelerationSuperMass;
        isMassUp = false;
        tempMass = 0;
        //GameObject.FindGameObjectWithTag("SuperMass").transform.Translate(100, 100, 100);
        //Destroy(GameObject.FindWithTag("SuperMass"));

    }
}
