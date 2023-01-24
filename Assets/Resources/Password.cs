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
        // 解密时摄像头不应该移动
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

    // button event 清空输入，每次去掉一个字符
    public void Clear()
    {
        int len = cur_password.Length;
        if (len <= 0) return;

        cur_password = cur_password.Substring(0, len - 1);
        UpdatePasswordText();
    }

    // button event 提交密码
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
                // 这可以放个密码错误的音效
                cur_password = "";
                UpdatePasswordText();
            }
        }
    }

    /// <summary>
    /// button event
    /// 关闭密码界面
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
    /// 更新密码UI
    /// </summary>
    private void UpdatePasswordText()
    {
        uiText.GetComponent<TMP_Text>().text = cur_password;
    }
    #endregion
}
