using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealEffectHeroQCommand : Command
{
    
    int amount;
   GameObject target;

    //float k = 0.5f;  //Percent of complete animation time

    public HealEffectHeroQCommand(int amount, GameObject target)
    {
        this.amount = amount;
        this.target = target;
    }

    public override void StartCommandExecution()
    {
        // if(amount == 0)
        // {
        //     target.GetComponent<HeroQueue>().CommandExecutionComplete();
        //     return;
        // }
      
        //create animation sequence here

        //Instantiate GameObject
        //Set Delay or Oncomplete
        //Instantiate next GO
        //Set Delay
        //CommExecComplete
        

        var healPrefab = SkillSystem.Instance.HealEffectPrefab;        

        //GameObject healAnim = (GameObject)GameObject.Instantiate(healPrefab.GetComponent<HealEffect>().healAnimation, target.transform.position, Quaternion.Euler(-90,0,0));  //Replace rotation with a constant
        //healAnim.transform.SetParent(target.transform);
        //float animDuration = healPrefab.GetComponent<AutoSelfDestruct>().timer;

        VisualSystem.Instance.CreateFloatingText("HEAL", target, Color.yellow);
        VisualSystem.Instance.GenericSkillEffectAnimation(target, GenericSkillName.HEAL);
        
        Sequence s = DOTween.Sequence();        

        //s.AppendInterval(k*animDuration);      //show damage effect at X % of playback animation
        s.AppendCallback(()=>{ 

        GameObject newHealEffect = (GameObject)GameObject.Instantiate(healPrefab, target.transform.position, Quaternion.identity);      
        newHealEffect.transform.SetParent(target.transform);

        HealEffect he = newHealEffect.GetComponent<HealEffect>();        

        he.AmountText.text = "+" + (amount).ToString();
        he.HealImage.color = Color.green;

        he.StartCoroutine(he.ShowHealEffect());  //has auto-destruct

        //Command.CommandExecutionComplete();

        });
        
        target.GetComponent<HeroQueue>().CommandExecutionComplete();  //Complete here means next command doesn't wait for this to finish
        





        
    }

}
