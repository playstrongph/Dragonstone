using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public HeroAsset heroAsset;

    [Header("Text Component References")]
    public Text heroName;
    public Text description;    
    public Text attackText;
    public Text healthText;
    public Text armorText;
    public Text ATBValue;
    public Text AttackText;
    public Text EnergyText;
    public Text NameText;

    [Header("Image References")]

    public Image heroImage;
    public Image heroPanelImage;
    public Image ActiveGlowImage;
    public Image TargetGlowImage;

    public Image TauntActiveGlowImage;
    public Image TauntTargetGlowImage;    

    public Image ATBfill;

    [Header("GameObject References")]
    public GameObject TauntFrame;
    public GameObject SkillPanel;    
    public GameObject HeroPanel;
    public GameObject BuffPanel;
    public GameObject HealthObject;
    public GameObject AttackObject;
    public GameObject ArmorObject;
     public GameObject EnergyObject;

    public GameObject HeroCanvas;
    public GameObject BuffCanvas;
    public GameObject Target;


    [Header("Enum References")]
    public CreatureType creatureType;
    public Rarity rarity;
    public Faction faction;


    public void ReadCreatureFromAsset()
    {
        HeroLogic hl = GetComponent<HeroLogic>();
        Color defaultColor = Color.white;

        heroName.text = heroAsset.hero.name;
        description.text = heroAsset.hero.description;

        attackText.text = heroAsset.hero.attack.ToString();
        attackText.color = defaultColor;       
        
        healthText.text = heroAsset.hero.health.ToString();   
        healthText.color = defaultColor;
        
        armorText.text = heroAsset.hero.armor.ToString(); 
        armorText.color = defaultColor;

        heroImage.sprite = heroAsset.hero.heroImage;            
        

        creatureType = heroAsset.hero.creatureType;   
        rarity = heroAsset.hero.rarity;
        faction = heroAsset.hero.faction;  
    }

    public void Start()
    {


    }

    public void UpdateCardATB(float timerValue)
    {
        ATBValue.text = timerValue.ToString();
        //Debug.Log ("Timer value: " + timerValue);
        ATBfill.fillAmount = (float)timerValue*10/(float)GlobalSettings.Instance.timerFull;           

    }

    public void ShowTauntFrame()
    {
        TauntFrame.SetActive(true);
    }

    public void HideTauntFrame()
    {
        TauntFrame.SetActive(false);

        TauntActiveGlowImage.enabled = false;
         TauntTargetGlowImage.enabled = false;
    }

    public void ShowActiveGlow()
    {
        if (GetComponent<Taunt>() != null)
            TauntActiveGlowImage.enabled = true;
        else ActiveGlowImage.enabled = true;
    }

    public void HideActiveGlow()
    {
        if (GetComponent<Taunt>() != null)
            TauntActiveGlowImage.enabled = false;
        else ActiveGlowImage.enabled = false;
    }


    public void ShowTargetGlow()
    {
        if (GetComponent<Taunt>() != null)
            TauntTargetGlowImage.enabled = true;
        else TargetGlowImage.enabled = true;
    }

    public void HideTargetGlow()
    {
        if (GetComponent<Taunt>() != null)
            TauntTargetGlowImage.enabled = false;
        else TargetGlowImage.enabled = false;
    }

    public void ClearAllGlows()
    {
        HideActiveGlow();
        HideTargetGlow();
    }

    public void HideHero()
    {
        HeroCanvas.SetActive(false);
        BuffCanvas.SetActive(false);
        SkillPanel.SetActive(false);
        Target.SetActive(false);
    }

    //TEMP for RESSURECT
    public void ShowHero()
    {
        HeroCanvas.SetActive(true);
        BuffCanvas.SetActive(true);
        SkillPanel.SetActive(true);
        Target.SetActive(true);
    }

}
