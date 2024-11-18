using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class CloseNetPhysic : MonoBehaviour
{
    public void CheckPlaced(bool set)
    {
        if (GetComponent<PlacePoint>().GetPlacedObject().name.Equals("Net"))
        {
            GetComponent<PlacePoint>().GetPlacedObject().GetComponentInChildren<Cloth>().enabled = set;
        }
    }
}
