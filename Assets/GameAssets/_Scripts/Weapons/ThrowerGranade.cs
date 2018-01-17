using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerGranade : WeaponBase
{
    [Header("ThrowerGranade")]
    [SerializeField]Granade _granadePrefab;
    [SerializeField]float _forceShoot;
    [SerializeField]float _forceUp;

    protected override void OnSecondAction()
    {
        throw new NotImplementedException();
    }

    protected override void OnShoot()
    {
        print("TrowerGranade::Bump!");
        Granade granade = Instantiate(_granadePrefab, transform.position, transform.rotation);
        Vector3 direction = (transform.forward + Vector3.up * _forceUp) * _forceShoot;
        granade.Throw(direction);
    }
}
