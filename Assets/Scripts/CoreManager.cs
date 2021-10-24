using UnityEngine;

namespace Mst.UI
{
public class CoreManager : MonoBehaviour
{
    private static GameObject _winGameText;
    private static GameObject _looseGameText;
    [SerializeField] private GameObject _playerGo;

    private void Awake() 
    {
        _winGameText = transform.GetChild(0).gameObject;
        _looseGameText = transform.GetChild(1).gameObject;

        _winGameText.SetActive(false);
        _looseGameText.SetActive(false);

        _playerGo = GameObject.Find("Player");
    }

    public void SetGameCoreState(bool areWeWon)
    {
       if(areWeWon == true)
        {
            SetFinalGameState(_winGameText,0,Color.green);
            Debug.Log("<color=green>win!!!</color>");
        }
       else
        {
            SetFinalGameState(_looseGameText,0f,Color.red);
            Debug.Log("<color=red>lost!!!</color>");
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