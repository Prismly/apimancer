using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _defaultRotation;
    [SerializeField] private float _defaultPitch;
    [SerializeField] private float _cameraMoveSpeed;
    [SerializeField] private float _cameraRotateSpeed;
    [SerializeField] private float _cameraPitchSpeed;
    [SerializeField] private float _cameraPitchMin;
    [SerializeField] private float _cameraPitchMax;
    [SerializeField] private float _cameraZoomSpeed;
    [SerializeField] private float _cameraZoomMin;
    [SerializeField] private float _cameraZoomMax;

    private Controls _controls;
    private Camera _camera;
    private bool _clickLeft = false;
    private bool _dragLeft = false;
    private bool _clickRight = false;
    private bool _dragRight = false;
    // private bool _clickMiddle = false;
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
    private float _cameraRotation;
    private float _cameraPitch;
    private float _cameraScale;
    
    private Vector3 _focusDifference;

    private void Awake()
    {
        _controls = new Controls();
        _camera = GetComponent<Camera>();
        _cameraRotation = _defaultRotation;
        _cameraPitch = -_defaultPitch;
        _cameraScale = 1;
    }

    private void Update()
    {
        // _cursorPosition = _camera.ScreenToWorldPoint(_controls.player.cursor.ReadValue<Vector2>());
        _cameraMovement = _controls.player.move.ReadValue<Vector2>();
        _cameraZoom = _controls.player.zoom.ReadValue<float>();
    }

    private void FixedUpdate()
    {

        Selectable focus = SelectionManager.Instance.Focused;

        if (_shiftHold)
        {
            this.transform.rotation = Quaternion.identity;
            float rotationDifference = Time.deltaTime * _cameraRotateSpeed * _cameraMovement.x;
            float pitchDifference = Time.deltaTime * _cameraPitchSpeed * _cameraMovement.y;
            _cameraRotation += rotationDifference;
            // _cameraPitch = Mathf.Clamp(_cameraPitch + pitchDifference, _cameraPitchMin, _cameraPitchMax);
            _cameraPitch = Mathf.Clamp(_cameraPitch + pitchDifference, -_cameraPitchMax, -_cameraPitchMin);
            this.transform.Rotate(_cameraRotation * Vector3.forward, Space.World);
            this.transform.Rotate(_cameraPitch * Vector3.right, Space.Self);
        }
        else
        {
            Vector3 positionDifference = Time.deltaTime * _cameraScale * _cameraMoveSpeed * _cameraMovement;
            positionDifference = Quaternion.Euler(0, 0, _cameraRotation) * positionDifference;
            this.transform.Translate(positionDifference, Space.World);
        }

        Vector3 cameraPosition = this.transform.position;

        float scaleDifference = Time.deltaTime * _cameraZoomSpeed * _cameraZoom;
        _cameraScale = Mathf.Clamp(_cameraScale - scaleDifference, _cameraZoomMin, _cameraZoomMax);
        this.transform.localScale = _cameraScale * Vector3.one;

        this.transform.position = cameraPosition;

        if (!_dragLeft && _clickLeft && _clickLeftOrigin != _cursorPosition)
            _dragLeft = true;

        if (!_dragRight && _clickRight && _clickRightOrigin != _cursorPosition) 
            _dragRight = true;
    }

    private void MouseLeftDown(InputAction.CallbackContext context)
    {
        _clickLeft = true;
        _clickLeftOrigin = _cursorPosition;
    }
    private void MouseLeftUp(InputAction.CallbackContext context)
    {
        _clickLeft = false;
        Selectable focus = SelectionManager.Instance.Focused;
        if (focus != null)
        {
            Vector3 focusPosition = focus.transform.position;
            this.transform.position = new Vector3(focusPosition.x, focusPosition.y, this.transform.position.z);
        }
    }

    private void MouseRightDown(InputAction.CallbackContext context)
    {
        _clickRight = true;
        _clickRightOrigin = _cursorPosition;
    }
    private void MouseRightUp(InputAction.CallbackContext context)
    {
        _clickRight = false;
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
        // _controls.player.ctrl.started -= CtrlDown;
        // _controls.player.ctrl.canceled -= CtrlUp;
        _controls.player.shift.started -= ShiftDown;
        _controls.player.shift.canceled -= ShiftUp;
        // _controls.player.alt.started -= AltDown;
        // _controls.player.alt.canceled -= AltUp;
    }
}
