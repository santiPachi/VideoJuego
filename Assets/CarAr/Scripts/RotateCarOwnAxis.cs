﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCarOwnAxis : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0f,1f,0f);
    }
}
