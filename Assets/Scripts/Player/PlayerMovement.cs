using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    private Camera _playerView;
    [SerializeField]
    private float _playerViewYOffset = 0.6f;

    [Header("Environment")]
    [SerializeField]
    private float _gravity = 20.0f;
    [SerializeField]
    private float _groundFriction = 6;

    [Header("Movement")]
    [SerializeField]
    private float _moveSpeed = 7.0f;
    [SerializeField]
    private float _runAcceleration = 14.0f;
    [SerializeField]
    private float _runDeacceleration = 10.0f;
    [SerializeField]
    private float _airAcceleration = 2.0f;
    [SerializeField]
    private float _airDecceleration = 2.0f;
    [SerializeField]
    private float _airControlPrecision = 0.3f;
    [SerializeField]
    private float _sideStrafeAcceleration = 50.0f;
    [SerializeField]
    private float _sideStrafeMaxSpeed = 1.0f;
    [SerializeField]
    private float _jumpAcceleration = 8.0f;
    [SerializeField]
    private bool _holdJumpToBhop = false;

    private CharacterController _controller;
    private PlayerMovementCommands _movementCommand;
    private PhotonView _photonView;

    private Vector3 _playerVelocity = Vector3.zero;
    private float _playerTopVelocity = 0.0f;
    private bool _wishToJump = false;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();

        if (_photonView.IsMine)
        {
            _controller = GetComponent<CharacterController>();
        }
    }

    private void Start()
    {
        if (_photonView.IsMine)
        {
            //HideAndFixCursor();
            SetupPlayerView();
        }
    }

    private void Update()
    {
        if (_photonView.IsMine)
        {
            //CheckCursor();

            QueueJump();

            if (_controller.isGrounded)
                GroundMove();
            else if (!_controller.isGrounded)
                AirMove();

            _controller.Move(_playerVelocity * Time.deltaTime);

            Vector3 udp = _playerVelocity;
            udp.y = 0.0f;
            if (udp.magnitude > _playerTopVelocity)
                _playerTopVelocity = udp.magnitude;

            MovePlayerView();
        }
    }

    //private void HideAndFixCursor()
    //{
    //    Cursor.visible = false;
    //    Cursor.lockState = CursorLockMode.Locked;
    //}

    //private void CheckCursor()
    //{
    //    if (Cursor.lockState != CursorLockMode.Locked)
    //    {
    //        if (Input.GetButtonDown("Fire1"))
    //            Cursor.lockState = CursorLockMode.Locked;
    //    }
    //}

    private void SetupPlayerView()
    {
        if (_playerView == null)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
                _playerView = mainCamera;
        }

        MovePlayerView();
    }

    private void MovePlayerView()
    {
        _playerView.transform.position = new Vector3(
            transform.position.x,
            transform.position.y + _playerViewYOffset,
            transform.position.z);
    }

    private void SetMovementDirection()
    {
        _movementCommand.forward = Input.GetAxisRaw("Vertical");
        _movementCommand.right = Input.GetAxisRaw("Horizontal");
    }

    private void QueueJump()
    {
        if (_holdJumpToBhop)
        {
            _wishToJump = Input.GetButton("Jump");
            return;
        }

        if (Input.GetButtonDown("Jump") && !_wishToJump)
            _wishToJump = true;
        if (Input.GetButtonUp("Jump"))
            _wishToJump = false;
    }

    private void AirMove()
    {
        Vector3 wishDir;
        float accel;

        SetMovementDirection();

        wishDir = new Vector3(_movementCommand.right, 0, _movementCommand.forward);
        wishDir = transform.TransformDirection(wishDir);

        float wishSpeed = wishDir.magnitude;
        wishSpeed *= _moveSpeed;

        wishDir.Normalize();


        accel = Vector3.Dot(_playerVelocity, wishDir) < 0 ? _airDecceleration : _airAcceleration;

        if (_movementCommand.forward == 0 && _movementCommand.right != 0)
        {
            if (wishSpeed > _sideStrafeMaxSpeed)
            {
                wishSpeed = _sideStrafeMaxSpeed;
            }

            accel = _sideStrafeAcceleration;
        }

        _playerVelocity = PlayerMovementCalculations.CalculateAcceleration(_playerVelocity, wishDir, wishSpeed, accel);

        if (_airControlPrecision > 0)
        {
            _playerVelocity = PlayerMovementCalculations.CalculateAirControl(_playerVelocity, wishDir, _airControlPrecision, wishSpeed, _movementCommand.forward);
        }

        _playerVelocity.y -= _gravity * Time.deltaTime;
    }

    private void GroundMove()
    {
        Vector3 wishDir;

        if (!_wishToJump)
        {
            _playerVelocity = PlayerMovementCalculations.CalculateFricition(_playerVelocity, _runDeacceleration, _groundFriction, _controller.isGrounded, 1);
        }
        else
        {
            _playerVelocity = PlayerMovementCalculations.CalculateFricition(_playerVelocity, _runDeacceleration, _groundFriction, _controller.isGrounded, 0);
        }

        SetMovementDirection();

        wishDir = new Vector3(_movementCommand.right, 0, _movementCommand.forward);
        wishDir = transform.TransformDirection(wishDir);
        wishDir.Normalize();

        var wishSpeed = wishDir.magnitude;
        wishSpeed *= _moveSpeed;

        _playerVelocity = PlayerMovementCalculations.CalculateAcceleration(_playerVelocity, wishDir, wishSpeed, _runAcceleration);

        _playerVelocity.y = -_gravity * Time.deltaTime;

        if (_wishToJump)
        {
            _playerVelocity.y = _jumpAcceleration;
            _wishToJump = false;
        }
    }
}
