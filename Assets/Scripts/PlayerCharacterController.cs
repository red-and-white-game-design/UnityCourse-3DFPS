using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    public static PlayerCharacterController instance;

    public Camera playerCamera;
    public float gravityDownForce = 20f;
    public float maxSpeedOnGround = 8f;
    public float moveSharpnessOnGround = 15f;
    public float rotationSpeed = 200f;

    public float cameraHeightRatio = 0.9f;

    private CharacterController _characterController;
    private PlayerInputHandler _playerInput;
    private float _targetCharacterheight = 1.8f;
    private float _cameraVerticalAngle = 0f;

    public Vector3 CharacterVelocity { get; set; } 

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInputHandler>();

        _characterController.enableOverlapRecovery = true;

        UpdateCharacterHeight();
    }

    void Update()
    {
        HandleCharacterMovement();
    }

    public void UpdateCharacterHeight()
    {
        _characterController.height = _targetCharacterheight;
        _characterController.center = Vector3.up * _characterController.height * 0.5f;

        playerCamera.transform.localPosition = Vector3.up * _characterController.height * 0.9f;
    }

    private void HandleCharacterMovement()
    {
        // Camera
        transform.Rotate(new Vector3(0, _playerInput.GetMouseLookHorizontal() * rotationSpeed, 0), 
            Space.Self);

        _cameraVerticalAngle += _playerInput.GetMouseLookVertical() * rotationSpeed;
        _cameraVerticalAngle = Mathf.Clamp(_cameraVerticalAngle, -89f, 89f);
        playerCamera.transform.localEulerAngles = new Vector3(-_cameraVerticalAngle, 0, 0);
        //move
        Vector3 worldSpaceMoveInput = transform.TransformVector(_playerInput.GetMoveInput());
        if (_characterController.isGrounded)
        {
            Vector3 targetVelocity = worldSpaceMoveInput * maxSpeedOnGround;

            CharacterVelocity = Vector3.Lerp(CharacterVelocity, targetVelocity,
                moveSharpnessOnGround*Time.deltaTime);
        }
        else
        {
            CharacterVelocity += Vector3.down * gravityDownForce * Time.deltaTime;
        }

        _characterController.Move(CharacterVelocity * Time.deltaTime);
    }
}
