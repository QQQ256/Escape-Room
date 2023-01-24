using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator flower;
    public Animator cabinet;
    // Start is called before the first frame update

    public static AnimationController instance;

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

    void Start()
    {
        //TriggerFlowerAnimation();
        //PlayCabinetAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        //TriggerFlowerAnimation();
    }

    public void PlayFlowerAnimation()
    {
        flower.SetBool("IsOn", true);
    }

    public void PlayCabinetAnimation()
    {
        cabinet.SetBool("IsOn", true);
    }
}
