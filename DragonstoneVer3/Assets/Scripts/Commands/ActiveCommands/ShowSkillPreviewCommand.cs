using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShowSkillPreviewCommand : Command
{
    
    SkillHoverPreview skillHoverPreview;

    public ShowSkillPreviewCommand(SkillHoverPreview skillHoverPreview)
    {
        this.skillHoverPreview = skillHoverPreview;
    }

    public override void StartCommandExecution()
    {
        skillHoverPreview.ShowPreview();

        Command.CommandExecutionComplete();

    }

}
