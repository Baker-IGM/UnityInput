using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    Vector3 vehiclePosition;

    [SerializeField]
    float speed;

    Vector3 cameraPosition;
    Vector2 halfCameraSize = Vector2.zero;

    Vector3 mobileGravity = Vector3.down;
    float maxMobileRotationMag = 1f;

    Vector2 playerInput = Vector2.zero;
    [SerializeField]
    Text playerInputLabel;
    const string k_PLAYER_INPUT_STR = "Player Input:\nX = {0}\nY = {1}";

    // Use this for initialization
    void Start()
    {
        vehiclePosition = transform.position;

        cameraPosition = Camera.main.transform.position;

        halfCameraSize.y = Camera.main.orthographicSize;
        halfCameraSize.x = halfCameraSize.y * Camera.main.aspect;

#if !UNITY_EDITOR && UNITY_ANDROID
        InputSystem.EnableDevice(GravitySensor.current);
#endif
    }

    // Update is called once per frame
    void Update()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        if(!GravitySensor.current.enabled)
        {
            InputSystem.EnableDevice(GravitySensor.current);
        }
#endif

        if (playerInputLabel != null)
            playerInputLabel.text = string.Format(k_PLAYER_INPUT_STR, playerInput.x, playerInput.y);

        Move(playerInput);
    }

    void Move(Vector3 movement)
    {
        vehiclePosition += movement * speed * Time.deltaTime;

        WrapVechileAroundCamera();

        transform.position = vehiclePosition;
    }

    public void OnMove(InputValue input)
    {
        playerInput = input.Get<Vector2>();
    }

    public void OnRotate(InputValue input)
    {
        mobileGravity = input.Get<Vector3>();

        playerInput.x = Vector3.Dot(Vector3.right, mobileGravity);
        playerInput.y = Vector3.Dot(Vector3.up, mobileGravity);

        Vector2.ClampMagnitude(playerInput, maxMobileRotationMag);
    }

    void WrapVechileAroundCamera()
    {
        //  Check if off the x side of screen
        if (vehiclePosition.x > cameraPosition.x + halfCameraSize.x)
        {
            vehiclePosition.x -= halfCameraSize.x * 2f;
        }
        else if (vehiclePosition.x < cameraPosition.x - halfCameraSize.x)
        {
            vehiclePosition.x += halfCameraSize.x * 2f;
        }

        //  Check if off the y side of screen
        if (vehiclePosition.y > cameraPosition.y + halfCameraSize.y)
        {
            vehiclePosition.y -= halfCameraSize.y * 2f;
        }
        else if (vehiclePosition.y < cameraPosition.y - halfCameraSize.y)
        {
            vehiclePosition.y += halfCameraSize.y * 2f;
        }
    }

    public void Reset()
    {
        vehiclePosition = Vector3.zero;
    }
}
