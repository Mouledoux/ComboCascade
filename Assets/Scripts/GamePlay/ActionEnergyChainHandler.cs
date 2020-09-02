using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mouledoux.Components;
using Mouledoux.Mesages;

namespace Energy
{
    [RequireComponent(typeof(ActionEnergyChain))]
    public class ActionEnergyChainHandler : MonoBehaviour
    {
        public float decayTime = 1f;
        private float timeSinceLastAdd = 0;

        private int m_lastComboIndex = -1;
        private ActionEnergyChain m_energyComboChain;

        private Mediator.Subscription AddEnergySub;
        private Mediator.Subscription PerformComboSub;

        private SuperiorStateMachine<ChainState> ChainStateMachine =
            new SuperiorStateMachine<ChainState>(ChainState.Init, ChainState.Any);

        public enum ChainState
        {
            Init,
            Any,
            Empty,
            Idle,
            Decaying,
            Cascadeing,
        }

        private void Awake()
        {
            m_energyComboChain = GetComponent<ActionEnergyChain>();

            AddEnergySub = new Mediator.Subscription(PredefinedMessages.AddEnergy.ToString(), OnAddEnergy);
            PerformComboSub = new Mediator.Subscription(PredefinedMessages.PerformCombo.ToString(), OnPerformCombo);

            System.Func<bool> chainIsEmpty = () => { return m_energyComboChain.ComboChainLength == 0; };
            System.Func<bool> chainIsPopulated = () => { return m_energyComboChain.ComboChainLength > 0; };
            System.Func<bool> chainDecayTimeExceeded = () => { return (timeSinceLastAdd += Time.deltaTime) > decayTime; };


            System.Func<bool>[] chainIsEmptyCheck =
            {
                chainIsEmpty,
            };
            System.Func<bool>[] chainIsIdleCheck =
            {
                chainIsPopulated,
            };
            System.Func<bool>[] chainIsDecayingCheck =
            {
                chainIsPopulated,
                chainDecayTimeExceeded,
            };

            System.Action decay = () =>
            {
                timeSinceLastAdd = 0;
                Mediator.NotifySubscribers(PredefinedMessages.RemoveEnergy.ToString(), new object[] { 0 });
            };

            ChainStateMachine.AddTransition(ChainState.Any, ChainState.Empty, chainIsEmptyCheck, null);
            ChainStateMachine.AddTransition(ChainState.Any, ChainState.Idle, chainIsIdleCheck, null);
            ChainStateMachine.AddTransition(ChainState.Any, ChainState.Decaying, chainIsDecayingCheck, decay);
        }

        private void Update()
        {
            ChainStateMachine.Update();
        }

        private void OnEnable()
        {
            AddEnergySub.Subscribe();
            PerformComboSub.Subscribe();
        }

        private void OnDisable()
        {
            AddEnergySub.Unsubscribe();
            PerformComboSub.Unsubscribe();
        }

        private void OnAddEnergy(object[] a_args)
        {
            timeSinceLastAdd = 0;

            if (a_args[0] is EnergyComboAction comboAction)
            {
                decayTime = comboAction.Stability * 1.11f;
            }
        }

        private void OnPerformCombo(object[] a_args)
        {
            EnergyComboAction nextCombo = m_energyComboChain.NextComboAction;
            if (nextCombo == null) return;

            m_lastComboIndex = m_energyComboChain.ComboChainLength - 1;

            for (int i = nextCombo.GetEnergyComboCode().Length; i > 0; i--)
            {
                Mediator.NotifySubscribers(PredefinedMessages.RemoveEnergy.ToString(),
                    new object[] { m_energyComboChain.ComboChainLength - 1 });
            }

            for (int i = 0; i < nextCombo.GetActionEnergyValue().Length; i++)
            {
                ActionEnergy nextEnergy = nextCombo.GetActionEnergyValue()[i];
                 
                Mediator.NotifySubscribers(PredefinedMessages.AddEnergy.ToString(),
                    new object[] { nextEnergy });
            }

            decayTime = 1 + nextCombo.Stability * 1.11f;
            Mediator.NotifySubscribers(nextCombo.name, new object[] { nextCombo });


            if (m_lastComboIndex != m_energyComboChain.ComboChainLength - 1)
            {
                //AddEnergySub.Unsubscribe();
                StartCoroutine(ICascadeCombos(nextCombo.Stability));
            }


            // Debugging messages
            ActionEnergy[] actioinValues = nextCombo.GetActionEnergyValue();
            string actionColorString = ($"{(actioinValues.Length > 0 ? actioinValues[0].ColorValue.ToString() : "gray")}").ToLower();
            print($"Combo: <color={actionColorString}>{nextCombo.Value}</color> performed");
            // Debugging messages
        }

        public IEnumerator ICascadeCombos(float a_duration)
        {
            yield return new WaitForSeconds(a_duration);
            //AddEnergySub.Subscribe();
            Mediator.NotifySubscribers(PredefinedMessages.PerformCombo.ToString());
        }
    }
}
