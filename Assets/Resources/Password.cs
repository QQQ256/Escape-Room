using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class Password : MonoBehaviour
{
    public TextMeshProUGUI uiText = null;
    [SerializeField]
    private string password = "6948";
    private string cur_password = string.Empty;
    private bool isRightPassword = false;

    private void Start()
    {
        // ����ʱ����ͷ��Ӧ���ƶ�
        GameController.instance.DisableCameraMovement();
    }

    #region public method
    public void AddNumber(string number)
    {
        int len = cur_password.Length;
        if (len >= 4) return;

        cur_password += number;
        UpdatePasswordText();
        Debug.Log(cur_password);
    }

    // button event ������룬ÿ��ȥ��һ���ַ�
    public void Clear()
    {
        int len = cur_password.Length;
        if (len <= 0) return;

        cur_password = cur_password.Substring(0, len - 1);
        UpdatePasswordText();
    }

    // button event �ύ����
    public void Confirm()
    {
        int len = cur_password.Length;
        if (len == 4)
        {
            isRightPassword = CheckPassword();
            if (isRightPassword)
            {
                GameController.instance.EnableCameraMovement();
                AnimationController.instance.PlayCabinetAnimation();
                GameController.instance.EnableSwitchButton();
                Return();
            }
            else
            {
                // ����ԷŸ�����������Ч
                cur_password = "";
                UpdatePasswordText();
            }
        }
    }

    /// <summary>
    /// button event
    /// �ر��������
    /// </summary>
    public void Return()
    {
        this.gameObject.SetActive(false);
        GameController.instance.EnableCameraMovement();
        GameController.instance.EnableSwitchButton();
    }

    #endregion

    #region private method
    private bool CheckPassword()
    {
        return cur_password.Equals(password);
    }


    /// <summary>
    /// ��������UI
    /// </summary>
    private void UpdatePasswordText()
    {
        uiText.GetComponent<TMP_Text>().text = cur_password;
    }
    #endregion
}
