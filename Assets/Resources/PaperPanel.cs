using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ܶ๦����Ҫ������һ��ֽ��������ű�������Ҫ������һ��ֽд��һ��
/// ��һ��panel�е�img��չʾ���滻
/// </summary>
public class PaperPanel : MonoBehaviour
{
    public Image paperImg; 
    // Start is called before the first frame update
    void Start()
    {
        GameController.instance.DisableCameraMovement();
    }

    public void ClosePaperPanel()
    {
        GameController.instance.EnableCameraMovement();
        this.gameObject.SetActive(false);
    }
}
