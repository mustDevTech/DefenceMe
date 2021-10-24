using UnityEngine;
using Mst.Simple_Pool_Manager;

public class Bullet : MonoBehaviour
{
    private void OnBecameInvisible() 
    {
        SPManager.instance.DisablePoolObject("BulletPool",this.transform);
    }

}
