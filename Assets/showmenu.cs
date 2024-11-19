using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class showmenu : MonoBehaviour
{
    //private Transform head;
    //public float spawnDistance = 2f;
    public GameObject menu;
    public InputActionProperty showButton;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            menu.SetActive (!menu.activeSelf);
            playerManager.GetInstance().FreezePlayer(!menu.activeSelf);

        }
        //menu.transform.LookAt(new Vector3(head.forward.x, menu.transform.position.y, head.forward.z));
        //menu.transform.forward *= -1;
        //Vector3 lookDirection = new Vector3(head.position.x, menu.transform.position.y, head.position.z);
        //menu.transform.LookAt(lookDirection);

    }
}
