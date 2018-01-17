using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multicrate : MonoBehaviour
{
     public enum EBehaviour
    {
        Life,
        Grenade,
        AKM
    }

    [SerializeField] Color _lifeColor = new Color(0, 1, 0);
    [SerializeField] Color _ammunitionColor = new Color(0, 0, 1);

    [SerializeField] EBehaviour currentBehaviour = EBehaviour.Life;

    void Start()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material mat = mr.material;
        
        mat.SetColor("_Color", currentBehaviour > EBehaviour.Life ? _ammunitionColor : _lifeColor);

        if (currentBehaviour > EBehaviour.Life)
        {
            int childIndex = (int)currentBehaviour - 1;
            transform.GetChild(childIndex).gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (currentBehaviour)
        {
            case EBehaviour.Life:
                break;
            case EBehaviour.Grenade:
                break;
            case EBehaviour.AKM:
                break;
        }
    }
}
