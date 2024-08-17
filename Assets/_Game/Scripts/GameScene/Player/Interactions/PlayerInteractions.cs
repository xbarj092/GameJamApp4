using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private LayerMask _interact;
    [SerializeField] private float _maxRange = 5f;
    [SerializeField] private float _placementRadius = 0.5f;
    public float MaxRange => _maxRange;

    private ITowerBase _highlightedTower;
    private ITowerBase _carryingTower;
    private GameObject _ghostTower;

    private KeyboardInputHandler _keyboardInputHandler = new();
    private MouseInputHandler _mouseInputHandler;

    private void Awake()
    {
        _mouseInputHandler = new MouseInputHandler(this, _interact);
    }

    private void OnEnable()
    {
        _mouseInputHandler.OnTowerPickedUp += OnTowerPickedUp;
        _mouseInputHandler.OnTowerPlaced += OnTowerPlaced;
        _mouseInputHandler.OnTowerHighlighted += OnTowerHighlighted;
        _mouseInputHandler.OnTowerLowlighted += OnTowerLowlighted;
    }

    private void OnDisable()
    {
        _mouseInputHandler.OnTowerPickedUp -= OnTowerPickedUp;
        _mouseInputHandler.OnTowerPlaced -= OnTowerPlaced;
        _mouseInputHandler.OnTowerHighlighted -= OnTowerHighlighted;
        _mouseInputHandler.OnTowerLowlighted -= OnTowerLowlighted;
    }

    private void Update()
    {
        _keyboardInputHandler.HandleInteraction();
        _mouseInputHandler.HandleInteraction();

        if (_ghostTower != null)
        {
            SnapGhostToMousePosition();
        }
    }

    private void OnTowerPickedUp(ITowerBase tower)
    {
        if (_carryingTower == null)
        {
            _carryingTower = tower;
            _carryingTower.GetTowerObject().SetActive(false);
            _ghostTower = Instantiate(tower.GetGhostTower(), transform);
        }
    }

    private void OnTowerPlaced()
    {
        if (_carryingTower != null)
        {
            Vector3 placementPosition = _ghostTower.transform.position;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(placementPosition, _placementRadius, _interact);
            if (colliders.Length == 0)
            {
                _carryingTower.GetTowerObject().transform.position = placementPosition;
                _carryingTower.GetTowerObject().SetActive(true);
                _carryingTower = null;
                Destroy(_ghostTower);
            }
            else
            {
                Debug.Log("Cannot place tower here, another object is too close.");
            }
        }
    }

    private void OnTowerHighlighted(ITowerBase tower)
    {
        if (_highlightedTower == null)
        {
            _highlightedTower = tower;
            tower.Highlight();
        }
    }

    private void OnTowerLowlighted()
    {
        if (_highlightedTower != null)
        {
            _highlightedTower.Lowlight();
            _highlightedTower = null;
        }
    }

    private void SnapGhostToMousePosition()
    {
        if (Camera.main != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            Vector3 playerPosition = transform.position;

            Vector3 direction = mousePosition - playerPosition;
            float distance = direction.magnitude;

            if (distance > _maxRange)
            {
                direction = direction.normalized * _maxRange;
                mousePosition = playerPosition + direction;
            }

            _ghostTower.transform.position = mousePosition;
        }
    }
}
