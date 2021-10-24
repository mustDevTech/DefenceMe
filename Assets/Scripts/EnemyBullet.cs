using UnityEngine;
using Mst.UI;
using Zenject;

public class EnemyBullet : MonoBehaviour
{
    [Header("Place scriptable objects here")]
    [SerializeField] private Stats stats;
    [Space(15)]
    private PlayerHealth playerHealthPoints;
    private Rigidbody2D _bulletRb;
    private Vector2 moveDirection;
    [SerializeField] private float enemyBulletSpeed;
    [SerializeField] private Transform playerTarget;

    private void Start()
    {
        playerHealthPoints = GameObject.Find("Player").GetComponent<PlayerHealth>();

        _bulletRb = GetComponent<Rigidbody2D>();
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        moveDirection = (playerTarget.position - transform.position).normalized * enemyBulletSpeed;
        _bulletRb.velocity = new Vector2 (moveDirection.x, moveDirection.y);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player") // если пуля попала в игрока
        {
            playerHealthPoints.DamagePlayer(PlayerPrefs.GetInt("MobsDamage") * stats.enemyDamagePoints);
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "Bullet") // если пуля столкнулась с пулей игрока
        {
            if(Random.value < 0.25f) 
            {
                //Debug.Log("Enemy bullet hited");
                Destroy(gameObject);
            }
        }
    }
}
