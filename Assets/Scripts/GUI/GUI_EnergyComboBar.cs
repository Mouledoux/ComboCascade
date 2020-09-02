using Mouledoux.Mesages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Energy.UI
{
    public class GUI_EnergyComboBar : MonoBehaviour
    {
        public Sprite EnergySprite;
        public ActionEnergyChain Chain;

        Mouledoux.Components.Mediator.Subscription AddEnergySub;
        Mouledoux.Components.Mediator.Subscription RemoveEnergySub;
        Mouledoux.Components.Mediator.Subscription PerfomComboSub;

        private void Awake()
        {
            AddEnergySub = new Mouledoux.Components.Mediator.Subscription(PredefinedMessages.AddEnergy.ToString(), UpdateGUIBar, -1);
            RemoveEnergySub = new Mouledoux.Components.Mediator.Subscription(PredefinedMessages.RemoveEnergy.ToString(), UpdateGUIBar, -1);
            PerfomComboSub = new Mouledoux.Components.Mediator.Subscription(PredefinedMessages.PerformCombo.ToString(), UpdateGUIBar, -1);
        }

        private void OnEnable()
        {
            AddEnergySub.Subscribe();
            RemoveEnergySub.Subscribe();
            PerfomComboSub.Subscribe();
        }

        private void OnDisable()
        {
            AddEnergySub.Unsubscribe();
            RemoveEnergySub.Unsubscribe();
            PerfomComboSub.Unsubscribe();
        }

        void UpdateGUIBar(object[] a_args)
        {
            ClearBar();

            foreach (ActionEnergy energy in Chain.GetEnergyChain())
            {
                Color energyColor = ActionEnergy.ActionColorLibrary[energy.ColorValue];

                Image newEnergyImage = new GameObject($"{energyColor} Energy").AddComponent<Image>();
                newEnergyImage.sprite = EnergySprite;
                newEnergyImage.color = energyColor;

                newEnergyImage.transform.SetParent(transform);
            }

        }

        private void ClearBar()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}
