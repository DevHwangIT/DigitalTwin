using UnityEngine;

public class ScreenController : MonoBehaviour
{
    public float panSpeed = 0.5f;
    public float rotateSpeed = 5.0f;
    public float zoomSpeed = 10.0f;

    public float minFov = 15f;  // 최소 시야각 (줌인 최대치)
    public float maxFov = 90f;  // 최대 시야각 (줌아웃 최대치)

    private Vector3 lastMousePosition;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
            Debug.LogError("Camera component missing!");
    }

    void Update()
    {
        HandlePan();
        HandleRotate();
        HandleZoom();

        lastMousePosition = Input.mousePosition;
    }

    void HandlePan()
    {
        if (Input.GetMouseButton(0))  // 좌클릭으로 이동
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            Vector3 move = new Vector3(-delta.x * panSpeed * Time.deltaTime, -delta.y * panSpeed * Time.deltaTime, 0);
            transform.Translate(move, Space.Self);
        }
    }

    void HandleRotate()
    {
        if (Input.GetMouseButton(1))  // 우클릭으로 회전
        {
            float mouseX = Input.GetAxis("Mouse X") * rotateSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed;

            transform.eulerAngles += new Vector3(-mouseY, mouseX, 0);
        }
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            cam.fieldOfView -= scroll * zoomSpeed;
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minFov, maxFov);
        }
    }
}