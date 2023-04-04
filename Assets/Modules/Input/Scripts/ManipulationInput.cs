using System;
using UnityEngine;

public class ManipulationInput : MonoBehaviour
{
    public event Action<Vector3> CameraMoved;
    public event Action<Vector2> CameraRotated;
    public event Action<float> CameraZoomed;
    public event Action<bool> CameraBoosted;
    public event Action LeftMouseButtonUp;
    public event Action LeftMouseButtonDown;
    public event Action LeftMouseButtonDoubleClicked;
    public event Action<int> RotatedKeyboard;
    public event Action DeleteButtonClicked;
    public event Action RightMouseButtonClicked;

    private const float DOUBLE_CLICK_TIME = 0.2f;
    private float _lastClickTime;

    void Update()
    {
        CameraMoveKeyboard();
        CameraZoomMouse();
        RotateCameraMouse();
        CameraBoostKeyboard();
        LeftMouseButtonPressed();
        RotateKeyboard();
        DeleteButton();
        OnLeftMouseButtonUp();
        OnRightMouseButtonClicked();
    }

    private void CameraMoveKeyboard()
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        CameraMoved?.Invoke(inputVector);
    }

    private void CameraZoomMouse()
    {
        float amountScrollWheel = Input.GetAxis("Mouse ScrollWheel");
        CameraZoomed?.Invoke(amountScrollWheel);
    }

    private void RotateCameraMouse()
    {
        if (Input.GetMouseButton(2))
        {
            var rotateCamera = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")) ;
            CameraRotated?.Invoke(rotateCamera);
        }
    }

    private void CameraBoostKeyboard()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            CameraBoosted.Invoke(true);
        }
        else
        {
            CameraBoosted.Invoke(false);
        } 
    }

    private void LeftMouseButtonPressed()
    {
        if(Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonDown?.Invoke();

            float timeSinceLastClick = Time.time - _lastClickTime;

            if (timeSinceLastClick <= DOUBLE_CLICK_TIME) 
            {
                LeftMouseButtonDoubleClicked?.Invoke();
            }
            else
            {
                LeftMouseButtonDown?.Invoke();
            }

            _lastClickTime= Time.time;
        }
    }

    private void RotateKeyboard()
    {
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
           if(Input.GetKey(KeyCode.Q))
           {
                RotatedKeyboard?.Invoke(-1);
           }
           else
           {
                RotatedKeyboard?.Invoke(1);
           }
        }
        else
        {
            RotatedKeyboard?.Invoke(0);
        }
    }

    private void DeleteButton()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            DeleteButtonClicked?.Invoke();
        }
    }

    private void OnLeftMouseButtonUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            LeftMouseButtonUp?.Invoke();
        }
    }

    private void OnRightMouseButtonClicked()
    {
        if(Input.GetMouseButtonDown(1))
        {
            RightMouseButtonClicked?.Invoke();
        }
    }
}
