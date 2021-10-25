using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Mst.Menu
{
public class MenuManager : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private Button startButton;
    [SerializeField] private ToggleGroup difficultyToggleGroup;
    [SerializeField] private Difficulties gameDifficulty;

    private enum Difficulties { Easy, Medium, Hard };

    private void FixedUpdate()
    {
        if(difficultyToggleGroup.AnyTogglesOn() != true)
        {
            startButton.interactable = false;
        }
        else
        { 
            startButton.interactable = true;
        }
    }

    public void StartGame(string nameLevel)
    {
        AssemblyDifficulty();
        SceneManager.LoadScene(nameLevel);
    }


    #region Difficulty
    public void SetEasyDifficulty(bool isOn)
    {
        if(isOn == true)
        {
            gameDifficulty = Difficulties.Easy;
        }
    }

    public void SetMediumDifficulty(bool isOn)
    {
        if(isOn == true)
        {
            gameDifficulty = Difficulties.Medium;
        }
    }

    public void SetHardDifficulty(bool isOn)
    {
        if(isOn == true)
        {
            gameDifficulty = Difficulties.Hard;
        }
    }
    #endregion

    #region DifficultuAssemblySettings
    private void AssemblyDifficulty()
    {
        switch (gameDifficulty)
        {
            case Difficulties.Easy:
            CreateSaves(10,2,5,1,10);
            break;
            case Difficulties.Medium:
            CreateSaves(15,1,10,1,20);
            break;
            case Difficulties.Hard:
            CreateSaves(20,1,15,2,25);
            break;
        }
    }

    private void CreateSaves(int hp, int dmg, int mobHp, int mobDmg, int maxScore)
    {
        PlayerPrefs.SetInt("PlayerHealth", hp);
        PlayerPrefs.SetInt("PlayerDamage", dmg);

        PlayerPrefs.SetInt("MobsHealth", mobHp);
        PlayerPrefs.SetInt("MobsDamage", mobDmg);

        PlayerPrefs.SetInt("WinScore", maxScore);
    }
    #endregion
}
}