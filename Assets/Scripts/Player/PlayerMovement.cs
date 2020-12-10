using UnityEngine;

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

    //Debug Values for Unity Editor
    private Vector3 _moveDirectionNorm = Vector3.zero;
    private Vector3 _playerVelocity = Vector3.zero;
    private float _playerTopVelocity = 0.0f;
    private bool _wishToJump = false;
    private float _playerFriction = 0.0f;

    private void Start()
    {
        HideAndFixCursor();
        SetupPlayerView();

        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CheckCursor();

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

    private void HideAndFixCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void CheckCursor()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            if (Input.GetButtonDown("Fire1"))
                Cursor.lockState = CursorLockMode.Locked;
        }
    }

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
        Vector3 wishdir;
        float accel;

        SetMovementDirection();

        wishdir = new Vector3(_movementCommand.right, 0, _movementCommand.forward);
        wishdir = transform.TransformDirection(wishdir);

        float wishspeed = wishdir.magnitude;
        wishspeed *= _moveSpeed;

        wishdir.Normalize();
        _moveDirectionNorm = wishdir;

        float wishspeed2 = wishspeed;

        accel = Vector3.Dot(_playerVelocity, wishdir) < 0 ? _airDecceleration : _airAcceleration;

        if (_movementCommand.forward == 0 && _movementCommand.right != 0)
        {
            if (wishspeed > _sideStrafeMaxSpeed)
            {
                wishspeed = _sideStrafeMaxSpeed;
            }
                
            accel = _sideStrafeAcceleration;
        }

        Accelerate(wishdir, wishspeed, accel);

        if (_airControlPrecision > 0)
        {
            AirControl(wishdir, wishspeed2);
        }

        _playerVelocity.y -= _gravity * Time.deltaTime;
    }

    private void AirControl(Vector3 wishdir, float wishspeed)
    {
        float zspeed;
        float speed;
        float dot;
        float multiplier;

        if (Mathf.Abs(_movementCommand.forward) < 0.001 || Mathf.Abs(wishspeed) < 0.001)
        {
            return;
        }

        zspeed = _playerVelocity.y;
        _playerVelocity.y = 0;

        speed = _playerVelocity.magnitude;
        _playerVelocity.Normalize();

        dot = Vector3.Dot(_playerVelocity, wishdir);
        multiplier = 32;
        multiplier *= _airControlPrecision * dot * dot * Time.deltaTime;

        if (dot > 0)
        {
            _playerVelocity.x = _playerVelocity.x * speed + wishdir.x * multiplier;
            _playerVelocity.y = _playerVelocity.y * speed + wishdir.y * multiplier;
            _playerVelocity.z = _playerVelocity.z * speed + wishdir.z * multiplier;

            _playerVelocity.Normalize();
            _moveDirectionNorm = _playerVelocity;
        }

        _playerVelocity.x *= speed;
        _playerVelocity.y = zspeed;
        _playerVelocity.z *= speed;
    }

    private void GroundMove()
    {
        Vector3 wishdir;

        if (!_wishToJump)
        {
            ApplyFriction(1.0f);
        } else
        {
            ApplyFriction(0);
        }   

        SetMovementDirection();

        wishdir = new Vector3(_movementCommand.right, 0, _movementCommand.forward);
        wishdir = transform.TransformDirection(wishdir);
        wishdir.Normalize();
        _moveDirectionNorm = wishdir;

        var wishspeed = wishdir.magnitude;
        wishspeed *= _moveSpeed;

        Accelerate(wishdir, wishspeed, _runAcceleration);

        _playerVelocity.y = -_gravity * Time.deltaTime;

        if (_wishToJump)
        {
            _playerVelocity.y = _jumpAcceleration;
            _wishToJump = false;
        }
    }

    private void ApplyFriction(float t)
    {
        Vector3 vec = _playerVelocity;
        float speed;
        float newspeed;
        float control;
        float drop;

        vec.y = 0.0f;
        speed = vec.magnitude;
        drop = 0.0f;

        if (_controller.isGrounded)
        {
            control = speed < _runDeacceleration ? _runDeacceleration : speed;
            drop = control * _groundFriction * Time.deltaTime * t;
        }

        newspeed = speed - drop;
        _playerFriction = newspeed;

        if (newspeed < 0)
        {
            newspeed = 0;
        }
            
        if (speed > 0)
        {
            newspeed /= speed;
        }

        _playerVelocity.x *= newspeed;
        _playerVelocity.z *= newspeed;
    }

    private void Accelerate(Vector3 wishdir, float wishspeed, float accel)
    {
        float addspeed;
        float accelspeed;
        float currentspeed;

        currentspeed = Vector3.Dot(_playerVelocity, wishdir);
        addspeed = wishspeed - currentspeed;

        if (addspeed <= 0)
        {
            return;
        }

        accelspeed = accel * Time.deltaTime * wishspeed;

        if (accelspeed > addspeed)
        {
            accelspeed = addspeed;
        }

        _playerVelocity.x += accelspeed * wishdir.x;
        _playerVelocity.z += accelspeed * wishdir.z;
    }
}
