using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] SceneLoadManager sceneLoad;
    [SerializeField] UIManager uiManager;
    [SerializeField] PlayerController playerController;
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine(StartLevel());
    }

    IEnumerator StartLevel()
    {
        uiManager.FadeToTransparent();
        yield return new WaitForSeconds(1f);
        playerController.alive = true;
    }

    public void ReturnToStartMenu()
    {
        sceneLoad.ReturnToStartMenu();
    }

    public void NextLevel()
    {
        sceneLoad.LoadNextLevel();
    }

    public void Restart()
    {
        sceneLoad.RestartLevel();
    }
}
