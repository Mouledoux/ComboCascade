using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Energy
{
    [Serializable][CreateAssetMenu(fileName = "NewComboAction", menuName = "ScriptableObjects/ComboActionObject", order = 1)]
    public class EnergyComboAction : ScriptableObject
    {
        [SerializeField]
        protected ActionEnergy.ActionColorsValues[] m_energyComboCode;
        [SerializeField]
        protected ActionEnergy.ActionColorsValues[] m_actionEnergyValue;
        [SerializeField][Space]
        protected float m_stability;


        public ActionEnergy[] GetEnergyComboCode() => ConvertArray(m_energyComboCode);
        public ActionEnergy[] GetActionEnergyValue() => ConvertArray(m_actionEnergyValue);

        public string Value => name;
        public float Stability => m_stability;

        public void CloneEnergyComboValues(EnergyComboAction a_energyCombo)
        {
            m_energyComboCode = a_energyCombo.m_energyComboCode;
            m_actionEnergyValue = a_energyCombo.m_actionEnergyValue;
            name = a_energyCombo.name;
            m_stability = a_energyCombo.m_stability;
        }

        public EnergyComboAction(ActionEnergy.ActionColorsValues[] a_energyComboCode, ActionEnergy.ActionColorsValues[] a_actionEnergyValue, string a_value, float a_stability)
        {
            m_energyComboCode = a_energyComboCode;
            m_actionEnergyValue = a_actionEnergyValue;
            name = a_value;
            m_stability = a_stability;
        }

        private static ActionEnergy[] ConvertArray(ActionEnergy.ActionColorsValues[] m_colorArray)
        {
            ActionEnergy[] arrayClone = new ActionEnergy[m_colorArray.Length];
            for(int i = 0; i < arrayClone.Length; i++)
            {
                arrayClone[i] = m_colorArray[i];
            }
            return arrayClone;
        }

        public sealed class EqualityComparer : IEqualityComparer<ActionEnergy[]>
        {
            public bool Equals(ActionEnergy[] x, ActionEnergy[] y)
            {
                if (x.Length == y.Length)
                {
                    for (int i = 0; i < x.Length; i++)
                    {
                        if (x[i].ColorValue != y[i].ColorValue)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }

            public int GetHashCode(ActionEnergy[] obj)
            {
                unchecked
                {
                    int hash = 17;

                    for (int i = 0; i < obj.Length; i++)
                    {
                        hash = hash * 23 + obj[i].ColorValue.GetHashCode();
                    }

                    return hash;
                }
            }
        }
    }
}
