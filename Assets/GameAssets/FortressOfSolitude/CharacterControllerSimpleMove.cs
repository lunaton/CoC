using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerSimpleMove : MonoBehaviour
{
    public float SpeedMovement;
    CharacterController _controller;

    float _bulletStartTime;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(Time.unscaledTime - _bulletStartTime >= 2)
        {
            Time.timeScale = 1;
        }

        if (Input.GetButtonDown("Jump"))
        {
            _bulletStartTime = Time.unscaledTime;
            Time.timeScale = .1f;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 speed = transform.forward * vertical
                      + transform.right * horizontal;

        _controller.SimpleMove(speed * SpeedMovement * Time.deltaTime);
    }


}
