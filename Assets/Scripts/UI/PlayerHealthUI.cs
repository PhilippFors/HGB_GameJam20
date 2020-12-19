using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthUI : MonoBehaviour
{
    public PlayerHealth player;
    public TMPro.TextMeshProUGUI ui;
    void Update()
    {
        ui.text = player.currentHealth.ToString();
    }
}
