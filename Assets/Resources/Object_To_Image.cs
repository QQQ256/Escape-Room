using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ҫ��������Ʒ�ܶ࣬ÿ����Ʒ��Ӧһ��ͼƬ����ʾ
/// ����ཫ��Ʒ��Ӧ��ͼƬ����һ��
/// </summary>
public class Object_To_Image : MonoBehaviour
{
    public static Object_To_Image instance { get; private set; }

    private Dictionary<GameObject, Sprite> obj_img_dict = new Dictionary<GameObject, Sprite>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    //private GameController gameController;
    //private UIController uiController;
    // Start is called before the first frame update
    void Start()
    {
        Sprite[] onePaperImage = UIController.instance.onePaperImages;
        GameObject[] interactableObj = GameController.instance.interactableObjs;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
