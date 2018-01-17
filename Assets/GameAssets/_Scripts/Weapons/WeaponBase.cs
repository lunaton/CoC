using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("WeaponBase")]
    [SerializeField] float ShootRate;
    [SerializeField] protected int _damage;
    [SerializeField] private int _maxAmmunition;
    [SerializeField] private AudioClip _emptySFX;
    [SerializeField] private bool infinityAmmunition;
    public bool unlocked;



    private float _nextTimeShoot;
    private int _currentAmmunition;

    protected virtual void Awake()
    {
        _currentAmmunition = _maxAmmunition;
    }

    public virtual void Shoot()
    {
        if(Time.time > _nextTimeShoot && (_currentAmmunition > 0 || infinityAmmunition))
        {
            _nextTimeShoot = Time.time + ShootRate;
            OnShoot();
            _currentAmmunition--;
        }
        else if (_emptySFX)
        {
            AudioSource.PlayClipAtPoint(_emptySFX, transform.position);
        }
    }

    public virtual void SecondAction()
    {
        OnSecondAction();
    }
    
    public void AddAmmunition(int ammount)
    {
        _currentAmmunition = Mathf.Min(_maxAmmunition, _currentAmmunition + ammount);
    }

    public int GetCurrentAmmunition()
    {
        return _currentAmmunition;
    }

    protected abstract void OnShoot();
    protected abstract void OnSecondAction();

}

