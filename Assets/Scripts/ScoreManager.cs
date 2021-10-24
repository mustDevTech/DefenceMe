using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Mst.UI
{
public class ScoreManager : MonoBehaviour
{
    private Text scoreText;

    [Header("Debug")]
    [SerializeField] private int _scorePoints;
    [Inject] private CoreManager z_coreManager;

    private int ScorePoints
    {
        get=>_scorePoints;

        set
        {
            if(_scorePoints < 0){_scorePoints = 0; print("ScorePoints <color=red>OVERSET</color> value set to: 0 !");}
            _scorePoints = value;
        }
    }

    void Awake()
    {
        scoreText = transform.GetChild(1).GetComponent<Text>();

        ScorePoints = 0;
        PlayerPrefs.SetInt("CurrentScore",ScorePoints);

        scoreText.text = ScorePoints.ToString();
    }

///<summary>Adds certain score points to the game</summary>
    public void AddScore(int scoreToPlus)
    {
        ScorePoints += scoreToPlus;
        UpdateScore();
    }

///<summary>Removes certain score points from the game</summary>
    public void MinusScore(int scoreToMinus)
    {
        ScorePoints -= scoreToMinus;
        UpdateScore();
    }

///<summary>Deletes score data from the playerPrefs</summary>
    public void DeleteScore()
    {
        PlayerPrefs.DeleteKey("CurrentScore");
        ScorePoints = 0;
        UpdateScore();
    }

///<summary>Updates score text UI elements</summary>
    private void UpdateScore()
    {
        scoreText.text = ScorePoints.ToString();
        PlayerPrefs.SetInt("CurrentScore",ScorePoints); //!ОПЦ. Смысл пихать и записывать карент скор

        if(ScorePoints >= PlayerPrefs.GetInt("WinScore"))
        {
            DeleteScore();
            z_coreManager.SetGameCoreState(true); //выигрыш
        }
    }
}
}
