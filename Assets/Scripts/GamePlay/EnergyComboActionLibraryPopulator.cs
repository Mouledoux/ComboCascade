using Energy;
using Energy.Library;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyComboActionLibraryPopulator : MonoBehaviour
{
    private static EnergyComboActionLibraryPopulator m_instacne;

    private static EnergyComboActionLibraryPopulator Instance
    {
        get
        {
            if(m_instacne == null)
            {
                m_instacne = FindObjectOfType<EnergyComboActionLibraryPopulator>();

                if (m_instacne == null)
                {
                    m_instacne = new GameObject("LibraryPopulator").AddComponent<EnergyComboActionLibraryPopulator>();
                }
            }

            return m_instacne;
        }
    }


    public List<EnergyComboAction> ActionsToAdd = new List<EnergyComboAction>();


    private void Awake()
    {
        if (Instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        foreach(EnergyComboAction eca in ActionsToAdd)
        {
            EnergyComboAction comboAction = ScriptableObject.CreateInstance<EnergyComboAction>();
            comboAction.CloneEnergyComboValues(eca);

            EnergyComboActionLibrary.AddEnergyComboActionToLibrary(comboAction);
        }
    }

}
