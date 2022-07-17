using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool open;
    public float OpenRotation, CloseRotation, Speed;
    public bool doorOpen;

    public void doorPlay()
    {
        open = !open;
        doorOpen = true;
    }
    void Update()
    {
        if (open)
        {
            Quaternion doorRotation = Quaternion.Euler(0, OpenRotation, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, doorRotation, Speed * Time.deltaTime);
          
        }
        else
        {
            Quaternion doorRotation2 = Quaternion.Euler(0, CloseRotation, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, doorRotation2, Speed * Time.deltaTime);
           
        }
    }
}
