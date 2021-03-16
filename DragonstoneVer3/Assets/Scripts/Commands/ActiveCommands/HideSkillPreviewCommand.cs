using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NO Delay required

public class HideSkillPreviewCommand : Command  //UI Command
{
    
    SkillHoverPreview skillHoverPreview;

    public HideSkillPreviewCommand(SkillHoverPreview skillHoverPreview)
    {
        this.skillHoverPreview = skillHoverPreview;
    }

    public override void StartCommandExecution()
    {
        skillHoverPreview.HidePreview();
        Command.CommandExecutionComplete();
    }


}
