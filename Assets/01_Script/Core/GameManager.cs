using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Camera cam;

    public Camera Cam
    {
        get
        {
            if(cam == null)
            {
                cam = Camera.main;
            }
            return cam;
        }
    }
}
