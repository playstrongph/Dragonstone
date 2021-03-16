using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCardManager : MonoBehaviour
{
    
    public SkillAsset skillAsset;

    [Header("Text Component References")]
    public Text skillName;
    public Text skillCooldown;
    public Text skillDescription;

    [Header("Image References")]
    public Image cardGlow;
    public Image skillGraphic;

    public Image XskillGraphic;


     [Header("Object References")]
     public GameObject skillPreview;
     public GameObject skillPreviewFrame;

     public GameObject target;

    
    
    public void ReadFromAsset()
    {
        skillGraphic.sprite = skillAsset.skillBasicInfo.thumbnail;
        skillCooldown.text = skillAsset.skillBasicInfo.skillCooldown.ToString();
        skillPreview.GetComponent<SkillCardManager>().skillGraphic.sprite = skillAsset.skillBasicInfo.thumbnail;
        skillPreview.GetComponent<SkillCardManager>().skillDescription.text = skillAsset.skillBasicInfo.description;
        skillPreview.GetComponent<SkillCardManager>().skillName.text = skillAsset.skillBasicInfo.skillName;    
        skillPreview.GetComponent<SkillCardManager>().skillCooldown.text = skillAsset.skillBasicInfo.skillCooldown.ToString();

        HidePassiveSkillGlowAndCooldownText();

    }

    void HidePassiveSkillGlowAndCooldownText()
    {
        if(skillAsset.skillBasicInfo.skillType == SkillType.PASSIVE || skillAsset.skillBasicInfo.skillType == SkillType.PASSIVE_ATTACK)
        {
            cardGlow.enabled = false;
            skillCooldown.enabled = false;
            skillPreview.GetComponent<SkillCardManager>().skillCooldown.text = " ";
        }
    }


    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
