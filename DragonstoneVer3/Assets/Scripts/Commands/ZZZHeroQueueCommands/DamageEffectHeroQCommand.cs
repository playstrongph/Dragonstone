using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DamageEffectHeroQCommand : Command
{
    
    int amount;
    GameObject target;

    
    public DamageEffectHeroQCommand(int amount, GameObject target)
    {
        this.amount = amount;       
        this.target = target;
    }

    public override void StartCommandExecution()
    {
        // if(amount == 0)
        // {
        //     Command.CommandExecutionComplete();
        //     return;
        // }
      
        
        GameObject newDamageEffect = GameObject.Instantiate(GlobalSettings.Instance.DamageEffectPrefab, target.transform.position, Quaternion.identity) as GameObject;        
        
        newDamageEffect.transform.SetParent(target.transform);


        DamageEffect de = newDamageEffect.GetComponent<DamageEffect>();

        if (amount < 0)
        {
            // NEGATIVE DAMAGE = HEALING
            de.AmountText.text = "+" + (-amount).ToString();
            de.DamageImage.color = Color.green;
        }        
        else
        {
            de.AmountText.text = "-"+amount.ToString();

            

            //GameObject newSFXDamageEffect = GameObject.Instantiate(GlobalSettings.Instance.SFX_TakeDamage_Prefab[Random.Range(0,GlobalSettings.Instance.SFX_TakeDamage_Prefab.Count-1)], position, Quaternion.identity) as GameObject;
            Sequence s = DOTween.Sequence();
            s.AppendInterval(0.2f);
            
            //s.OnComplete(() => Destroy(newSFXDamageEffect));


        }
        // start a coroutine to fade away and delete this effect after a certain time
        de.StartCoroutine(de.ShowDamageEffect());

        target.GetComponent<HeroQueue>().CommandExecutionComplete();





        
    }

}
