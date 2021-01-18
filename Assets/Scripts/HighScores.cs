using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{
    public Text highScoresText;
    private int[] highScoresArray = new int[10];

    private void Start()
    {
        highScoresArray = PlayerPrefsX.GetIntArray("HighScoreArray");

        if (highScoresArray[0] == 0)
        {
            highScoresText.text = "BRAK WYNIKÓW!";
        }
        else
        {
            highScoresText.text = "";

            for (int i = 0; highScoresArray[i] != 0; i++)
            {
                highScoresText.text += (i + 1) + ". " + highScoresArray[i] + "Pkt" + System.Environment.NewLine;

                if (i == 9)
                {
                    break;
                }
            }
        }
    }
}
