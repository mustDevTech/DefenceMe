using UnityEngine;

namespace Mst.Main
{
public class MoveTowards : MonoBehaviour 
{
     private GameObject targetObject;
     private GameObject Player;
     [SerializeField] private float moveSpeed = 2f;

     [Header("Debug settings")]
     [SerializeField] private bool _canDebugRayOn = false;
 
     private void Start() 
     {
          Player = this.gameObject;
          targetObject = GameObject.FindGameObjectWithTag("Player");
     }
     private void Update ()
	{
         float step = moveSpeed * Time.deltaTime;
         Player.transform.position = Vector2.MoveTowards(Player.transform.position, targetObject.transform.position, step);
	
	}

     private void OnDrawGizmos()
     {
          if (targetObject != null && _canDebugRayOn == true)
          {
               // Draws a blue line from this transform to the target
               Gizmos.color = Color.yellow;
               Gizmos.DrawLine(transform.position, targetObject.transform.position);
          }
          
     }
}
}