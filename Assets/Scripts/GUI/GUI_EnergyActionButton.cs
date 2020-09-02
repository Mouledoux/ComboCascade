using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mouledoux.Components;
using Mouledoux.Mesages;


namespace Energy.UI
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Button))]
    public class GUI_EnergyActionButton : MonoBehaviour
    {
        public ActionEnergy.ActionColorsValues Color;

        private Button button;

        private void OnEnable()
        {
            name = $"{Color} Button";

            button = GetComponent<Button>();
            button.onClick.AddListener(AddEnergy);
            button.onClick.AddListener(PerformCombo);

            button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Color.ToString();
            button.image.color = ActionEnergy.ActionColorLibrary[Color];
        }

        private void AddEnergy()
        {
            Mediator.NotifySubscribers(PredefinedMessages.AddEnergy.ToString(), new object[] { new ActionEnergy(Color) });
            print($"Button: Add {Color}");
        }

        private void PerformCombo()
        {
            Mediator.NotifySubscribers(PredefinedMessages.PerformCombo.ToString());
            print($"Button: Perform Combo");
        }
    }
}
