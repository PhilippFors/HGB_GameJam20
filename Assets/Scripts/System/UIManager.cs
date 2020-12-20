using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] Animation transtionImage;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] InputManager input;
    bool isPaused;
    private void Start()
    {
        if (input != null)
            input.inputControls.Gameplay.Pause.performed += ctx => TogglePause();
    }

    void TogglePause()
    {
        if (isPaused)
            UnPause();
        else
            Pause();
    }
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
            isPaused = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }

    public void UnPause()
    {
        if (pauseMenu != null)
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    public void ToStartMenu()
    {
        UnPause();
        GameManager.instance.ReturnToStartMenu();
    }

    public void Restart()
    {
        UnPause();
        GameManager.instance.Restart();
    }
}
