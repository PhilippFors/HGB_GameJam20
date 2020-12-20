using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthUI : MonoBehaviour
{
    public PlayerHealth player;
    public Image[] healthPoints;

    public void UpdateUI(int playerHealth)
    {
        if (playerHealth >= 0)
            for (int x = 1; x <= healthPoints.Length; x++)
                if (x <= playerHealth)
                    healthPoints[x - 1].gameObject.SetActive(true);
                else
                    healthPoints[x - 1].gameObject.SetActive(false);
    }
}
