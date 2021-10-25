using System.Collections;
using UnityEngine;
using Mst.UI;
using Mst.Simple_Pool_Manager;

public class EnemyDamage : MonoBehaviour
{
    [Header("Place scriptable objects here")]
    [SerializeField] private Stats stats; //Scriptable Obj
    [SerializeField] private PlayerHealth playerHealthPoint; //Instance
    private bool canTakeDamage = true;

    [Header("Govnokod settings")]
    [SerializeField] private bool isBomb = false;
    [SerializeField] private bool isMelee = false;

    private void Awake()
    {
        playerHealthPoint = InstancePlayer.instance.GetComponent<PlayerHealth>(); //Get instance Player instead FindGameObjectWithTag
    }

    private void OnTriggerStay2D(Collider2D other) //если моб коснулся игрока
    {
        if(other.gameObject.tag == "Player")
        {
            if(isBomb == true)
            {
                playerHealthPoint.DamagePlayer(PlayerPrefs.GetInt("MobsDamage")* stats.enemyDamagePoints);
                SPManager.instance.DisablePoolObject("BombMob_pool",transform);
            }

            if(isMelee == true && canTakeDamage == true)
            {
                StartCoroutine(WaitDamage());
                playerHealthPoint.DamagePlayer(PlayerPrefs.GetInt("MobsDamage")* stats.enemyDamagePoints);
            }
        }
    }

    IEnumerator WaitDamage()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(1f);
        canTakeDamage = true;
    }
}
