using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] Animation transtionImage;
    [SerializeField] GameObject pauseMenu;
    public void FadeToBlack()
    {
        transtionImage.Play("FadeToBlack");
    }
    public void FadeToTransparent()
    {
        transtionImage.Play("FadeToTransparent");
    }

    public void Pause()
    {
        if (pauseMenu != null)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }

    public void UnPause()
    {
        if (pauseMenu != null)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    public void ToStartMenu()
    {
        GameManager.instance.ReturnToStartMenu();
    }

    public void Restart()
    {
        GameManager.instance.Restart();
    }
}
