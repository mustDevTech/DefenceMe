using UnityEngine;
using UnityEngine.UI;
using Mst.UI;
using Mst.Main;
using Mst.Simple_Pool_Manager;

public abstract class EnemyClass : MonoBehaviour
{
    [Header("Place scriptable objects here")]
    [SerializeField] protected Stats statsSO;

    [Header("Health debug")]
    [SerializeField] protected Slider healthSlider;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int _currentHealth;

#region Svoistvo
    protected int CurrentHealth
    {
        get => _currentHealth;
        
        private set
        {
            _currentHealth = value;
            healthSlider.value = _currentHealth;

            if(_currentHealth <= 0)
            {
                ScoreManager.instance.AddScore(1);
                SPManager.instance.DisablePoolObject(this.gameObject);

                _currentHealth = maxHealth;             //обнуление
                healthSlider.value = _currentHealth;    //обнуление
                this.GetComponent<MoveTowards>().enabled = true; //обнуление
            }
        }
        
    }
#endregion

    private void Awake() 
    {
        maxHealth = statsSO.enemyHealthPoints *10;
        _currentHealth = maxHealth;

        //_scoreManager = ScoreManager.instance.GetComponent

        healthSlider = transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Slider>(); //spaghetti code
        healthSlider.maxValue = maxHealth;
        healthSlider.value = _currentHealth;
    }

    private void OnTriggerEnter2D(Collider2D other) //enemy damage from bullet
    {
        if(other.gameObject.tag == "Bullet")
        {
            TakeDamage(PlayerPrefs.GetInt("PlayerDamage"));
            SPManager.instance.DisablePoolObject(other.gameObject); //remove Bullet
            
            //Debug.Log($"damaged : {this.name}");
        }
    }

    ///<summary>
    ///Applyies certain damage to enemy. Where damageValue is points
    ///</summary>
    protected void TakeDamage(int damageValue)
    {
        this.CurrentHealth -= damageValue;
    }

}
