using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1Raycast : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerCam>().cam;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (Input.GetMouseButtonDown(0))
            {
                // ����������
                if (hitInfo.transform.CompareTag("Puzzle1_1"))
                {
                    GameController.instance.DisableSwitchButton();
                    PuzzleController.instance.StartRoom1Puzzle1();
                    Debug.Log("Clicked Room1Puzzle1");
                }
                // ���ݵ�����
                if (hitInfo.transform.CompareTag("Puzzle1_2"))
                {
                    // ��������2 �ȴ����� ������Ϸ
                    GameController.instance.DisableSwitchButton();
                    PuzzleController.instance.StartGamePuzzle2();
                    GameController.instance.DisableCurrentCamera();
                    Debug.Log("Clicked Room1Puzzle2");
                }
                if (hitInfo.transform.CompareTag("Puzzle1_3"))
                {
                    GameController.instance.DisableSwitchButton();
                    PuzzleController.instance.StartRoom1Puzzle3Game();
                    Debug.Log("Clicked Room1Puzzle2");
                }
            }
        }
    }
}
