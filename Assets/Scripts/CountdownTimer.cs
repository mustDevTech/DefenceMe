using UnityEngine;
using UnityEngine.UI;
using Mst.Spawn;
using Zenject;

namespace Mst.UI
{
public class CountdownTimer : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private float _countdownTimer;
    
    [Header("Set start numer")]
    [SerializeField] private float _countdownNumber = 3f;
    
    private Text _countdownText;
    [Inject] private SpawnEnemy z_spawnEnemy; //zenjected

    private void Awake()
    {
        _countdownText = transform.GetChild(1).GetComponent<Text>();
        _countdownTimer = _countdownNumber;
        //Debug.Log(z_spawnEnemy);
    }
    
    private void Update()
    {
        CountDownStart();

        if(_countdownTimer < 0f)
        {
            _countdownTimer = _countdownNumber + 1;
            z_spawnEnemy.SpawnNewMob();
        }
    }

    private void CountDownStart()
    {
        _countdownText.text = Mathf.FloorToInt(_countdownTimer) + " сек";
        _countdownTimer -= Time.deltaTime;
    }
}
}
