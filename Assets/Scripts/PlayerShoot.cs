using UnityEngine;
using Mst.Simple_Pool_Manager;

public class PlayerShoot : MonoBehaviour
{
    [Header("Shooting point")]
    [SerializeField] private Transform shootPoint;

    [Header("Bullet power")]
    [Range(5f,45f)] [SerializeField] private float _bulletForce = 15f;
    private float timeBeforeFire; //множитель времени перед выстрелом null
    
    [Header("Shots per one shot")]
    [Range(5f,45f)] [SerializeField] private float shotsPerTact = 10f;

    [Header("Debug Object pooling")]
    [SerializeField] private string _objectPoolName;

    private void Start() 
    {
        if(_objectPoolName == "")
        {
            Time.timeScale = 0;
            throw new UnassignedReferenceException();
        }
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && Time.time >= timeBeforeFire)
        {
            timeBeforeFire = Time.time +1/ shotsPerTact;
            ShootBullet();
        }
    }

    ///<summary>Creates gameObject out of ObjectPooler and add force power to it</summary>
    private void ShootBullet()
    {
        GameObject bullet = SPManager.instance.GetNextAvailablePoolItem(_objectPoolName); //pool instead instantiate
        bullet.transform.position = shootPoint.position;
        bullet.transform.rotation = shootPoint.rotation;

        bullet.SetActive(true);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(shootPoint.up * _bulletForce, ForceMode2D.Impulse);
    }
}
