using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 这其实是第一人称，文件名不对，来不及改了
/// </summary>
public class ThirdPersonCamera : MonoBehaviour
{
    public GameObject thirdPersonCameraObject;
    public Camera thirdPersonCamera;
    public Camera firstPersonCamera;
    public Transform player;

    private int index; // 0 - 1 切换， 0代表第三人称的，1代表第一人称的

    private float cameraValue; // 摄像机移动速度
    private float inputX;
    private float inputY;
    private float cameraVerticalRotation;
    // Start is called before the first frame update
    void Start()
    {
        cameraValue = 0f;
        thirdPersonCamera = GetComponent<Camera>();
        firstPersonCamera = GetComponent<Camera>();
    }

    void Update()
    {
        cameraValue = thirdPersonCameraObject.GetComponent<FirstPersonCamera>().mouseSensitivity;

        inputX = Input.GetAxis("Mouse X") * cameraValue;
        inputY = Input.GetAxis("Mouse Y") * cameraValue;



        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        player.Rotate(Vector3.up * inputX);

    }

    /// <summary>
    /// button event
    /// 绑定 player - fpp camera - canvas - button
    /// </summary>
    public void Switch()
    {
        index++;
        // i = 1 -> 第一人称
        if (index == 1)
        {
            thirdPersonCamera.GetComponent<Camera>().enabled = false;
            firstPersonCamera.GetComponent<Camera>().enabled = true;
            Debug.Log("第一人称");
        }
        // i = 2 -> 第三人称
        else if (index == 2)
        {
            index = 0; 
            thirdPersonCamera.GetComponent<Camera>().enabled = true;
            firstPersonCamera.GetComponent<Camera>().enabled = false;
            Debug.Log("第三人称");
        }
    }
}
