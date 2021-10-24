using System;
using UnityEngine;
using Mst.Simple_Pool_Manager;

namespace CreativeVeinStudio.Simple_Pool_Manager.Examples.Scripts
{
    public class DespawnAfterTime : MonoBehaviour
    {
        public float _despawnTime = 2;
        private float _timer = 0;

        private void OnDisable()
        {
            _timer = 0;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Wall"))
            {
                SPManager.instance.DisablePoolObject(gameObject);
            }
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if(_timer > _despawnTime)
            {
                _timer = 0;
                // Allows you to disable the pool object you are using
                // in this case the object is the same projectile it retrieved
                SPManager.instance.DisablePoolObject(gameObject);
            }
        }
    }
}
