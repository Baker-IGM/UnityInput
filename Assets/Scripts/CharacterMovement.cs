using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    #region Fields
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
    #endregion

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
        if(!GravitySensor.current.enabled)                      // Turn on the Gravity Sensor on the phone if it turned off
        {
            InputSystem.EnableDevice(GravitySensor.current);
        }
#endif

        //  Display the value of the player input to the HUD
        if (playerInputLabel != null)
            playerInputLabel.text = string.Format(k_PLAYER_INPUT_STR, playerInput.x, playerInput.y);

        //  Move the Chacter based on player input
        Move(playerInput);
    }

    /// <summary>
    /// Move the Charcater based on Input from the Player
    /// </summary>
    /// <param name="movement">The delta movement to apply to the Player</param>
    void Move(Vector3 movement)
    {
        //  Calculate a new position that is frame rate independant
        vehiclePosition += movement * speed * Time.deltaTime;

        //  Check if the new postion is off the screen
        WrapVechileAroundCamera();

        //  Apply the final new position to the Character
        transform.position = vehiclePosition;
    }

    /// <summary>
    /// Called when there is a change to the any of the Inputs set in InputActionMap.
    /// </summary>
    /// <param name="input">Data from the Input Device (Vector2)</param>
    public void OnMove(InputValue input)
    {
        //  Read the data from the Input System
        playerInput = input.Get<Vector2>();
    }

    /// <summary>
    /// Called when there is a change to the Orientation of the Input Device.
    /// </summary>
    /// <param name="input">Data from the Input Device (Vector3)</param>
    public void OnRotate(InputValue input)
    {
        //  Read the data from the Input System
        mobileGravity = input.Get<Vector3>();

        //  Find the amount of difference off of the set baseline axes
        playerInput.x = Vector3.Dot(Vector3.right, mobileGravity);
        playerInput.y = Vector3.Dot(Vector3.up, mobileGravity);

        //  Limit how far the Input Device needs to be Rotated
        Vector2.ClampMagnitude(playerInput, maxMobileRotationMag);
    }

    /// <summary>
    /// Wrap the Charcter around the screen if they go past the edge of the screen
    /// </summary>
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

    /// <summary>
    /// Move the Character back to it's starting Position
    /// </summary>
    public void Reset()
    {
        vehiclePosition = Vector3.zero;
    }
}
