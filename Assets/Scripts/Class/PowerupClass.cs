using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mst.UI;

public class PowerupClass : MonoBehaviour
{
    [Header("Healing scriptable here")]
    [SerializeField] private HealScriptable _healPowerupSO;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().HealPlayer(_healPowerupSO.healPoints);
            Destroy(this.gameObject);
        }
    }
}
