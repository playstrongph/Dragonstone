using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTThreeRootCoroutines: MonoBehaviour
{
    
    CoroutineTree tree = new CoroutineTree();

    void Start()
    {
        tree.AddRoot(SampleCoroutine1());
        tree.AddRoot(SampleCoroutine2());
        tree.AddRoot(SampleCoroutine3());
        tree.Start();
      
    }

    IEnumerator SampleCoroutine1()
    {
        Debug.Log("SampleCoroutine1 Start");

        new WaitForSeconds(2f);

        Debug.Log("SampleCoroutine1 End");

        yield return null;
    }

    IEnumerator SampleCoroutine2()
    {
        Debug.Log("SampleCoroutine2 Start");

        new WaitForSeconds(2f);

        Debug.Log("SampleCoroutine2 End");

        yield return null;
    }

    IEnumerator SampleCoroutine3()
    {
        Debug.Log("SampleCoroutine3 Start");

        new WaitForSeconds(2f);

        Debug.Log("SampleCoroutine3 End");

        yield return null;
    }


}
