using UnityEngine;

public class DarkSoldier : MonoBehaviour{
    [SerializeField] private float moveSpeed;

    private Rigidbody2D _rb;
    private Vector2 _movement;
    private void Start(){
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }


    private void Update(){
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate(){
        _rb.MovePosition(_rb.position + Time.fixedDeltaTime * moveSpeed * _movement);
    }
}
