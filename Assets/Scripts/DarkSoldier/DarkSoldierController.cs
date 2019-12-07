using System.Collections;
using UnityEngine;

public class DarkSoldierController : MonoBehaviour{
    [SerializeField] private float moveSpeed;
    [SerializeField] private int startingDarkSoldierHealth;
    private GameController _gameController;
    private int _darkSoldierHealth;
    

    private Rigidbody2D _rb;
    private Vector2 _movement;
    private void Start(){
        _darkSoldierHealth = startingDarkSoldierHealth;
        _gameController = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<GameController>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update(){
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        
    }

    private void FixedUpdate(){
        _rb.MovePosition(_rb.position + Time.fixedDeltaTime * moveSpeed * _movement);
    }

    public void DecreaseHp(int byAmount){
        _darkSoldierHealth -= byAmount;
        if (_darkSoldierHealth <= 0){
            _gameController.DarkSoldierDied();
            Destroy(gameObject);
        }
    }

    
}
