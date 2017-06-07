using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {
    public bool isSpeedUp;
    public bool isMassUp;
    public bool isDashUp;
    PlayerMovementRigid playerManager;
    public float durationSpeedUp;
    public float accelerationPowerUp;
    public float maxVelPowerUp;
    public float cooldownPowerUp;
    public float durationSuperDash;
    public float durationSuperMass;
    public Vector3 scaleVector;
    public float superMass;
    public float superMassAcceleration;

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
                playerManager.acceleration += accelerationPowerUp;
                playerManager.maxVel += maxVelPowerUp;
                tempSpeed = 1;
            }
            StartCoroutine(SpeedUp());
        }

        else if(isMassUp == true)
        {
            if (tempMass == 0)
            {
                transform.localScale += scaleVector;
                GetComponent<Rigidbody>().mass += superMass;
                playerManager.acceleration += superMassAcceleration;
                tempMass = 1;
            }
            StartCoroutine(SuperMass());
        }

        else if (isDashUp == true)
        {
            if (tempDash == 0)
            {
                cacheCooldown = playerManager.cooldown;
                playerManager.cooldown = cooldownPowerUp;
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
        playerManager.acceleration -= accelerationPowerUp;
        playerManager.maxVel -= maxVelPowerUp;
        isSpeedUp = false;
        tempSpeed = 0;
        //GameObject.FindGameObjectWithTag("SpeedUpCached").transform.Translate(100, 100, 100);
        //Destroy(GameObject.FindWithTag("SpeedUpCached"));

    }
    

    public IEnumerator MultipleDash()
    {
        yield return new WaitForSeconds(durationSuperDash);
        playerManager.cooldown = cacheCooldown;
        tempDash = 0;
        //GameObject.FindGameObjectWithTag("MultipleDash").transform.Translate(100, 100, 100);
        //Destroy(GameObject.FindWithTag("MultipleDash"));

    }

    public IEnumerator SuperMass()
    {
        yield return new WaitForSeconds(durationSuperMass);
        transform.localScale -= scaleVector;
        GetComponent<Rigidbody>().mass -= superMass;
        playerManager.acceleration -= superMassAcceleration;
        tempMass = 0;
        //GameObject.FindGameObjectWithTag("SuperMass").transform.Translate(100, 100, 100);
        //Destroy(GameObject.FindWithTag("SuperMass"));

    }
}
