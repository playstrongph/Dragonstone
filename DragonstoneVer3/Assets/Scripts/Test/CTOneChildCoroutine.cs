using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTOneChildCoroutine: MonoBehaviour
{
    
    CoroutineTree tree = new CoroutineTree();

    // void Start()
    // {
    //     tree.AddRoot(SampleCoroutine1());        
    //     tree.AddRoot(SampleCoroutine2());        
    //     tree.Start();
      
    // }

    public IEnumerator RunCoroutineTree(CoroutineTree tree)
    
    {
        this.tree = tree;
        tree.AddRoot(SampleCoroutine1(tree));        
        tree.AddRoot(SampleCoroutine2(tree));        
        tree.Start();

        yield return null;
    }

    public IEnumerator SampleCoroutine1(CoroutineTree tree)
    {
        Debug.Log("CTOCCor SampleCoroutine1 Start");
       

        tree.AddCurrent(SampleCoroutine3(tree));

        yield return null;
    }

    IEnumerator SampleCoroutine2(CoroutineTree tree)
    {
        Debug.Log("SampleCoroutine2 Start");

        new WaitForSeconds(2f);

        Debug.Log("SampleCoroutine2 End");

        yield return null;
    }

    IEnumerator SampleCoroutine3(CoroutineTree tree)
    {
        Debug.Log("CTOCCor SampleCoroutine3 Start");       

         tree.AddCurrent(SampleCoroutine4(tree));

        yield return null;
    }

     IEnumerator SampleCoroutine4(CoroutineTree tree)
    {
        Debug.Log(" CTOCCor SampleCoroutine4 Start");        

        yield return null;
    }


}
