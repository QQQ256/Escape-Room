using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2_Puzzle3 : MonoBehaviour
{
    public GameObject puzzle3Object;
    public Light light;
    private void OnEnable()
    {
        puzzle3Object.layer = LayerMask.NameToLayer("Interactable");
        light.GetComponent<Light>().enabled = true;
    }

    private void OnDisable()
    {
        puzzle3Object.layer = LayerMask.NameToLayer("Default");
    }

    public void EnableRoom2Puzzle3Game()
    {
        puzzle3Object.GetComponent<Password>().enabled = true;
    }

    public void DisableRoom2Puzzle3Game()
    {
        puzzle3Object.GetComponent<Password>().enabled = false;
    }
}
