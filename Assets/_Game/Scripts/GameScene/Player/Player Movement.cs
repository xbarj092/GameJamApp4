using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameInput gameInput;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Rigidbody2D _rb;

    private void Awake()
    {
        gameInput = new GameInput();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        gameInput.Enable();
    }

    private void FixedUpdate()
    {
        Vector2 direction = gameInput.Player.Movement.ReadValue<Vector2>();
        RotatePlayer(direction);
        MovePlayer(direction);
    }

    private void MovePlayer(Vector2 direction)
    {
        _rb.velocity = direction * _speed;
    }

    private void RotatePlayer(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            transform.up = new Vector3(direction.x, direction.y, 0);
        }
    }
}