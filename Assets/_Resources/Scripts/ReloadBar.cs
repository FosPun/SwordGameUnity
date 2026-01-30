using System;
using UnityEngine;
using UnityEngine.UI;

public class ReloadBar : MonoBehaviour
{
   [SerializeField] private Image imgBar;
   [SerializeField] private Ability ability;

   private void FixedUpdate()
   {
      imgBar.fillAmount = ability.CooldownPercentage;
   }
}
