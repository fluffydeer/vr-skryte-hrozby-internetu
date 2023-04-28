using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera characterCamera;
    [SerializeField] private CharacterController character;
    [SerializeField] private float defaultSpeed = 4;
    [SerializeField] private float cameraSpeed = 40;
    public float mouseSensitivity = 0.6f;
    private float currentSpeed;
    private float sprint = 8;

    private void Start()
    {
        SetUpCursor();
        SetUpMouseSensitivity();
    }

    private void Update()
    {
        MovePlayer();
    }

    private void SetUpCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void SetUpMouseSensitivity()
    {
        float sensitivity = PlayerPrefs.GetFloat("sensitivity");
        //not set in our local storage yet
        if (sensitivity == 0.0f)
        {
            this.mouseSensitivity = 0.6f;
        }
        else
        {
            this.mouseSensitivity = sensitivity;
        }
    }

    private static float SoftenMouseMovement(float value)
    {
        if (Mathf.Abs(value) > 1f)
        {
            float dampenMouse = 0.9f;
            return Mathf.Lerp(value, Mathf.Sign(value), dampenMouse);
        }
        return value;
    }

    private void MovePlayer()
    {
        //get user input
        var userX = Input.GetAxis("Horizontal");
        var userY = Input.GetAxis("Vertical");

        //rotate move vector in character direction
        var forward = new Vector3(userX, 0, userY);
        forward = character.transform.rotation * forward;

        //if length of move vector is bigger than 1, normalize it
        if (forward.magnitude > 1)
            forward = forward.normalized;

        //if holding shift set sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = defaultSpeed + sprint;
        }
        else
        {
            currentSpeed = defaultSpeed;
        }
        character.SimpleMove(forward * currentSpeed);

        //player mouse input for camera and character rotation
        var mouseX = Input.GetAxisRaw("Mouse X");
        var mouseY = Input.GetAxisRaw("Mouse Y");

        //set mouse sensitivity
        mouseX = SoftenMouseMovement(mouseX);
        mouseY = SoftenMouseMovement(mouseY);
        mouseX *= mouseSensitivity;
        mouseY *= mouseSensitivity;

        //rotate character
        character.transform.Rotate(Vector3.up, mouseX * cameraSpeed);

        //get camera rotation on x axis
        var currentRotationX = characterCamera.transform.localEulerAngles.x;
        currentRotationX += -mouseY * cameraSpeed;

        //limit camera movement to +-60 degrees on x axis
        if (currentRotationX < 180)
        {
            currentRotationX = Mathf.Min(currentRotationX, 60);
        }
        else if (currentRotationX > 180)
        {
            currentRotationX = Mathf.Max(currentRotationX, 300);
        }
        characterCamera.transform.localEulerAngles = new Vector3(currentRotationX, 0, 0);
    }
}