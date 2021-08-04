using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings Instance;

    #region Setting Values
    private static float volume = 100f;
    public static float Volume { get { return volume; } set { volume = value; } }


    private static float cameraZoom;
    public static float CameraZoom { get { return cameraZoom; } set { CameraZoom = value; } }

    #endregion

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }


}
