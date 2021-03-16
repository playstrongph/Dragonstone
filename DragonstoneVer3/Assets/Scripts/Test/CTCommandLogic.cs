using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CTCommandLogic : Command
{
    

    IEnumerator coroutine;

   
    public CTCommandLogic(IEnumerator coroutine)
    {  
        this.coroutine = coroutine;
    }

      

    public override void StartCommandExecution()
    {
      
        GlobalSettings.Instance.StartCoroutine(coroutine);
      
        //Command.CommandExecutionComplete();  //Located at end of method

    }

}
