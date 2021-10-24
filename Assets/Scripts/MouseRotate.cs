using UnityEngine;

public class MouseRotate : MonoBehaviour
{
    private Camera _mainCamera;
    private Rigidbody2D _playerRigidBody;
    [SerializeField] private Vector2 _mousePosition;
    
    void Awake()
    {
        _playerRigidBody = gameObject.GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
    }
    void Update()
    {
        _mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
    
    void FixedUpdate()
    {
        Vector2 lookDir = _mousePosition - _playerRigidBody.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        _playerRigidBody.rotation = angle;
    }
}
