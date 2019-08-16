using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateControl : MonoBehaviour
{
    private Gyroscope gyro;
    private bool gyroEnabled;

    private Quaternion rot;
    private const float rot_coef = 30f;

    // Start is called before the first frame update
    void Start()
    {
        gyroEnabled = EnableGyro();

        rot = new Quaternion(1f, 0f, 0f, 0f);
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            return true;
        }

        return false;
    }

    void Update()
    {
        if (!gyroEnabled)
            return;

        transform.Rotate(new Vector3(0f, 0f, -Input.acceleration.x));
        MyDebug.Log(Input.acceleration.x);
    }
}
