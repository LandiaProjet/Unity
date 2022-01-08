using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  
    public void PlayGame(){
        SceneManager.LoadScene(1);
    }

    [SerializeField]
    private GameObject settingsPanel;
    
    public void OpenSettings(){
        settingsPanel.SetActive(true);
    }

    public void CloseSettings(){
        settingsPanel.SetActive(false);
    }
}
