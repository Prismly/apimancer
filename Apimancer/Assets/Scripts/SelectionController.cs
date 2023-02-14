using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionController : MonoBehaviour
{
    private Controls _controls;
    private Camera _camera;
    private bool _clickLeft = false;
    private bool _dragLeft = false;
    private bool _clickRight = false;
    private bool _dragRight = false;
    // private bool _clickMiddle = false;
    // private bool _dragMiddle = false;
    private bool _ctrlHold = false;
    private bool _shiftHold = false;
    private bool _altHold = false;
    private Vector2 _cursorPosition;
    private Vector2 _clickLeftOrigin;
    private Vector2 _clickRightOrigin;
    private Vector2 _clickMiddleOrigin;

    private Vector3 _focusDifference;

    private void Awake()
    {
        _controls = new Controls();
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        // _cursorPosition = _camera.ScreenToWorldPoint(_controls.player.cursor.ReadValue<Vector2>());
    }
    private void FixedUpdate()
    {
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
        if (_dragLeft)
        {
            _dragLeft = false;
            if (_ctrlHold)
            {
                SelectionManager.Instance.SelectHovered();
            }
            else
            {
                SelectionManager.Instance.SelectMoreHovered();
            }
        }
        else
        {
            if (_ctrlHold)
            {
                SelectionManager.Instance.SelectAnother();
            }
            else
            {
                SelectionManager.Instance.SelectOne();
            }
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
        HashSet<Selectable> selected = SelectionManager.Instance.Selected;

        Task task = new Task();
        task.target = null;

        if (_dragRight)
        {
            task.destination = _clickRightOrigin;
            task.direction = (_cursorPosition - _clickRightOrigin).normalized;
            _dragRight = false;
        }
        else
        {
            task.destination = _cursorPosition;
            task.direction = Vector2.zero;
        }
        task.halt = true;
        task.repeat = _altHold;
        SelectionManager.Instance.Assign(task, _shiftHold);
    }

    private void CtrlDown(InputAction.CallbackContext context)
    {
        _ctrlHold = true;
    }
    private void CtrlUp(InputAction.CallbackContext context)
    {
        _ctrlHold = false;
    }

    private void ShiftDown(InputAction.CallbackContext context)
    {
        _shiftHold = true;
    }
    private void ShiftUp(InputAction.CallbackContext context)
    {
        _shiftHold = false;
    }

    private void AltDown(InputAction.CallbackContext context)
    {
        _altHold = true;
    }
    private void AltUp(InputAction.CallbackContext context)
    {
        _altHold = false;
    }

    private void OnEnable()
    {
        _controls.Enable();
        _controls.player.clickLeft.started += MouseLeftDown;
        _controls.player.clickLeft.canceled += MouseLeftUp;
        _controls.player.clickRight.started += MouseRightDown;
        _controls.player.clickRight.canceled += MouseRightUp;
        _controls.player.ctrl.started += CtrlDown;
        _controls.player.ctrl.canceled += CtrlUp;
        _controls.player.shift.started += ShiftDown;
        _controls.player.shift.canceled += ShiftUp;
        _controls.player.alt.started += AltDown;
        _controls.player.alt.canceled += AltUp;
    }
    private void OnDisable()
    {
        _controls.Disable();
        _controls.player.clickLeft.started -= MouseLeftDown;
        _controls.player.clickLeft.canceled -= MouseLeftUp;
        _controls.player.clickRight.started -= MouseRightDown;
        _controls.player.clickRight.canceled -= MouseRightUp;
        _controls.player.ctrl.started -= CtrlDown;
        _controls.player.ctrl.canceled -= CtrlUp;
        _controls.player.shift.started -= ShiftDown;
        _controls.player.shift.canceled -= ShiftUp;
        _controls.player.alt.started -= AltDown;
        _controls.player.alt.canceled -= AltUp;
    }
}
