using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonuses : MonoBehaviour
{
    [Header("Type of bonus")]
    public bool isDurability;
    public bool isShield;
    public bool isSpeed;
    
    [Header("Bonuses Settings")] 
    public float bonusSpeed = 10f;

    [Header("Durability Settings")] 
    public float repairPoints;
    
    [Header("Shield Settings")] 
    public GameObject shield;
    private GameObject playerCar;
    private Vector3 playerCarPos;
    
    [Header("Speed Settings")] 
    public float speedBoost;
    public float duration;
    private bool isActivated = false;

    private void Update()
    {
        this.gameObject.transform.Translate(new Vector3(0, -1, 0) * bonusSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "Player" || obj.gameObject.tag == "Shield")
        {
            if (isDurability == true)
            {
                obj.gameObject.GetComponent<PlayerCarMovement>().durability += repairPoints;
                Destroy(this.gameObject);
            }
            else if (isShield == true)
            {
                playerCar = GameObject.FindWithTag("Player");
                obj.gameObject.tag = "Shield";
                playerCarPos = playerCar.transform.position;
                playerCarPos.z = -0.1f;
                GameObject shieldObj = (GameObject)Instantiate(shield, playerCarPos, Quaternion.identity);
                shieldObj.transform.parent = playerCar.transform;
                Destroy(this.gameObject);
            }
            else if (isSpeed == true)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                isActivated = true;
                StartCoroutine("SpeedBoostActivated");
            }
        }
        else if (obj.gameObject.tag == "EndOfTheRoad" && isActivated == false)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator SpeedBoostActivated()
    {
        while (duration > 0)
        {
            duration -= Time.deltaTime / speedBoost;
            Time.timeScale = speedBoost;
            yield return null;
        }

        Time.timeScale = 1f;
        Destroy(this.gameObject);
    }
}
