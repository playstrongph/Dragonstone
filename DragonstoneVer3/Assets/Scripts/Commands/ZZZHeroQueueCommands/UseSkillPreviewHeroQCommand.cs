using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UseSkillPreviewHeroQCommand : Command
{
    SkillComponent skill;
    GameObject useSkillPreview;

    GameObject Target;
    float previewTime = 2f;

    float k = 0.4f;  //percentage time 
        public UseSkillPreviewHeroQCommand(GameObject Target, SkillComponent skill)
    {
        this.skill = skill;
        this.Target = Target;
    }

    public override void StartCommandExecution()
    {
        Sequence s = DOTween.Sequence();

        Sequence s1 = DOTween.Sequence();

        s.AppendCallback(()=>{

           
           useSkillPreview = GameObject.Instantiate(skill.GetComponentInChildren<SkillHoverPreview>().previewGameObject, skill.transform.position, Quaternion.identity) as GameObject;
           
           useSkillPreview.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f );
           
           useSkillPreview.SetActive(true);
           useSkillPreview.transform.DOMove(GlobalSettings.Instance.skillPreviewLocation.transform.position, 1f);
           useSkillPreview.transform.DOScale(useSkillPreview.transform.localScale*30f, 1f);


        });

        s.AppendInterval(previewTime);

        // s.AppendCallback(()=>{ GameObject.Destroy(useSkillPreview);    })
        // .OnComplete(()=> { Command.CommandExecutionComplete(); });

        s.AppendCallback(()=>{ GameObject.Destroy(useSkillPreview);    });       
        

        //2nd sequence for Command Exec Complete

        s1.AppendInterval(k*previewTime);
        s1.AppendCallback(()=>{ Target.GetComponent<HeroQueue>().CommandExecutionComplete(); });

        
        
    }




    

}
