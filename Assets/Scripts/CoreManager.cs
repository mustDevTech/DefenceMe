using UnityEngine;

namespace Mst.UI
{
public class CoreManager : MonoBehaviour
{
    private GameObject _winGameText;
    private GameObject _looseGameText;
    [SerializeField] private GameObject _playerGo;
    
    public static bool s_gameIsFinished;

    private void Awake() 
    {
        _winGameText = transform.GetChild(0).gameObject;
        _looseGameText = transform.GetChild(1).gameObject;

        _winGameText.SetActive(false);
        _looseGameText.SetActive(false);

        s_gameIsFinished = false; 
    }

    private void Start() 
    {
        _playerGo = InstancePlayer.instance.gameObject;
    }

    public void SetGameCoreState(bool areWeWon)
    {
       if(areWeWon == true)
        {
            SetFinalGameState(_winGameText,0,Color.green);
            Debug.Log("<color=green>win!!!</color>");
            s_gameIsFinished = true;
        }
       else
        {
            SetFinalGameState(_looseGameText,0f,Color.red);
            Debug.Log("<color=red>lost!!!</color>");
            s_gameIsFinished = true;
        }
    }

    private void SetFinalGameState(GameObject winOrLooseText,float zVector, Color playerColor) //привт конструктор
    {
        winOrLooseText.SetActive(true); //даем ВИНТЕКСТ ИЛИ ЛУЗТЕКСТ
        _playerGo.transform.rotation = Quaternion.Euler(0,0,zVector); // задаем угол игрока на финале
        _playerGo.GetComponent<SpriteRenderer>().color = playerColor; // задаём цвет игрока на финале

        Time.timeScale = 0; // пауза игры 
    }
}
}