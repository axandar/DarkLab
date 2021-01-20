using Readonly;
using UnityEngine;
using UnityEngine.UI;

public class DarkSoldierController : MonoBehaviour{
    [SerializeField] private float moveSpeed;
    [SerializeField] private int startingDarkSoldierHealth;
    [SerializeField] private float healthBarYOffset;
    private Canvas _darkSoldierHealthCanvas;
    private Image _darkSoldierHealthBar;
    private GameController _gameController;
    private int _darkSoldierHealth;
    private Animator _animator;
    private SpriteRenderer _renderer;
    
    private Rigidbody2D _rb;
    private Vector2 _movement;
    private void Start() {
        InitializeFields();
        UpdateSoldierHealthbarDisplay();
    }

    private void InitializeFields() {
        _darkSoldierHealthCanvas = GameObject.FindGameObjectWithTag(Tags.DS_CANVAS).GetComponent<Canvas>();
        _darkSoldierHealthBar = GameObject.FindGameObjectWithTag(Tags.DS_IMAGE).GetComponent<Image>();
        _darkSoldierHealthCanvas.enabled = true;
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _darkSoldierHealth = startingDarkSoldierHealth;
        _gameController = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<GameController>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }
    
    private void Update(){
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        _darkSoldierHealthCanvas.transform.position = new Vector2(transform.position.x, transform.position.y + healthBarYOffset);
        
    }

    private void FixedUpdate(){
        if (_movement.magnitude > 0){
            _animator.SetBool("isMoving",true);
            _renderer.flipX = _movement.x > 0;
        }
        else{
            _animator.SetBool("isMoving",false);
        }
        _rb.MovePosition(_rb.position + Time.fixedDeltaTime * moveSpeed * _movement);
    }

    public void DecreaseHp(int byAmount){
        _darkSoldierHealth -= byAmount;
        UpdateSoldierHealthbarDisplay();
        if (_darkSoldierHealth <= 0){
            _gameController.DarkSoldierDied();
            _darkSoldierHealthCanvas.enabled = false;
            Destroy(gameObject);
        } else {
            _gameController.DarkSoldierDamaged();
        }
    }
    
    private void UpdateSoldierHealthbarDisplay() {
        _darkSoldierHealthBar.fillAmount = (float)_darkSoldierHealth / startingDarkSoldierHealth;
    }
}
