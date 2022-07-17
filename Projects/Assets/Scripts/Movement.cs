using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Transform playerObj;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _minSpeed;
    [SerializeField] private Animator _anim;
    [SerializeField] private Vector3 _startFirstPersonCamPos;

    [Header("FirstPerson")]
    [SerializeField] private float _sensivityX;
    [SerializeField] private float _sensivityY;
    [SerializeField] private float _xRotation;
    [SerializeField] private float _yRotation;
    [SerializeField] private bool _isThirdPerson;
    [SerializeField] private bool _isHouseMode;
    [SerializeField] private CinemachineFreeLook _thirdPersonCam;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _rigidBody = transform.GetComponent<Rigidbody>();
        _anim = GetComponentInChildren<Animator>();

        if (_isHouseMode)
            _isThirdPerson = false;
        else
            _isThirdPerson = true;
    }

    private void Update()
    {
        if (_isThirdPerson)
        {
            ThirdPerson();
        }
        else if (!_isThirdPerson)
        {
            FirstPerson();
        }

        if (Input.GetKeyDown("q") && _anim.GetFloat("Speed") < 0.75f)
        {
            _anim.SetBool("Waving", true);
        }
        if (Input.GetKeyDown("c") && !_isHouseMode)
        {
            _isThirdPerson = !_isThirdPerson;
            _thirdPersonCam.gameObject.SetActive(_isThirdPerson);

        }
    }
    public void FirstPerson()
    {
        transform.forward = Camera.main.transform.forward;
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * _sensivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * _sensivityY;
        _yRotation += mouseX;
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -150f, 150f);
        Camera.main.transform.rotation = Quaternion.Euler(_xRotation, _yRotation + transform.eulerAngles.y, 0);
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        _rigidBody.AddForce(moveDirection.normalized * _moveSpeed, ForceMode.VelocityChange);
        SpeedControl();


    }

    public void ThirdPerson()
    {
        Vector3 lookDirection = transform.position - new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
        transform.forward = lookDirection.normalized;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        if (moveDirection != Vector3.zero)
        {
            _anim.SetBool("Waving", false);
            playerObj.forward = Vector3.Slerp(playerObj.forward, moveDirection, Time.deltaTime * _rotationSpeed);
            _anim.SetFloat("Speed", 0.75f);
        }
        else if (moveDirection == Vector3.zero)
        {
            _rigidBody.velocity = Vector3.zero;
            _anim.SetFloat("Speed", 0f);
        }

        //transform.position += moveDirection*Time.deltaTime* _moveSpeed;
        _rigidBody.AddForce(moveDirection.normalized * _moveSpeed, ForceMode.VelocityChange);
        SpeedControl();

    }
    public void SpeedControl()
    {
        Vector3 velocity = _rigidBody.velocity;//new Vector3(_rigidBody.velocity.x, _rigidBody.velocity.y, _rigidBody.velocity.z);

        if (velocity.magnitude > _maxSpeed)
        {
            Vector3 limitedVelocity = velocity.normalized * _maxSpeed;
            _rigidBody.velocity = new Vector3(limitedVelocity.x, velocity.y, limitedVelocity.z);

        }

    }
}