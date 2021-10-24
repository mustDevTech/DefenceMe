using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mst.Main;
using Mst.UI;

public class UfoMob : ArcherMob
{
    [Header("Ufo Rotate")]
    [Range(5f,90f)] [SerializeField] private float _rotateAroundSpeed = 10; //degree per sec

    void Update()
    {
        if(Vector3.Distance(transform.position, playerTarget.position) < mobAttackDistance)
        {
            this.GetComponent<MoveTowards>().enabled = false;

            GameObject plr = GameObject.FindGameObjectWithTag("Player");
            transform.RotateAround(plr.transform.position,new Vector3(0,0,10), _rotateAroundSpeed * Time.deltaTime);
            
            AimAndFire();

        }
    }

}
