using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 cameraInitialPosition;
    private float shakeMegnetude = 0.05f, shakeTime = 0.5f;
    public Camera mainCamera;
    // Start is called before the first frame update
    public void ShakeIt()
    {
        cameraInitialPosition = mainCamera.transform.position;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopShaking", shakeTime);
    }

    public void StartCameraShaking()
    {
        float cameraShakingOffsetX = Random.value * shakeMegnetude * 2 - shakeMegnetude;
        float cameraShakingOffsetY = Random.value * shakeMegnetude * 2 - shakeMegnetude;
        Vector3 cameraIntermediatePosition = mainCamera.transform.position;
        cameraIntermediatePosition.x += cameraShakingOffsetX;
        cameraIntermediatePosition.y += cameraShakingOffsetY;
        mainCamera.transform.position = cameraIntermediatePosition;
    }
    // Update is called once per frame
    void StopShaking()
    {
        CancelInvoke("StartCameraShaking");
        mainCamera.transform.position = cameraInitialPosition;
    }
}
