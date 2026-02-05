using System;
using UnityEngine;
using UnityEngine.UI;

public class ReloadBar : MonoBehaviour
{
   [SerializeField] private Image imgBar;
   [SerializeField] private Ability ability;

   private void LateUpdate()
   {
      imgBar.enabled = !(imgBar.fillAmount >= 1);
      imgBar.fillAmount = ability.CooldownPercentage;
   }
}
