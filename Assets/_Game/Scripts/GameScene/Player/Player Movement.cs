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
        if (TutorialManager.Instance.CanPlayerMove)
        {
            RotatePlayer(direction);
            MovePlayer(direction);
        }
        else
        {
            MovePlayer(Vector2.zero);
        }
    }

    public void AddSpeed(float speed) {
        _speed += speed;
    }

    private void MovePlayer(Vector2 direction)
    {
        _rb.velocity = direction * _speed;
    }

    private void RotatePlayer(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            if (TutorialManager.Instance.IsTutorialPlaying(TutorialID.Movement) ||
                TutorialManager.Instance.IsTutorialPlaying(TutorialID.Replacing))
            {
                TutorialEvents.OnPlayerMovedInvoke();
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.forward ,direction), Time.deltaTime * 20f);
            //transform.up = new Vector3(direction.x, direction.y, 0);
        }
    }
}
