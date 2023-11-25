using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool doMovement = true;

    [SerializeField] private float panSpeed = 30f;
    [SerializeField] private float zoomSpeed = 1000f;
    [SerializeField] private float minY = 10f;
    [SerializeField] private float maxY = 80f;

    [SerializeField] private float minX = 0f;
    [SerializeField] private float maxX = 100f;

    [SerializeField] private float minZ = -50f;
    [SerializeField] private float maxZ = 100f;

    void FixedUpdate()
    {
        if (GameManager.GameIsOver)
        {
            this.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            doMovement = !doMovement;

        if (!doMovement)
            return;

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y > Screen.height)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.fixedDeltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y < 0)
        {
            transform.Translate(Vector3.back * panSpeed * Time.fixedDeltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x > Screen.width)
        {
            transform.Translate(Vector3.right * panSpeed * Time.fixedDeltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x < 0)
        {
            transform.Translate(Vector3.left * panSpeed * Time.fixedDeltaTime, Space.World);
        }

        Vector3 pos = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX), 
            transform.position.y, 
            Mathf.Clamp(transform.position.z, minZ, maxZ)
        );
        transform.position = pos;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            pos = transform.position;
            pos.y -= scroll * zoomSpeed * Time.fixedDeltaTime;
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            transform.position = pos;
        }
    }
}
