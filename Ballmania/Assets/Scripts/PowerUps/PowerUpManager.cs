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

    int temp = 0;

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
            if (temp == 0)
            {
                playerManager.acceleration += accelerationPowerUp;
                playerManager.maxVel += maxVelPowerUp;
                temp = 1;
            }
            StartCoroutine(SpeedUp());
        }

        else if(isMassUp == true)
        {
            StartCoroutine(SuperMass());
        }

        else if (isDashUp == true)
        {
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
        temp = 0;
        //GameObject.FindGameObjectWithTag("SpeedUpCached").transform.Translate(100, 100, 100);
        //Destroy(GameObject.FindWithTag("SpeedUpCached"));

    }
    

    public IEnumerator MultipleDash()
    {
        float cacheCooldown = playerManager.cooldown;
        playerManager.cooldown = cooldownPowerUp;
        yield return new WaitForSeconds(durationSuperDash);
        playerManager.cooldown = cacheCooldown;
        //GameObject.FindGameObjectWithTag("MultipleDash").transform.Translate(100, 100, 100);
        //Destroy(GameObject.FindWithTag("MultipleDash"));

    }

    public IEnumerator SuperMass()
    {
        transform.localScale += scaleVector;
        GetComponent<Rigidbody>().mass += superMass;
        playerManager.acceleration += superMassAcceleration;
        yield return new WaitForSeconds(durationSuperMass);
        transform.localScale -= scaleVector;
        GetComponent<Rigidbody>().mass -= superMass;
        playerManager.acceleration -= superMassAcceleration;
        //GameObject.FindGameObjectWithTag("SuperMass").transform.Translate(100, 100, 100);
        //Destroy(GameObject.FindWithTag("SuperMass"));

    }
}
