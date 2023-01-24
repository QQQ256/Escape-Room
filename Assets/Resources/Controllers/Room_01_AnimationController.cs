using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_01_AnimationController : MonoBehaviour
{
    public Animator hypnotizer_Door;
    public Animator hypnotizer_Drawer;
    public Animator hypnotizer_RoundScreen;

    public static Room_01_AnimationController instance;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play_Hypnotizer_Door()
    {
        hypnotizer_Door.SetBool("IsOn", true);
    }

    public void Play_Hypnotizer_Drawer()
    {
        hypnotizer_Drawer.SetBool("IsOn", true);
    }

    public void Play_Hypnotizer_RoundScreen()
    {
        hypnotizer_RoundScreen.SetBool("IsOn", true);
    }
}
