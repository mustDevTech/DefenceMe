using System.Collections.Generic;
using UnityEngine;
using Mst.Simple_Pool_Manager;
using Mst.UI;

namespace Mst.Spawn
{
public class SpawnEnemy : MonoBehaviour
{
    [Header("Lists of spawn prefabs & spawn points")]
    [SerializeField] private List<GameObject> _enemyPrefabs;
    [SerializeField] private List<Transform> _transformSpawnPoints;

    [Header("Lists of pool objects")]
    [SerializeField] private List<string> _mobNamesString = new List<string>{"InfantryMob_pool", "BombMob_pool", "ArcherMob_pool", "UfoMob_pool"};

    [Header("Debug")]
    [SerializeField] private int _randomMobID;
    [SerializeField] private int _randomPointID;

    [Header("Can we spawn something")]
    [SerializeField] private bool canSpawn = true; //убрать потом

    ///<summary>Spawn random enemy from random point. Instantiating!</summary>
    public void SpawnMob()
    {
        if(canSpawn == true)
        {
            _randomMobID = Random.Range(0,_enemyPrefabs.Count); // random enemy spawn
            _randomPointID = Random.Range(0,_transformSpawnPoints.Count); // random spawn points
        
            GameObject spawnedEnemy = Instantiate(_enemyPrefabs[_randomMobID], _transformSpawnPoints[_randomPointID]);
        }
    }

    ///<summary>Spawn random enemy from ObjectPool from random point. Object pooling!</summary>
    public void SpawnNewMob()
    {
        if(canSpawn == true)
        {
            _randomMobID = Random.Range(0,_enemyPrefabs.Count); //generates random enemy spawn from MIN to MAX
            _randomPointID = Random.Range(0,_transformSpawnPoints.Count); // random spawn points
        
            GameObject newEnemy = SPManager.instance.GetNextAvailablePoolItem(_mobNamesString[_randomMobID]);
            newEnemy.transform.position = _transformSpawnPoints[_randomPointID].position;
            newEnemy.transform.rotation = _transformSpawnPoints[_randomPointID].rotation;
            newEnemy.SetActive(true);
        }
    }


    ///<summary>Spawn certain enemy from certain point</summary>
    public void SpawnCertainMob(int mobId, int spawnerId)
    {
        //GameObject spawnedCertainEnemy = Instantiate(_enemyPrefabs[mobId], _transformSpawnPoints[spawnerId]);
        GameObject spawnedCertainEnemy = SPManager.instance.GetNextAvailablePoolItem(_mobNamesString[mobId]);
        
        spawnedCertainEnemy.transform.position = _transformSpawnPoints[spawnerId].position;
        spawnedCertainEnemy.transform.rotation = _transformSpawnPoints[spawnerId].rotation;
        spawnedCertainEnemy.SetActive(true);
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnCertainMob(0,1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpawnCertainMob(1,1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SpawnCertainMob(2,1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            SpawnCertainMob(3,1);
        }
    }
}
}
