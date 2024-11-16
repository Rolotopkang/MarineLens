using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTime : MonoBehaviour
{
    void Start()
    {
        //Start the coroutine we define below named ExampleCoroutine.
        StartCoroutine(WaitCoroutine());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) this.enabled = true;

    }


    IEnumerator WaitCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        this.enabled = false;
     

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2.0f);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        this.enabled = true;
    }
}
