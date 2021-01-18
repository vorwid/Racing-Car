using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDurabilityManager : MonoBehaviour
{
    public GameObject playerCarPrefab;
    public GameObject spawnPoint;
    public TextMesh durabilityText;
    public GameObject[] hearts;
    public int lifes;
    public GameObject EndGameScreen;
    [HideInInspector] public int maxLifes;
    private GameObject playerCar;

    private void Start()
    {
        durabilityText.GetComponent<MeshRenderer>().sortingLayerName = "Durability";
        maxLifes = lifes;
        playerCar = (GameObject)Instantiate(playerCarPrefab, spawnPoint.transform.position, Quaternion.identity);
    }

    private void Update()
    {
        if (playerCar.GetComponent<PlayerCarMovement>().durability <= 0)
        {
            Destroy(playerCar);
            lifes--;
            Destroy(hearts[lifes]);

            if (lifes > 0)
            {
                StartCoroutine("SpawnaCar");
            }
            else if (lifes <= 0)
            {
                Time.timeScale = 0;
                EndGameScreen.SetActive(true);
            }
        }
        else if (playerCar.GetComponent<PlayerCarMovement>().durability > playerCar.GetComponent<PlayerCarMovement>().maxDurability)
        {
            playerCar.GetComponent<PlayerCarMovement>().durability = playerCar.GetComponent<PlayerCarMovement>().maxDurability;
        }

        durabilityText.text = "Durability: " + playerCar.GetComponent<PlayerCarMovement>().durability + "/" + playerCar.GetComponent<PlayerCarMovement>().maxDurability;
    }

    IEnumerator SpawnaCar()
    {
        playerCar = (GameObject)Instantiate(playerCarPrefab, spawnPoint.transform.position, Quaternion.identity);
        playerCar.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);
        playerCar.GetComponent<BoxCollider2D>().isTrigger = true;
        playerCar.tag = "Untouchable";
        yield return new WaitForSeconds(3);
        playerCar.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        playerCar.GetComponent<BoxCollider2D>().isTrigger = false;
        playerCar.tag = "Player";
    }
}
