using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDProgressbar : MonoBehaviour
{
    [SerializeField] private Image _foreground;

    public void setValue(float value)
    {
        _foreground.fillAmount = value;
    }
}