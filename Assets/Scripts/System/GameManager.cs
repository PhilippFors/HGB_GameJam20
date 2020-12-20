using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] SceneLoadManager sceneLoad;
    [SerializeField] PlayerController player;
    [SerializeField] UIManager uiManager;
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        if (sceneLoad.GetScene().Equals("StartMenu"))
            StartCoroutine(StartLevel(true));
        else
            StartCoroutine(StartLevel(false));
    }

    IEnumerator StartLevel(bool isStartMenu)
    {
        uiManager.FadeToTransparent();
        yield return new WaitForSeconds(1f);
        if (!isStartMenu)
            player.TurnOn();
    }

    public void ReturnToStartMenu()
    {
        if (player != null)
            player.TurnOff();
        sceneLoad.ReturnToStartMenu();
    }


    public void NextLevel()
    {
        if (player != null)
            player.TurnOff();
        sceneLoad.LoadNextLevel();
    }

    public void Restart()
    {
        if (player != null)
            player.TurnOff();
        sceneLoad.RestartLevel();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
