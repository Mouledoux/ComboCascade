using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Energy
{
    [Serializable]
    public class ActionEnergy : IEquatable<ActionEnergy.ActionColorsValues>
    {
        [SerializeField]
        protected ActionColorsValues m_colorValue;
        public ActionColorsValues ColorValue => m_colorValue;

        public ActionEnergy(ActionColorsValues a_colorValue)
        {
            m_colorValue = a_colorValue;
        }

        public static implicit operator ActionEnergy(ActionColorsValues a_colorValue)
        {
            return new ActionEnergy(a_colorValue);
        }

        public static implicit operator ActionColorsValues(ActionEnergy a_actionEnergy)
        {
            return a_actionEnergy.m_colorValue;
        }


        public enum ActionColorsValues
        {
            Red,
            Orange,
            Yellow,
            Green,
            Blue,
            Purple,
            White,
            Black,

        }

        public static Dictionary<ActionColorsValues, Color> ActionColorLibrary = new Dictionary<ActionColorsValues, Color>()
        {
            {ActionColorsValues.Red,    Color.red       },
            {ActionColorsValues.Orange, new Color(1, 0.5f, 0f)},
            {ActionColorsValues.Yellow, Color.yellow    },
            {ActionColorsValues.Green,  Color.green     },
            {ActionColorsValues.Blue,   Color.blue      },
            {ActionColorsValues.Purple, Color.magenta   },
            {ActionColorsValues.White,  Color.white     },
            {ActionColorsValues.Black,  Color.black     },
        };

        public bool Equals(ActionColorsValues other)
        {
            return ColorValue == other;
        }
    }
}