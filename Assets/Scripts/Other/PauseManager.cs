using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

namespace Mst.UI
{
public class PauseManager : MonoBehaviour
{   
    private GameObject _pauseContainer;

    private void Awake()
    {
        _pauseContainer = transform.GetChild(0).gameObject;
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

///<summary>Pause the game</summary>
    public void PauseGame()
    {
        if(CoreManager.s_gameIsFinished == true)
        {
            Button cont = transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).GetComponent<Button>();
            cont.interactable = false;
        }
        _pauseContainer.SetActive(true);
        Time.timeScale = 0;
    }

///<summary>unPauses the game</summary>
    public void UnPauseGame()
    {
        _pauseContainer.SetActive(false);
        Time.timeScale = 1;
    }

///<summary>Reloads current game level</summary>
    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

///<summary>Returns to main menu </summary>
    public void ToMainMenu()
    {
        PlayerPrefs.DeleteAll();
        
        SceneManager.LoadScene("Menu");
        UnPauseGame();
    }

///<summary>Quits game</summary>
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
}