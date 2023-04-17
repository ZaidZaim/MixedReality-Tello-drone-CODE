using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorHandler : MonoBehaviour
{
    public DroneHandler droneHandler; // Reference to the DroneHandler script
    public float positionScale = 0.01f; // To convert the drone's position from cm to Unity units (meters)

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
        SubscribeToDroneHandlerEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromDroneHandlerEvents();
    }

    private void SubscribeToDroneHandlerEvents()
    {
        if (droneHandler != null)
        {
            droneHandler.OnPositionChanged += UpdateAnchorPosition;
        }
    }

    private void UnsubscribeFromDroneHandlerEvents()
    {
        if (droneHandler != null)
        {
            droneHandler.OnPositionChanged -= UpdateAnchorPosition;
        }
    }

    private void UpdateAnchorPosition(float deltaX, float deltaY, float deltaZ)
    {
        // Convert the position to Unity units (meters) and apply the offset
        Vector3 newPosition = new Vector3(deltaX * positionScale, deltaY * positionScale, deltaZ * positionScale);

        // Apply the drone's rotation to the position deltas
        newPosition = droneHandler.transform.rotation * newPosition;

        // Update the 3D anchor's position
        transform.position = initialPosition + newPosition;
    }

}
