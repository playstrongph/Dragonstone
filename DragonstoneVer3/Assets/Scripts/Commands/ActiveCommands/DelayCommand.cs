using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DelayCommand : Command
{
    float value = 0.5f;
    
    public DelayCommand(float value)
    {
        this.value = value;
        
    }

    public override void StartCommandExecution()
    {
    
        Sequence s = DOTween.Sequence();
        s.AppendInterval(value)        
        
        .OnComplete(()=>{ Command.CommandExecutionComplete();});


    }


}
