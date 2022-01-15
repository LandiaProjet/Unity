using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class LoadingTransition : MonoBehaviour
{
    float extraTime = 1;

    private void OnEnable()
    {

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void startLoading(float extraTime)
    {
        gameObject.SetActive(true);
        this.extraTime = extraTime;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(endLoading());
    }

    IEnumerator endLoading()
    {
        yield return new WaitForSeconds(extraTime);
        gameObject.SetActive(false);
    }
}
