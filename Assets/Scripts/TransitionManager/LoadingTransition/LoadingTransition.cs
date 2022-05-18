using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingTransition : MonoBehaviour
{
    float extraTime = 1;
    bool sceneLoad;
    
    public Slider slider;
    public GameObject icon;
    public TMPro.TextMeshProUGUI progressText;

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

    public void startLoadingLevel(float extraTime, bool sceneLoad = true, int levelIndex = 0)
    {
        this.sceneLoad = sceneLoad;
        this.extraTime = extraTime;
        gameObject.SetActive(true);
        if (InteractManager.instance)
            InteractManager.instance.InteractButton.SetActive(false);
        StartCoroutine(load(levelIndex));
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

    IEnumerator load(int levelIndex){
        yield return null;
        slider.value = 1;
        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelIndex);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            Debug.Log("Loading progress: " + (asyncOperation.progress * 100) + "%");
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.12f);
            progressText.text = (int)(progress * 100f) + "%";
            slider.value = asyncOperation.progress;
            icon.transform.position = new Vector3(((int)(progress * 100f)) / 100 * 600,20,0);
            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                asyncOperation.allowSceneActivation = true;
                slider.value = 1;
            }

            yield return null;
        }
    }
}
