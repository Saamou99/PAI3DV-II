using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGDisabler : MonoBehaviour
{
    public float delay = 2.3f; // Time in seconds before disabling the GameObject

    void Start()
    {
        // Schedule the Disable function to be called after the delay
        Invoke("DisableGameObject", delay);
    }

    void DisableGameObject()
    {
        // Disable the GameObject
        gameObject.SetActive(false);
    }
}