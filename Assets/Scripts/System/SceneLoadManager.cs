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
            StartCoroutine(LoadScene(levelBuildindex[currentLevel]));
        else
            ReturnToStartMenu();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(levelBuildindex[currentLevel]);
    }

    public void ReturnToStartMenu()
    {
        StartCoroutine(LoadScene(startMenu));
    }
    public void LoadSpecificScene(int i)
    {
        StartCoroutine(LoadScene(i));
    }
    public string GetScene()
    {
        return SceneManager.GetActiveScene().name;
    }
    IEnumerator LoadScene(int newScene)
    {
        uiManager.FadeToBlack();
        yield return new WaitForSeconds(1f);
        yield return SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Single);
    }
}
