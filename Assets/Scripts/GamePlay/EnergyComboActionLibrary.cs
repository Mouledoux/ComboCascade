using System;
using System.Collections.Generic;

namespace Energy.Library
{
    public static class EnergyComboActionLibrary
    {
        private static Dictionary<ActionEnergy[], EnergyComboAction> ActionLibrary;
      
        public static void AddEnergyComboActionToLibrary(EnergyComboAction a_comboAction)
        {
            if(!ActionLibrary.ContainsKey(a_comboAction.GetEnergyComboCode()))
                ActionLibrary.Add(a_comboAction.GetEnergyComboCode(), a_comboAction);
        }

        public static bool TryGetEnergyComboAction(ActionEnergy[] a_energyCombo, out EnergyComboAction o_comboAction)
        {
            return ActionLibrary.TryGetValue(a_energyCombo, out o_comboAction);
        }

        public static int CheckChainForCombo(List<ActionEnergy> a_energyChain, out EnergyComboAction o_comboAction)
        {
            List<ActionEnergy> chainCache = new List<ActionEnergy>(a_energyChain);

            while (chainCache.Count > 0)
            {
                if (TryGetEnergyComboAction(chainCache.ToArray(), out o_comboAction))
                {
                    return a_energyChain.Count - chainCache.Count;
                }
                else
                {
                    chainCache.RemoveAt(0);
                }
            }

            o_comboAction = null;
            return -1;
        }


        static EnergyComboActionLibrary()
        {
            ActionLibrary =
            new Dictionary<ActionEnergy[], EnergyComboAction>(new EnergyComboAction.EqualityComparer());
        }


        //private const ActionEnergy.ActionColorsValues ActionRed =       ActionEnergy.ActionColorsValues.Red;
        //private const ActionEnergy.ActionColorsValues ActionYellow =     ActionEnergy.ActionColorsValues.Yellow;
        //private const ActionEnergy.ActionColorsValues ActionGreen =     ActionEnergy.ActionColorsValues.Green;
        //private const ActionEnergy.ActionColorsValues ActionBlue =      ActionEnergy.ActionColorsValues.Blue;
        //private const ActionEnergy.ActionColorsValues ActionPurple =    ActionEnergy.ActionColorsValues.Purple;
        //private const ActionEnergy.ActionColorsValues ActionWhite =    ActionEnergy.ActionColorsValues.White;
        //private const ActionEnergy.ActionColorsValues ActionBlack =    ActionEnergy.ActionColorsValues.Black;


        //private static readonly EnergyComboAction BasicPunch = new EnergyComboAction(
        //    new ActionEnergy.ActionColorsValues[] { ActionBlue },
        //        new ActionEnergy.ActionColorsValues[] { ActionBlue },
        //            "Basic Punch", 0.2f);
        
        //private static readonly EnergyComboAction BasicKick = new EnergyComboAction(
        //    new ActionEnergy.ActionColorsValues[] { ActionYellow },
        //        new ActionEnergy.ActionColorsValues[] { ActionYellow },
        //            "Basic Kick", 0.5f);

        //private static readonly EnergyComboAction SpinKick = new EnergyComboAction(
        //    new ActionEnergy.ActionColorsValues[] { ActionYellow, ActionYellow, ActionYellow },
        //       new ActionEnergy.ActionColorsValues[] { ActionRed },
        //            "Spin-Kick", 1.2f);

        //private static readonly EnergyComboAction DragonPunch = new EnergyComboAction(
        //    new ActionEnergy.ActionColorsValues[] { ActionBlue, ActionRed, ActionRed },
        //        new ActionEnergy.ActionColorsValues[] { ActionGreen, ActionPurple },
        //            "Dragon Punch", 0.8f);

        //private static readonly EnergyComboAction TurtleKick = new EnergyComboAction(
        //    new ActionEnergy.ActionColorsValues[] { ActionGreen, ActionGreen, ActionRed },
        //        new ActionEnergy.ActionColorsValues[] { ActionBlue, ActionYellow },
        //            "Trutle Kick", 0.95f);

        //private static readonly EnergyComboAction SpiritThrust = new EnergyComboAction(
        //    new ActionEnergy.ActionColorsValues[] { ActionGreen, ActionPurple, ActionWhite },
        //        new ActionEnergy.ActionColorsValues[] { ActionWhite, ActionYellow },
        //            "Spirit Thrust", 0.15f);

        //private static readonly EnergyComboAction PurpleRain = new EnergyComboAction(
        //    new ActionEnergy.ActionColorsValues[] { ActionPurple, ActionWhite },
        //        new ActionEnergy.ActionColorsValues[] { ActionPurple, ActionPurple, ActionPurple },
        //            "Purple Rain", 2.0f);


    }
}