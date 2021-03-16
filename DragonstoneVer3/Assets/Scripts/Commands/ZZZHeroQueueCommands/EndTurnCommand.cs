using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndTurnCommand : Command
{
    
    float animDelay = 0f;
    GameObject Target;

    public CoroutineTree tree = new CoroutineTree();

    
    public EndTurnCommand(GameObject Target)
    {        
        this.Target = Target;
    }

    public EndTurnCommand(GameObject Target, float animDelay)
    {        
        this.animDelay = animDelay;
        this.Target = Target;
    }

    public override void StartCommandExecution()
    {
        
        //insert anim here, delay for now

        TurnManager.Instance.EndTurn(Target, tree);

        Command.CommandExecutionComplete();  //Relocated to TurnManager.Instance.EndTurn

        


    }


}
