using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    public GameObject ParticlesPrefab;
    public AudioClip ExplosionClip;
    public float ExplosionTime;
    public float RadiusDamage;

    private Rigidbody _rb;

    public void Throw(Vector3 direction)
    {
        _rb.AddForce(direction, ForceMode.Impulse);
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Invoke("Explode", ExplosionTime);
    }

    void Explode()
    {
        foreach (Collider c in Physics.OverlapSphere(transform.position, RadiusDamage, LayerMask.GetMask("Damageable")))
        {
            Damageable d = c.GetComponent<Damageable>();
            if (d)
            {
                if (d.Damage(10))
                {
                    print("Has matado a " + d.name + " con una granada");
                }
            }
        }

        if(ExplosionClip)
        {
            AudioSource.PlayClipAtPoint(ExplosionClip, transform.position);
        }

        if(ParticlesPrefab)
        {
            Destroy(Instantiate(ParticlesPrefab, transform.position, Quaternion.identity), 1f);
        }

        Destroy(gameObject);
    }
    
}
