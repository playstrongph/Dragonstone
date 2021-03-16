using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddBuffHeroQCommand : Command
{
   BuffComponent bc;
   int buffCounter;
   GameObject target;
   
   //Creates the BuffCard on top of the hero

   public AddBuffHeroQCommand(BuffComponent bc, int buffCounter, GameObject target)
   {
       this.bc = bc;
       this.buffCounter = buffCounter;
       this.target = target;
       
   }

    public override void StartCommandExecution()
    {
        GameObject buffCard = GameObject.Instantiate(GlobalSettings.Instance.BuffCardPrefab, target.GetComponent<CardManager>().BuffPanel.transform) as GameObject;
        //Set a reference for the instantiated object to the buffComponent
        bc.buffVisualObject = buffCard;

        buffCard.transform.SetParent(target.GetComponent<CardManager>().BuffPanel.transform);        
        buffCard.GetComponent<BuffCardManager>().BuffText.text = buffCounter.ToString();
        buffCard.GetComponent<BuffCardManager>().BuffImage.sprite = bc.buffAsset.buffBasicInfo.thumbnail;
        buffCard.name = bc.buffAsset.buffBasicInfo.buffName.ToString();

      
        target.GetComponent<HeroQueue>().CommandExecutionComplete();

        //Command.CommandExecutionComplete();
        

    }




}
