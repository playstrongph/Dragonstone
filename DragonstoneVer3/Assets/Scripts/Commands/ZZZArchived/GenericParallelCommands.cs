using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GenerciParallelCommands : Command
{
    
    GameObject Attacker;
    GameObject Target;

    

    // public GenerciParallelCommands(GameObject Attacker, GameObject Target, ParallelCommandsDelegate m_ParallelCommands)
    // {
    //     this.Attacker = Attacker;
    //     this.Target = Target;
    // }

    public override void StartCommandExecution()
    {
       
        // m_ParallelCommands(Attacker, Target);
          

        // Sequence s = DOTween.Sequence();

        // //animation Delay    
        // s.AppendInterval(1f).OnComplete(()=>{
        //     Command.CommandExecutionComplete();
        // });

        
    }


}
