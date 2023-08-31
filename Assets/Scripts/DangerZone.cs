using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    GameObject gmObject;
    GameManager gameManager;

    private bool incresePollution;

    private void Awake()
    {
        incresePollution = false;
        gmObject = GameObject.Find("GameManager");
        gameManager = gmObject.GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameManager.retoFinalMissions)
        {
            incresePollution = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameManager.retoFinalMissions)
        {
            incresePollution = false;
        }
    }

    private void Update()
    {
        if (incresePollution)
        {
            gameManager.OnPollution();
        }
        else
        {
            if (gameManager.retoFinalMissions)
            {
                gameManager.CleanPollution();
            }
        }
    }
}
