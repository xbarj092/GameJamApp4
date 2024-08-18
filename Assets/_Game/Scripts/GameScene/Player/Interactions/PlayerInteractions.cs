using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private LayerMask _interact;
    [SerializeField] private float _maxRange = 5f;
    [SerializeField] private float _placementRadius = 0.35f;
    [SerializeField] private SpriteRenderer _interactionZone;
    [SerializeField] private GameInput _input;
    public float MaxRange { get { return _maxRange; } set { _maxRange = value; _interactionZone.transform.localScale = Vector3.one * _maxRange * 2; } }

    private ITowerBase _highlightedTower;
    private ITowerBase _carryingTower;
    private GameObject _ghostTower;
    private SpriteRenderer _ghostRenderer;

    private KeyboardInputHandler _keyboardInputHandler = new();
    private MouseInputHandler _mouseInputHandler;

    private void Awake()
    {
        _mouseInputHandler = new MouseInputHandler(this, _interact);
        _interactionZone.transform.localScale = Vector3.one * _maxRange * 2;

        _input = new GameInput();
        _input.Player.Interact.performed += _ => _mouseInputHandler.HandleInteraction();
        _input.Player.UI.performed += _ => _keyboardInputHandler.HandleInteraction();
    }

    private void OnEnable()
    {
        _mouseInputHandler.OnTowerPickedUp += OnTowerPickedUp;
        _mouseInputHandler.OnTowerPlaced += OnTowerPlaced;
        _mouseInputHandler.OnTowerHighlighted += OnTowerHighlighted;
        _mouseInputHandler.OnTowerLowlighted += OnTowerLowlighted;

        _input.Enable();
    }

    private void OnDisable()
    {
        _mouseInputHandler.OnTowerPickedUp -= OnTowerPickedUp;
        _mouseInputHandler.OnTowerPlaced -= OnTowerPlaced;
        _mouseInputHandler.OnTowerHighlighted -= OnTowerHighlighted;
        _mouseInputHandler.OnTowerLowlighted -= OnTowerLowlighted;

        _input.Disable();
    }

    private void Update()
    {
        if(Camera.main != null && ScreenManager.Instance.ActiveGameScreen == null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _mouseInputHandler.HandleMouseHover(mousePosition);
        }

        if (_ghostTower != null && ScreenManager.Instance.ActiveGameScreen == null)
        {
            SnapGhostToMousePosition();
            _ghostRenderer.material.SetInt("_Outlined" , CanBeTowerPlace() ? 0 : 1);
        }
    }

    private bool CanBeTowerPlace() {
        Vector3 placementPosition = _ghostTower.transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(placementPosition, _placementRadius, _interact);
        if(colliders.Length == 0) {
            return true;
        }

        return false;
    }

    public void OnTowerPickedUp(ITowerBase tower)
    {
        if (_carryingTower == null)
        {
            if (TutorialManager.Instance.IsTutorialPlaying(TutorialID.Replacing))
            {
                TutorialEvents.OnTowerPickedUpInvoke();
            }

            _carryingTower = tower;
            _carryingTower.GetTowerObject().SetActive(false);
            _ghostTower = Instantiate(tower.GetGhostTower(), transform);
            _ghostRenderer = _ghostTower.GetComponent<SpriteRenderer>();
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
                if (TutorialManager.Instance.IsTutorialPlaying(TutorialID.Core) ||
                    TutorialManager.Instance.IsTutorialPlaying(TutorialID.Replacing) ||
                    TutorialManager.Instance.IsTutorialPlaying(TutorialID.Upgrades))
                {
                    TutorialManager.Instance.TowerPosition = placementPosition;
                    TutorialEvents.OnTowerPlacedInvoke();
                }

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
