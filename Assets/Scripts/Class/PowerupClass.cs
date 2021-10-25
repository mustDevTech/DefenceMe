using UnityEngine;
using Mst.UI;

public class PowerupClass : MonoBehaviour
{
    [Header("Healing scriptable here")]
    [SerializeField] private HealScriptable _healPowerupSO;
    
    private void OnCollisionEnter2D(Collision2D collission) 
    {
        if(collission.gameObject.TryGetComponent(out PlayerHealth playerHp))
        {
            playerHp.HealPlayer(_healPowerupSO.healPoints);
            Destroy(this.gameObject);
        }
    }
}
