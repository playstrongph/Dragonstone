using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTHierarchyOfCoroutines: MonoBehaviour
{
    
    CoroutineTree tree = new CoroutineTree();
    //IEnumerator addCoroutine;

    CTOneCoroutine cT1Cor;

    
      

    void Start()
    {
       cT1Cor = GetComponent<CTOneCoroutine>();
       tree.Start();
       tree.AddRoot(RootCoroutine());      

    }

    IEnumerator RootCoroutine()
    {
        Debug.Log("RootCoroutine");            

        tree.AddCurrent(RootCoroutine1(tree));
        tree.AddCurrent(RootCoroutine2(tree));
        tree.AddCurrent(RootCoroutine3(tree));

        tree.CorQ.CoroutineCompleted();              
        yield return null;
    }



    IEnumerator RootCoroutine1(CoroutineTree tree)
    {
        Debug.Log("RootCoroutine1");    
        

        tree.AddCurrent(cT1Cor.SampleCoroutine(tree));

        tree.CorQ.CoroutineCompleted();        
        yield return null;
    }

    IEnumerator RootCoroutine2(CoroutineTree tree)
    {
        Debug.Log("RootCoroutine2");    

        tree.AddCurrent(cT1Cor.SampleCoroutine(tree));
        
        tree.CorQ.CoroutineCompleted();        

        yield return null;
    }

    IEnumerator RootCoroutine3(CoroutineTree tree)
    {
        Debug.Log("RootCoroutine3");  

          tree.AddCurrent(cT1Cor.SampleCoroutine(tree));

          //tree.AddCurrent(cT1Cor.SampleCoroutine());

          //tree.AddCurrent(cT1Cor.CTOneStartCoroutines());
        
          //Command.CommandExecutionComplete();
           tree.CorQ.CoroutineCompleted();        
        

        yield return null;
    }

   

    



    


}
