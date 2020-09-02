using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mouledoux.Components;
using Mouledoux.Mesages;
using Energy.Library;

namespace Energy
{
    public class ActionEnergyChain : MonoBehaviour
    {
        // input energy only, no combo converts
        private List<ActionEnergy> m_deltaEnergyChain = new List<ActionEnergy>();
        // processed energy chain
        private List<ActionEnergy> m_comboEnergyChain = new List<ActionEnergy>();
        // chain of defined moves
        private List<EnergyComboAction> m_comboActionChain = new List<EnergyComboAction>();

        public int ComboChainLength => m_comboEnergyChain.Count;
        public int DeltaChainLength => m_deltaEnergyChain.Count;
        public List<ActionEnergy> GetEnergyChain() => new List<ActionEnergy>(m_comboEnergyChain);
        public List<ActionEnergy> GetDeltaEnergyChain() => new List<ActionEnergy>(m_deltaEnergyChain);
        public EnergyComboAction NextComboAction => CheckForCombo();


        private Mediator.Subscription AddEnergySub;
        private Mediator.Subscription RemoveEnergySub;

        private Mediator.Subscription PerformComboSub;


        private void Awake()
        {
            AddEnergySub = new Mediator.Subscription(PredefinedMessages.AddEnergy.ToString(), OnAddEnergy);
            RemoveEnergySub = new Mediator.Subscription(PredefinedMessages.RemoveEnergy.ToString(), OnRemoveEnergy);

            PerformComboSub = new Mediator.Subscription(PredefinedMessages.PerformCombo.ToString(), OnPerformCombo);
        }

        private void OnEnable()
        {
            AddEnergySub.Subscribe();
            RemoveEnergySub.Subscribe();
        }

        private void OnDisable()
        {
            AddEnergySub.Unsubscribe();
            RemoveEnergySub.Unsubscribe();
        }

        private void OnAddEnergy(object[] a_args)
        {
            if (a_args[0] is ActionEnergy energy)
            {
                AddEnergyToChain(ref m_deltaEnergyChain, energy, a_args.Length > 1 && a_args[1] is int index1 ? index1 : m_deltaEnergyChain.Count); 
                AddEnergyToChain(ref m_comboEnergyChain, energy, a_args.Length > 1 && a_args[1] is int index2 ? index2 : m_comboEnergyChain.Count);            
            }
        }

        private void OnRemoveEnergy(object[] a_args)
        {
            if(a_args[0] is int index)
            {
                RemoveEnergyAtIndex(ref m_comboEnergyChain, index);
            }
        }

        private void OnPerformCombo(object[] a_args)
        {
            if(a_args[0] is EnergyComboAction eca)
            {
                AddEnergyToChain(ref m_comboActionChain, eca, m_comboActionChain.Count - 1);
            }
        }

        private void AddEnergyToChain<T>(ref List<T> a_chain, T a_energy, int a_index = -1)
        {
            a_index = a_index == -1 ? a_chain.Count : a_index;
            a_chain.Insert(a_index, a_energy);
        }

        private void RemoveEnergyAtIndex(ref List<ActionEnergy> a_chain, int a_index)
        {
            a_index = Mathf.Clamp(Mathf.Abs(a_index), 0, a_chain.Count - 1);
            a_chain.RemoveAt(a_index);
        }

        private EnergyComboAction CheckForCombo()
        {
            EnergyComboActionLibrary.CheckChainForCombo(m_comboEnergyChain, out EnergyComboAction comboAction);
            return comboAction;
        }
    }
}