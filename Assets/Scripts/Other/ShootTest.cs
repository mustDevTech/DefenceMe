using UnityEngine;
using Mst.Simple_Pool_Manager;
using System.Collections;
using System.Collections.Generic;

public class ShootTest : MonoBehaviour
{
    [Header("Place prefab here")]
    [SerializeField] private GameObject _itemPrefab;

    [Header("Shoot power")]
    [Range(1f,25f)] [SerializeField] private float _itemForce = 15f;

    [Header("Set spawn direction")]
    [SerializeField] private AxisToSpawn myAxis;
    private Vector3[] vectorAxises = new Vector3[5];

    private enum AxisToSpawn
    {
        None, Up, Down, Left, Right//, UpLeft,UpRight,DownLeft,DownRight
    }

    private void Start() 
    {
        vectorAxises[0] = Vector3.zero; //null
        vectorAxises[1] = Vector3.up;
        vectorAxises[2] = Vector3.down;
        vectorAxises[3] = Vector3.left;
        vectorAxises[4] = Vector3.right;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SpawnAndShoot();
        }
    }


    ///<summary>Gets enum names and sets spawn points spawn</summary>
    private Vector3 GetEnumAxis(AxisToSpawn axis)
    {
        return vectorAxises[(int)axis];
    }


    ///<summary>Spawns and shoots powerup items from spawner point</summary>
    private void SpawnAndShoot()
    {
        //GameObject bullet = SPManager.instance.GetNextAvailablePoolItem(_objectPoolName); //pool instead instantiate
        //bullet.transform.position = shootPoint.position;
        //bullet.transform.rotation = shootPoint.rotation;
        GameObject pwerupItem = Instantiate(_itemPrefab,transform.position,transform.rotation);

        //bullet.SetActive(true);
        Rigidbody2D itemRb = pwerupItem.GetComponent<Rigidbody2D>();
        itemRb.AddForce(GetEnumAxis(myAxis) * _itemForce, ForceMode2D.Impulse);
    }
}
