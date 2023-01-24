using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 要交互的物品很多，每个物品对应一个图片的显示
/// 这个类将物品对应的图片绑定在一起
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
