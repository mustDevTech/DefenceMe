using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mst.Main
{
public class MenuManager : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] int playerHp;
    [SerializeField] int playerDm;
    [Space(20)]
    [SerializeField] int mobsHp;
    [SerializeField] int mobsDm;
    [Space(10)]
    [SerializeField] int maxWinScore;


    void Awake()
    {
        PlayerPrefs.SetInt("PlayerHealth",0);
        PlayerPrefs.SetInt("PlayerDamage",0);

        PlayerPrefs.SetInt("MobsHealth",0);
        PlayerPrefs.SetInt("MobsDamage",0);

        PlayerPrefs.SetInt("WinScore",0);
    }

    public void StartGame(string nameLevel)
    {
        PlayerPrefs.SetInt("PlayerHealth",playerHp);
        PlayerPrefs.SetInt("PlayerDamage",playerDm);

        PlayerPrefs.SetInt("MobsHealth",mobsHp);
        PlayerPrefs.SetInt("MobsDamage",mobsDm);

        PlayerPrefs.SetInt("WinScore",maxWinScore);

        SceneManager.LoadScene(nameLevel);
    }

    public void OnHpChange(string hpPoints)
    {
        playerHp = int.Parse(hpPoints);
    }
    public void OnDmgChange(string dmgPoints)
    {
        playerDm = int.Parse(dmgPoints);
    }

    public void OnHpEnemy(string hpEnemy)
    {
        mobsHp = int.Parse(hpEnemy);
    }
    public void OnDmgEnemy(string dmgEnemy)
    {
        mobsDm = int.Parse(dmgEnemy);
    }

    public void OnChangeMaxScore(string maxScore)
    {
        maxWinScore = int.Parse(maxScore);
    }

}
}