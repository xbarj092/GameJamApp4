using System;
using UnityEngine;

public class TowerBase<TInstance, TStats> : MonoBehaviour, ITowerBase
    where TInstance : TowerInstanceBase
    where TStats : TowerScriptable
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _ghostTower;
    [SerializeField] private InteractionControler _interaction;

    protected bool _pickedUp = false;

    public bool Highlighting;
    public TInstance Instance;
    public TStats Stats;

    public event Action OnTowerOutOfRange;
    private void OnTowerOutOfRangeInvoke()
    {
        OnTowerOutOfRange?.Invoke();
    }

    protected virtual void OnEnable()
    {
        _interaction.OnTowerOutOfRange += OnTowerOutOfRangeInvoke;
    }

    protected virtual void OnDisable()
    {
        _interaction.OnTowerOutOfRange -= OnTowerOutOfRangeInvoke;
    }

    public void Upgrade()
    {
    }

    public bool IsMaxLevel() => true;

    public void Highlight()
    {
        if (!Highlighting)
        {
            Highlighting = true;
            _renderer.material.SetInt("_Outlined", 1);
        }
    }

    public void Lowlight()
    {
        if (Highlighting)
        {
            Highlighting = false;
            _renderer.material.SetInt("_Outlined", 0);
        }
    }

    public GameObject GetGhostTower()
    {
        return _ghostTower;
    }

    public GameObject GetTowerObject()
    {
        return gameObject;
    }

    public bool IsInteractable() {
        return _interaction.Interactable;
    }

    public void IsPickedUp(bool pickedUp)
    {
        _pickedUp = pickedUp;
    }
}
