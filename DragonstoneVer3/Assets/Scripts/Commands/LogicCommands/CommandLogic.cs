using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CommandLogic : Command
{
    
    
    CommandLogicDelegate executeLogic;

    BuffComponent bc;

    SkillComponent sc;

    

   
    string thisBuff;

    public CommandLogic(CommandLogicDelegate executeLogic)
    {  
        this.executeLogic = executeLogic;        
    }

    public CommandLogic(BuffComponent bc)
    {  
        this.bc = bc;        
    }

    public CommandLogic(SkillComponent sc)
    {  
        this.sc = sc;        
    }

      

    public override void StartCommandExecution()
    {
      
        if(executeLogic != null)
        {
            executeLogic();
            return;
            
        }else if(bc != null)
        {            
            bc.BuffCommandLogic();
            return;
        }else if(sc != null)
        {
            sc.SkillCommandLogic();
            return;
        }
      
        //Command.CommandExecutionComplete();  //Located at end of method

    }

}
