using UnityEngine;

public class PlayerInteractions :MonoBehaviour
{
    [SerializeField] private LayerMask _interact;

    private ITowerBase _carryingTower;
    private GameObject _ghostTower;

    private KeyboardInputHandler _keyboardInputHandler = new();
    private MouseInputHandler _mouseInputHandler;

    private void Awake()
    {
        _mouseInputHandler = new(_interact);
    }

    private void OnEnable()
    {
        _mouseInputHandler.OnTowerPickedUp += OnTowerPickedUp;
        _mouseInputHandler.OnTowerPlaced += OnTowerPlaced;
        _mouseInputHandler.OnTowerHighlighted += OnTowerHighlighted;
    }

    private void OnDisable()
    {
        _mouseInputHandler.OnTowerPickedUp -= OnTowerPickedUp;
        _mouseInputHandler.OnTowerPlaced -= OnTowerPlaced;
        _mouseInputHandler.OnTowerHighlighted -= OnTowerHighlighted;
    }

    private void Update()
    {
        _keyboardInputHandler.HandleInteraction();
        _mouseInputHandler.HandleInteraction();
    }

    private void OnTowerPickedUp(ITowerBase tower)
    {
        if (_carryingTower == null)
        {
            Debug.Log("Picking up");
            _carryingTower = tower;
            _carryingTower.GetTowerObject().SetActive(false);
            _ghostTower = Instantiate(tower.GetGhostTower(), transform);
        }
    }

    private void OnTowerPlaced()
    {
        if (_carryingTower != null)
        {
            Debug.Log("Placing");
            _carryingTower.GetTowerObject().SetActive(true);
            // update position
            _carryingTower = null;
            Destroy(_ghostTower);
        }
    }

    private void OnTowerHighlighted(ITowerBase tower)
    {
        tower.Highlight();
    }
}
