using UnityEngine;

public class VFXHandler : MonoBehaviour
{
    [SerializeField] private GameObject _damageBuffFX;
    [SerializeField] private GameObject _rangeBuffFX;
    [SerializeField] private GameObject _attackSpeedBuffFX;

    [SerializeField] private bool _isOff;

    public void SetFX(UpgradeTowerType type, bool active)
    {
        if(_isOff)
            return;

        switch (type)
        {
            case UpgradeTowerType.Damage:
                _damageBuffFX.SetActive(active);
                break;
            case UpgradeTowerType.Range:
                _rangeBuffFX.SetActive(active);
                break;
            case UpgradeTowerType.AttackSpeed:
                _attackSpeedBuffFX.SetActive(active);
                break;
            default:
                break;
        }
    }
}
