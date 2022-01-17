using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingTransition : MonoBehaviour
{
    float extraTime = 1;
    bool sceneLoad;

    private void OnEnable()
    {
        if (sceneLoad == true)
            SceneManager.sceneLoaded += OnSceneLoaded;
        else
            StartCoroutine(endLoading());
    }

    private void OnDisable()
    {
        if (sceneLoad == true)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void startLoading(float extraTime, bool sceneLoad = true)
    {
        this.sceneLoad = sceneLoad;
        this.extraTime = extraTime;
        gameObject.SetActive(true);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("c bon");
        StartCoroutine(endLoading());
    }

    IEnumerator endLoading()
    {
        yield return new WaitForSeconds(extraTime);
        gameObject.SetActive(false);
    }
}
