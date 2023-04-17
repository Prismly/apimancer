using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _pitchSpeed;
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private float _dragMoveSpeed;
    [SerializeField] private float _dragRotateSpeed;
    [SerializeField] private float _dragPitchSpeed;
    [SerializeField] private float _scrollZoomSpeed;
    [SerializeField] private float _rotationMin;
    [SerializeField] private float _rotationMax;
    [SerializeField] private float _pitchMin;
    [SerializeField] private float _pitchMax;
    [SerializeField] private float _zoomMin;
    [SerializeField] private float _zoomMax;
    [SerializeField] private float _defaultRotation;
    [SerializeField] private float _defaultPitch;

    private Controls _controls;
    private Camera _camera;
    private bool _clickLeft = false;
    private bool _dragLeft = false;
    private bool _clickRight = false;
    private bool _dragRight = false;
    private bool _clickMiddle = false;
    // private bool _dragMiddle = false;
    // private bool _ctrlHold = false;
    private bool _shiftHold = false;
    // private bool _altHold = false;
    private Vector2 _cursorPosition;
    private Vector2 _clickLeftOrigin;
    private Vector2 _clickRightOrigin;
    private Vector2 _clickMiddleOrigin;

    private Vector2 _cameraMovement;
    private float _cameraZoom;
    private Vector2 _mouseDelta;
    private float _scrollDelta;
    private float _cameraRotation;
    private float _cameraPitch;
    private float _cameraScale;
    
    private Vector3 _focusDifference;

    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        _controls = new Controls();
        _camera = GetComponent<Camera>();
        _cameraRotation = _defaultRotation;
        _cameraPitch = -_defaultPitch;
        _cameraScale = 1;

        audioSource.Play();
    }

    //private void Start()
    //{
    //    Camera.main = transform.GetChild(0).GetComponent<Camera>();
    //}

    private void Update()
    {
        // _cursorPosition = _camera.ScreenToWorldPoint(_controls.player.cursor.ReadValue<Vector2>());
        _cameraMovement = _controls.player.move.ReadValue<Vector2>();
        _cameraZoom = _controls.player.zoom.ReadValue<float>();
        _mouseDelta += _controls.player.mouseDelta.ReadValue<Vector2>();
        _scrollDelta += _controls.player.scroll.ReadValue<Vector2>().y;
    }

    private void FixedUpdate()
    {
        Selectable focus = SelectionManager.Instance.Focused;

        if (_clickMiddle)
        {
            Vector3 positionDifference = Time.deltaTime * _cameraScale * _dragMoveSpeed * -_mouseDelta;
            positionDifference = Quaternion.Euler(0, 0, _cameraRotation) * positionDifference;
            this.transform.Translate(positionDifference, Space.World);
        }

        if (_clickRight)
        {
            this.transform.rotation = Quaternion.identity;
            float rotationDifference = Time.deltaTime * _dragRotateSpeed * -_mouseDelta.x;
            float pitchDifference = Time.deltaTime * _dragPitchSpeed * -_mouseDelta.y;
            // _cameraRotation += rotationDifference;
            _cameraRotation = Mathf.Clamp(_cameraRotation + rotationDifference, _rotationMin, _rotationMax);
            _cameraPitch = Mathf.Clamp(_cameraPitch + pitchDifference, -_pitchMax, -_pitchMin);
            this.transform.Rotate(_cameraRotation * Vector3.forward, Space.World);
            this.transform.Rotate(_cameraPitch * Vector3.right, Space.Self);
        }

        if (_shiftHold)
        {
            this.transform.rotation = Quaternion.identity;
            float rotationDifference = Time.deltaTime * _rotateSpeed * _cameraMovement.x;
            float pitchDifference = Time.deltaTime * _pitchSpeed * _cameraMovement.y;
            // _cameraRotation += rotationDifference;
            _cameraRotation = Mathf.Clamp(_cameraRotation + rotationDifference, _rotationMin, _rotationMax);
            _cameraPitch = Mathf.Clamp(_cameraPitch + pitchDifference, -_pitchMax, -_pitchMin);
            this.transform.Rotate(_cameraRotation * Vector3.forward, Space.World);
            this.transform.Rotate(_cameraPitch * Vector3.right, Space.Self);
        }
        else
        {
            Vector3 positionDifference = Time.deltaTime * _cameraScale * _moveSpeed * _cameraMovement;
            positionDifference = Quaternion.Euler(0, 0, _cameraRotation) * positionDifference;
            this.transform.Translate(positionDifference, Space.World);
        }

        float scaleDifference = Time.deltaTime * _zoomSpeed * _cameraZoom;
        _cameraScale = Mathf.Clamp(_cameraScale - scaleDifference, _zoomMin, _zoomMax);

        scaleDifference = Time.deltaTime * _scrollZoomSpeed * _scrollDelta;
        _cameraScale = Mathf.Clamp(_cameraScale - scaleDifference, _zoomMin, _zoomMax);

        this.transform.localScale = _cameraScale * Vector3.one;

        if (!_dragLeft && _clickLeft && _clickLeftOrigin != _cursorPosition)
            _dragLeft = true;

        if (!_dragRight && _clickRight && _clickRightOrigin != _cursorPosition) 
            _dragRight = true;

        _mouseDelta = Vector2.zero;
        _scrollDelta = 0;

        // Debug.Log(_controls.player.mouseDelta);
    }

    private void MouseLeftDown(InputAction.CallbackContext context)
    {
        _clickLeft = true;
        _clickLeftOrigin = _cursorPosition;
    }
    private void MouseLeftUp(InputAction.CallbackContext context)
    {
        _clickLeft = false;
        // Selectable focus = SelectionManager.Instance.Focused;
        // if (focus != null)
        // {
        //     Vector3 focusPosition = focus.transform.position;
        //     this.transform.position = new Vector3(focusPosition.x, focusPosition.y, this.transform.position.z);
        // }
    }

    private void MouseRightDown(InputAction.CallbackContext context)
    {
        _clickRight = true;
        Cursor.lockState = CursorLockMode.Locked;
        _clickRightOrigin = _cursorPosition;
    }
    private void MouseRightUp(InputAction.CallbackContext context)
    {
        _clickRight = false;
        if (!_clickMiddle)
            Cursor.lockState = CursorLockMode.None;
    }
    
    private void MouseMiddleDown(InputAction.CallbackContext context)
    {
        _clickMiddle = true;
        Cursor.lockState = CursorLockMode.Locked;
        _clickMiddleOrigin = _cursorPosition;
    }
    private void MouseMiddleUp(InputAction.CallbackContext context)
    {
        _clickMiddle = false;
        if (!_clickRight)
            Cursor.lockState = CursorLockMode.None;
    }

    // private void CtrlDown(InputAction.CallbackContext context)
    // {
    //     _ctrlHold = true;
    // }
    // private void CtrlUp(InputAction.CallbackContext context)
    // {
    //     _ctrlHold = false;
    // }

    private void ShiftDown(InputAction.CallbackContext context)
    {
        _shiftHold = true;
    }
    private void ShiftUp(InputAction.CallbackContext context)
    {
        _shiftHold = false;
    }

    // private void AltDown(InputAction.CallbackContext context)
    // {
    //     _altHold = true;
    // }
    // private void AltUp(InputAction.CallbackContext context)
    // {
    //     _altHold = false;
    // }

    private void OnEnable()
    {
        _controls.Enable();
        _controls.player.clickLeft.started += MouseLeftDown;
        _controls.player.clickLeft.canceled += MouseLeftUp;
        _controls.player.clickRight.started += MouseRightDown;
        _controls.player.clickRight.canceled += MouseRightUp;
        _controls.player.clickMiddle.started += MouseMiddleDown;
        _controls.player.clickMiddle.canceled += MouseMiddleUp;
        // _controls.player.ctrl.started += CtrlDown;
        // _controls.player.ctrl.canceled += CtrlUp;
        _controls.player.shift.started += ShiftDown;
        _controls.player.shift.canceled += ShiftUp;
        // _controls.player.alt.started += AltDown;
        // _controls.player.alt.canceled += AltUp;
    }
    private void OnDisable()
    {
        _controls.Disable();
        _controls.player.clickLeft.started -= MouseLeftDown;
        _controls.player.clickLeft.canceled -= MouseLeftUp;
        _controls.player.clickRight.started -= MouseRightDown;
        _controls.player.clickRight.canceled -= MouseRightUp;
        _controls.player.clickMiddle.started -= MouseMiddleDown;
        _controls.player.clickMiddle.canceled -= MouseMiddleUp;
        // _controls.player.ctrl.started -= CtrlDown;
        // _controls.player.ctrl.canceled -= CtrlUp;
        _controls.player.shift.started -= ShiftDown;
        _controls.player.shift.canceled -= ShiftUp;
        // _controls.player.alt.started -= AltDown;
        // _controls.player.alt.canceled -= AltUp;
    }
}
