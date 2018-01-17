using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKM : WeaponBase
{
    [SerializeField] GameObject defaultBulletHole;
    [SerializeField] GameObject muzzleFlashEffect;
    Transform _shootSource;

    protected override void Awake()
    {
        base.Awake();
        _shootSource = transform.Find("ShootSource");
    }

    protected override void OnSecondAction()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnShoot()
    {
        Destroy(Instantiate(muzzleFlashEffect, _shootSource), .5f);

        

        Vector3 screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(.5f, .5f));
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.PositiveInfinity))
        {
            Vector3 worldPoint = hit.point;
            Vector3 dirWorldPoint = (worldPoint - _shootSource.position).normalized;

            RaycastHit[] hits = Physics.RaycastAll(_shootSource.position, dirWorldPoint, 1000);
            for (int i = 0; i < hits.Length; i++)
            {
                if (i > 1) break;

                Damageable damageable = hits[i].collider.GetComponent<Damageable>();
                Rigidbody rigidbody = hits[i].collider.GetComponent<Rigidbody>();

                if (damageable)
                {
                    damageable.Damage(_damage);

                }
                if (rigidbody)
                {
                    rigidbody.AddForceAtPosition(hits[i].point, dirWorldPoint * 10, ForceMode.Impulse);
                }

                Quaternion rotation = Quaternion.LookRotation(hits[i].normal);
                GameObject bulletHoleParticles = damageable && damageable.GetOverrideBulletHole() ? damageable.GetOverrideBulletHole() : defaultBulletHole;
                GameObject instance = Instantiate(bulletHoleParticles, hits[i].point, rotation);
                instance.transform.SetParent(hits[i].transform);
                Destroy(instance, 3f);
            }
        }
    }
}
