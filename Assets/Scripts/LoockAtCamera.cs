using UnityEngine;

public class LoockAtCamera : MonoBehaviour
{
    private enum Mode
    {
        LoockAt,
        LoockAtInverted,
        CameraForward,
        CameraForwardInverted
    }

    [SerializeField] private Mode mode;
    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LoockAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LoockAtInverted:
                transform.LookAt(transform.position + (transform.position - Camera.main.transform.position));
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
