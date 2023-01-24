using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ʵ�ǵ�һ�˳ƣ��ļ������ԣ�����������
/// </summary>
public class ThirdPersonCamera : MonoBehaviour
{
    public GameObject thirdPersonCameraObject;
    public Camera thirdPersonCamera;
    public Camera firstPersonCamera;
    public Transform player;

    private int index; // 0 - 1 �л��� 0��������˳Ƶģ�1�����һ�˳Ƶ�

    private float cameraValue; // ������ƶ��ٶ�
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
    /// �� player - fpp camera - canvas - button
    /// </summary>
    public void Switch()
    {
        index++;
        // i = 1 -> ��һ�˳�
        if (index == 1)
        {
            thirdPersonCamera.GetComponent<Camera>().enabled = false;
            firstPersonCamera.GetComponent<Camera>().enabled = true;
            Debug.Log("��һ�˳�");
        }
        // i = 2 -> �����˳�
        else if (index == 2)
        {
            index = 0; 
            thirdPersonCamera.GetComponent<Camera>().enabled = true;
            firstPersonCamera.GetComponent<Camera>().enabled = false;
            Debug.Log("�����˳�");
        }
    }
}
