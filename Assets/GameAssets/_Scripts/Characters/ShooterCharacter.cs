using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterCharacter : MonoBehaviour
{
    //Referencia a todas las armas que este personaje puede usar
    [SerializeField]WeaponBase[] _weapons;

    //Referencia al arma actual que este usando el personaje
    WeaponBase _currentWeapon;

    //Indice actual al arma que se este usando
    int _currentIndex;

    void SelectWeaponAtIndex(int index)
    {
        //0. Comprobar si [index] es una rango valido
        if (index >= Camera.main.transform.childCount && index >= _weapons.Length)
        {
            Debug.LogError("ShooterCharacter::SelectWeaponAtIndex() Index sale de rango");
            return;
        }

        //1.b Interar por todas las armas y activar la correcta segun el parametro index
        for(int i = 0; i < _weapons.Length; i++)
        {
            _weapons[i].gameObject.SetActive(false);
        }

        if (_weapons[index].unlocked) _weapons[index].gameObject.SetActive(true);
        _currentIndex = index;
        _currentWeapon = _weapons[index];
    }

    void SelectNextWeapon()
    {
        /*
         * 0 % 2 = 0
         * 1 % 2 = 1
         * 2 % 2 = 0
         * 3 % 2 = 1
         * 4 % 2 = 0
         */
        _currentIndex = ++_currentIndex % _weapons.Length;
        SelectWeaponAtIndex(_currentIndex);
    }

    void SelectBackWeapon()
    {
        _currentIndex = Math.Abs(--_currentIndex % _weapons.Length);
        SelectWeaponAtIndex(_currentIndex);
    }

    private void OnGUI()
    {
        if (MainMenu.GAME_PAUSE) return;
        int ascii = Event.current.character;
        int number = ascii - 49;
        if(number >= 0 && number <= 8)
        {
            SelectWeaponAtIndex(number);
        }
    }

    private void Update()
    {
        if (MainMenu.GAME_PAUSE) return;
        //Weapon selection from mouse wheel
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        if (mouseWheel > 0.075f)
        {
            SelectNextWeapon();
        }
        else if (mouseWheel < -0.075f)
        {
            SelectBackWeapon();
        }

        //Fire input
        if(Input.GetButton("Fire1"))
        {
            if (_currentWeapon)  _currentWeapon.Shoot(); 
        }
        if (Input.GetButton("Fire2"))
        {
            if (_currentWeapon)  _currentWeapon.SecondAction();
        }
    }

    private void Start()
    {
        //Debug.unityLogger.logEnabled = false;

        //Empezamos con todas las armas desactivadas
        for (int i = 0; i < _weapons.Length; i++)
        {
            _weapons[i].gameObject.SetActive(false);
        }
    }
}
