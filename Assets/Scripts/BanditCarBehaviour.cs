using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditCarBehaviour : MonoBehaviour
{
    public GameObject bomb;
    public int bombsAmount;
    public int banditCarVerticalSpeed;
    public int banditCarHorizontalSpeed;
    public float bombDelay;
    [HideInInspector] public int pointsPerCar;

    private float Delay;
    private GameObject playerCar;
    private Vector3 banditCarPos;

    private void Start()
    {
        playerCar = GameObject.FindWithTag("Player");
        Delay = bombDelay;
    }

    private void FixedUpdate()
    {
        if (playerCar == null)
        {
            playerCar = GameObject.FindWithTag("Player");
        }
        else
        {
            banditCarPos = Vector3.Lerp(transform.position, playerCar.transform.position, Time.deltaTime * banditCarHorizontalSpeed);
            Mathf.Clamp(banditCarPos.x, -2.35f, 2.35f); 
            transform.position = new Vector3(banditCarPos.x, transform.position.y, 0);
        }
    }

    private void Update()
    {
        if (gameObject.transform.position.y > 3.8f && bombsAmount > 0)
        {
            this.gameObject.transform.Translate(new Vector3(0, -1, 0) * banditCarVerticalSpeed * Time.deltaTime);
        }
        else if (bombsAmount <= 0)
        {
            this.gameObject.transform.Translate(new Vector3(0, 1, 0) * banditCarVerticalSpeed * Time.deltaTime);

            if (gameObject.transform.position.y > 6.5f)
            {
                PointsManager.points += pointsPerCar;
                Destroy(this.gameObject);
            }
        }
        else
        {
            Delay -= Time.deltaTime;

            if (Delay <= 0 && bombsAmount > 0)
            {
                Delay = bombDelay;
                bombsAmount--;
                Instantiate(bomb, transform.position, Quaternion.identity);
            }
            else if (Delay <= 0 && bombsAmount <= 5 && bombsAmount > 0)
            {
                Delay = bombDelay / 2;
                bombsAmount--;
                Instantiate(bomb, transform.position, Quaternion.identity);
            }
        }
    }
}
