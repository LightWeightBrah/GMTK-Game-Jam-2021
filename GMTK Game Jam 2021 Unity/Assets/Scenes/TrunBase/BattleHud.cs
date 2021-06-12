using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleHud : MonoBehaviour
{

    public Slider hpSlider;


    public void HudSettings()
    {
        //hpSlider.maxValue = maxHp;
        //hpSlider.value = currentHp;
    }
    public void SetHp(int hp)
    {
        hpSlider.value = hp;
    }
}
