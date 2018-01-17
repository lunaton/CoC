using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int _maxlife;
    [SerializeField] private bool _godMode;
    [SerializeField] private GameObject overrideBulletHole;

    [SerializeField] private HUDProgressbar _canvasLife;

    private int _currentLife;

    private void Start()
    {
        _currentLife = _maxlife;
    }

    public virtual bool Damage(int damage)
    {
        _currentLife -= damage;

        bool isDead = _currentLife <= 0;

        if (isDead)
        {
            OnDead();
        }
        UpdateHUD();
        return isDead;
    }

    protected virtual void OnDead()
    {
        print(name + ": He muerto!");
    }

    public GameObject GetOverrideBulletHole()
    {
        return overrideBulletHole;
    }

    private void UpdateHUD()
    {
        if(_canvasLife)
        {
            _canvasLife.setValue((float)_currentLife / _maxlife);
        }
    }
}
