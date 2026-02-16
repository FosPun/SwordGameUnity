using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadLevelByName(string levelName, float delay)
    {
        StartCoroutine(LoadLevelCoroutine(levelName, delay));
    }

    private IEnumerator LoadLevelCoroutine(string levelName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(levelName);
    }
}
