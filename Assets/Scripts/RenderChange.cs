using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class RenderChange : MonoBehaviour
{
    public ParticleSystem particle;

    private void Start()
    {
        particle.GetComponent<Renderer>().material.renderQueue = 2000;
    }
}
