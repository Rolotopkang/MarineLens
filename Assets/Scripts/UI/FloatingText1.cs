using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText1 : MonoBehaviour
{

    public Transform mainCam;
    Transform unit;

    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {

        unit = transform.parent;

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position);
        transform.position = unit.position + offset;
    }
}
