using System;
using UnityEngine;
using UnityEngine.UI;

public class ReloadBar : MonoBehaviour
{
   [SerializeField] private Image imgBar;
   [SerializeField] private AbilitiesController controller;
   [SerializeField] private int abilityNumber;

   private void UpdateUI()
   {
      imgBar.enabled = !(imgBar.fillAmount >= 1);
      imgBar.fillAmount = controller.abilities[abilityNumber].CooldownPercentage;
   }

   private void LateUpdate()
   {
      UpdateUI();
   }
}
