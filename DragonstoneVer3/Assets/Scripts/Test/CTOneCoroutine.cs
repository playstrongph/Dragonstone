using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTOneCoroutine : MonoBehaviour
{
    
    CoroutineTree tree1 = new CoroutineTree();

    // public void StartTree()
    // {
    //     tree.Start();
    //     tree.AddRoot(RootCoroutine());
        
    // }

    // public IEnumerator RootCoroutine()
    // {
    //     Debug.Log("CTOne RootCoroutine");        
    //     tree.AddCurrent(SampleCoroutine());

    //     yield return null;
    // }  

    public IEnumerator SampleCoroutine(CoroutineTree tree)
    {
        Debug.Log("CTOne SampleCoroutine");        
        tree.AddCurrent(SampleCoroutine2(tree));

          //Command.CommandExecutionComplete();
           tree.CorQ.CoroutineCompleted();        

        yield return null;
    }  

    IEnumerator SampleCoroutine2(CoroutineTree tree)
    {
        Debug.Log("CTOne SampleCoroutine2");    
        tree.AddCurrent(SampleCoroutine3(tree));    

        //Command.CommandExecutionComplete();
         tree.CorQ.CoroutineCompleted();        

        yield return null;
    }  

    IEnumerator SampleCoroutine3(CoroutineTree tree)
    {
        Debug.Log("CTOne SampleCoroutine3");  

          //Command.CommandExecutionComplete();  
           tree.CorQ.CoroutineCompleted();            

        yield return null;
    }  

    public IEnumerator CTOneStartCoroutines()
    {

        tree1.Start();
        tree1.AddRoot(RootSampleCoroutine());


        yield return null;
    }

    public IEnumerator RootSampleCoroutine()
    {
        Debug.Log("CTOne SampleCoroutine");        
        tree1.AddCurrent(SampleCoroutine());

        yield return null;
    }  

    public IEnumerator SampleCoroutine()
    {
        Debug.Log("CTOne SampleCoroutine");        
        tree1.AddCurrent(SampleCoroutine2());

        yield return null;
    }  

    IEnumerator SampleCoroutine2()
    {
        Debug.Log("CTOne SampleCoroutine2");    
        tree1.AddCurrent(SampleCoroutine3());    

        yield return null;
    }  

    IEnumerator SampleCoroutine3()
    {
        Debug.Log("CTOne SampleCoroutine3");        

        yield return null;
    }  

    




}
