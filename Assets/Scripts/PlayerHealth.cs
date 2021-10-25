using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Mst.UI
{
public class PlayerHealth : MonoBehaviour
{
    private Slider _healthSlider;
    [Header("Debug")]
    [SerializeField] private int _maxHealth = 10;
    [SerializeField] private int _currentHealth;
    [Inject] private CoreManager z_coreManager; //zenjected

    [Header("Health bar color")]
    [SerializeField] private Gradient _gradient = null;
    [SerializeField] private Image _gradientFillImage;

#region PlayerHpHandle
    public int PlayerCurrentHp  
    {
        get=> _currentHealth;

        private set
        {
            _currentHealth = value;
            _healthSlider.value = _currentHealth;

            _gradientFillImage.color = _gradient.Evaluate(_healthSlider.normalizedValue);
            
            
            if(_currentHealth <= 0)
            {
                PlayerPrefs.SetInt("CurrentScore",0); //Обнуляем Действующие (оставить для наглядности). Удалить позже
                z_coreManager.SetGameCoreState(false); // Проигрыш
            }

            if(_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
                Debug.LogWarning($"<color=orange>Player_HP_Overset: </color>Player HP set to: {_currentHealth}");
            }
        }
    }
#endregion

    private void Awake()
    {
        _maxHealth = PlayerPrefs.GetInt("PlayerHealth");

        _healthSlider = transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).GetComponent<Slider>();
        _healthSlider.maxValue = _maxHealth;
        _currentHealth = _maxHealth;
        _healthSlider.value = _currentHealth;

        _gradientFillImage.color = _gradient.Evaluate(1f);
    }

    private void Update() // - + health points [<-->] REMOVE LATER!!!
    {
        if(Input.GetKeyDown(KeyCode.LeftBracket)) { this.PlayerCurrentHp -=1; }

        if(Input.GetKeyDown(KeyCode.RightBracket)) { this.PlayerCurrentHp +=1; }
    }
    

    ///<summary>Damages player by certain int value</summary>
    public void DamagePlayer(int damagePoints)
    {
        this.PlayerCurrentHp -=damagePoints;
    }


    ///<summary>Heals player by certain ammount value</summary>
    public void HealPlayer(int healPoints)
    {
        this.PlayerCurrentHp +=healPoints;
    }

}
}
