using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mst.UI;
using Mst.Main;
using Mst.Simple_Pool_Manager;

public class ArcherMob : EnemyClass
{
    [Header("Place")]
    [SerializeField] protected GameObject enemyBulletPrefab;
    [Range(0.3f,1.5f)] [SerializeField] protected float fireRate = 1f;
    protected float timeNextFire;
    [Range(1f,10f)] [SerializeField] protected float mobAttackDistance;
    protected Transform playerTarget;

    private void Start()
    {
        timeNextFire = Time.time;
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, playerTarget.position) < mobAttackDistance)
        {
            this.GetComponent<MoveTowards>().enabled = false;
            AimAndFire();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, mobAttackDistance);
    }

    protected void AimAndFire()
    {
        if(Time.time > timeNextFire)
        {
            Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
            timeNextFire = Time.time + fireRate;
        }
    }
}
