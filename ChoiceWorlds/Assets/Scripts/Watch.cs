using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watch : MonoBehaviour
{
    private GameObject target;



    private void Start()
    {
        target = GameObject.FindWithTag("MainCamera");
    }
}
