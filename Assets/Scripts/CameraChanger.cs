using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchThree : MonoBehaviour
{
    public Camera camera1; // Reference to the first camera
    public Camera camera2; // Reference to the second camera
    public Camera camera3; // Reference to the third camera

    private int currentCameraIndex = 0; // Tracks the currently active camera

    void Start()
    {
        // Enable only the first camera and its audio listener
        ActivateCamera(camera1, true);
        ActivateCamera(camera2, false);
        ActivateCamera(camera3, false);
    }

    void Update()
    {
        // Check if the C key is pressed
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCamera();
        }
    }

    private void SwitchCamera()
    {
        // Disable all cameras and their audio listeners
        ActivateCamera(camera1, false);
        ActivateCamera(camera2, false);
        ActivateCamera(camera3, false);

        // Cycle to the next camera
        currentCameraIndex = (currentCameraIndex + 1) % 3;

        // Enable the next camera and its audio listener
        switch (currentCameraIndex)
        {
            case 0:
                ActivateCamera(camera1, true);
                break;
            case 1:
                ActivateCamera(camera2, true);
                break;
            case 2:
                ActivateCamera(camera3, true);
                break;
        }
    }

    private void ActivateCamera(Camera camera, bool isActive)
    {
        if (camera != null)
        {
            camera.enabled = isActive; // Enable/disable the camera
            AudioListener audioListener = camera.GetComponent<AudioListener>();
            if (audioListener != null)
            {
                audioListener.enabled = isActive; // Enable/disable the audio listener
            }
        }
    }
}