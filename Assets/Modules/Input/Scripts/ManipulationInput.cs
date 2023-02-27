using System;
using UnityEngine;

public class ManipulationInput : MonoBehaviour
{
    public event Action<Vector3> OnCameraMove;
    public event Action<float> OnCameraZoom;
    public event Action<Vector2> OnCameraRotate;

    void Update()
    {
        CameraMoveKeyboard();
        CameraZoomMouse();
        RotateCameraMouse();
    }

    private void CameraMoveKeyboard()
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        CameraMove(inputVector);
    }

    private void CameraMove(Vector3 inputVector)
    {
        OnCameraMove?.Invoke(inputVector);
    }

    private void CameraZoomMouse()
    {
        float amountScrollWheel = Input.GetAxis("Mouse ScrollWheel");
        CameraZoom(amountScrollWheel);
    }

    private void CameraZoom(float amountScrollWheel )
    {
        OnCameraZoom?.Invoke(amountScrollWheel);
    }

    private void RotateCameraMouse()
    {
        if (Input.GetMouseButton(2))
        {
            var rotateCamera = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")) ;
            RotateCamera(rotateCamera);
        }
    }

    private void RotateCamera(Vector2 rotate)
    {
        OnCameraRotate?.Invoke(rotate);
    }
}
