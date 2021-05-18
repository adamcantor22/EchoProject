using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MovementProvider : LocomotionProvider
{
    public float speed = 1.0f;
    public float gravityMultiplier = 1.0f;

    // GameObjects are set as null in the script, are defined in the Unity window
    public List<XRController> controllers = null;
    private CharacterController characterController = null;
    private GameObject head = null;

    protected override void Awake()
    {
        characterController = GetComponent<CharacterController>();
        head = GetComponent<XRRig>().cameraGameObject;
    }

    // On start, use position controller to update player position
    private void Start()
    {
        PositionController();
    }

    // Once a frame, use position controller to update player position, then
    // check for player input. Finally, apply gravity so that the player cannot
    // unintentionally start floating.
    private void Update()
    {
        PositionController();
        CheckForInput();
        ApplyGravity();
    }

    private void PositionController()
    {
        // Get the head in local, playspace ground
        // Additionally, clamp head so nobody can see over the maze by standing on a chair
        float headHeight = Mathf.Clamp(head.transform.localPosition.y, 1, 2);
        characterController.height = headHeight;

        // Cut in half, and make sure to account for characterController "skin"
        Vector3 newCenter = Vector3.zero;
        newCenter.y = characterController.height / 2;
        newCenter.y += characterController.skinWidth;

        // Set the new center to the the localPosition of the camera
        newCenter.x = head.transform.localPosition.x;
        newCenter.z = head.transform.localPosition.z;

        // Apply new center to the character controller
        characterController.center = newCenter;
    }

    // Check each controller in the "controllers" list and see if they are able
    // to provide input. If so, check for movement on that particular device.
    private void CheckForInput()
    {
        foreach(XRController controller in controllers)
        {
            if (controller.enableInputActions)
                CheckForMovement(controller.inputDevice);
        }
    }

    // If the primary2DAxis on the controller is a non-default value then have
    // the player start moving in the direction of that value
    private void CheckForMovement(InputDevice device)
    {
        if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 position))
            StartMove(position);
    }

    private void StartMove(Vector2 position)
    {
        // Apply the touch position to the head's forward Vector
        Vector3 direction = new Vector3(position.x, 0, position.y);
        Vector3 headRotation = new Vector3(0, head.transform.eulerAngles.y, 0);

        // Rotate the input direction by the horizontal head rotation
        direction = Quaternion.Euler(headRotation) * direction;

        // Apply speed and move
        Vector3 movement = direction * speed;
        characterController.Move(movement * Time.deltaTime);
    }

    //Move the player down in the y-axis based on gravity to avoid floating
    private void ApplyGravity()
    {
        Vector3 gravity = new Vector3(0, Physics.gravity.y * gravityMultiplier, 0);
        gravity.y *= Time.deltaTime;
        characterController.Move(gravity * Time.deltaTime);
    }
}
