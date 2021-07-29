using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public float damage;
    public PlayerController player;
    public RectTransform frameRT;
    public RectTransform fillRT;
    public Image fillImage;


    public void SetFill(float percentage)
    {
        if(player.currentHealth > 20)
        {
            damage = (Mathf.Ceil(percentage / 0.05f) * 0.05f);
            fillImage.DOFillAmount(damage, .7f);
        }
        else
        {
            damage = (Mathf.Ceil(percentage / 0.05f) * 0.05f);
            fillImage.DOFillAmount(damage - 0.05f, .7f);
        }
    }
}
