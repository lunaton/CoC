using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class CharacterControllerMove : MonoBehaviour
{
    [Header("References")]
    [SerializeField] AudioClip[] Footsteps;
    [SerializeField] GameObject _pauseMenu;

    [Header("Values")]
    [SerializeField] float SpeedMovementWalk;
    [SerializeField] float SpeedMovementRun;
    [Range(0, 1)][SerializeField] float SpeedMovementInAir;
    [SerializeField] Vector2 SpeedRotation;
    [SerializeField] float JumpForce;
    [SerializeField] float Gravity = 9.8f;
    [SerializeField] float MaxGravityFall = 10.0f;

    AudioSource _audioSource;
    CharacterController _controller;
    Vector3 currentDirection;
    float _cameraPitch;

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (MainMenu.GAME_PAUSE) return;

        if (Input.GetButtonDown("Pause"))
        {
            ShowPauseMenu(true);
        }
    
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        Vector3 direction = Vector3.zero;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        _cameraPitch += -mouseY * SpeedRotation.y;
        _cameraPitch = Mathf.Clamp(_cameraPitch, -90f, 75f);
        Camera.main.transform.localRotation = Quaternion.Euler(_cameraPitch, 0, 0);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        direction = transform.forward * vertical
                  + transform.right * horizontal;


        float SpeedMovement = (Input.GetKey(KeyCode.LeftShift) && _controller.isGrounded)
                                ? SpeedMovementRun
                                : SpeedMovementWalk;

        currentDirection.x = direction.x * (_controller.isGrounded ? SpeedMovement : SpeedMovement * SpeedMovementInAir);
        currentDirection.z = direction.z * (_controller.isGrounded ? SpeedMovement : SpeedMovement * SpeedMovementInAir);

        if (Input.GetButtonDown("Jump") && _controller.isGrounded)
        {
            currentDirection.y = JumpForce;
        }

        currentDirection.y -= Gravity * Time.deltaTime;
        currentDirection.y = Mathf.Clamp(currentDirection.y, -MaxGravityFall, MaxGravityFall);


        _controller.Move(currentDirection * Time.deltaTime);
        transform.Rotate(Vector3.up * mouseX * SpeedRotation.x * Time.deltaTime);


        float magnitudeXZ = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
        if (magnitudeXZ > 0 && _audioSource.isPlaying == false && _controller.isGrounded)
        {
            int rndIndex = Random.Range(0, Footsteps.Length);
            _audioSource.clip = Footsteps[rndIndex];
            _audioSource.Play();
        }
    }

    public void ShowPauseMenu(bool isVisible)
    {
        MainMenu.GAME_PAUSE = isVisible;
        _pauseMenu.SetActive(isVisible);
        Time.timeScale = isVisible ? 0 : 1;
        Cursor.visible = isVisible;
        Cursor.lockState = isVisible ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void ReturnToMainMenu()
    {
        MainMenu.GAME_PAUSE = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    } 
}
