using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoadManager : MonoBehaviour
{
    public int startMenu;
    public int[] levelBuildindex;
    public int currentLevel;
    [SerializeField] UIManager uiManager;
    [SerializeField] PlayerController player;
    public void LoadNextLevel()
    {
        currentLevel++;
        if (currentLevel < levelBuildindex.Length)
            StartCoroutine(LoadLevel(currentLevel));
        else
            ReturnToStartMenu();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentLevel);
    }

    public void ReturnToStartMenu()
    {
        StartCoroutine(LoadLevel(startMenu));
    }

    IEnumerator LoadLevel(int newLevel)
    {
        uiManager.FadeToBlack();
        player.alive = false;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(newLevel, LoadSceneMode.Single);
    }
}
