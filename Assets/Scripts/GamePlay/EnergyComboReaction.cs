using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mouledoux.Components;
using Mouledoux.Mesages;
using Energy.Library;

namespace Energy
{
    public class EnergyComboReaction : MonoBehaviour
    {
        [SerializeField]
        private EnergyComboAction m_comboActionDefinition = null;

        [SerializeField]
        private LayerMask m_affectedLayers;

        [Range(0f, 1f)]
        public float progress;

        public UnityEvent OnPreformComboAction;


        private Mediator.Subscription PerformComboActionSub;

        private void Awake()
        {
            PerformComboActionSub = new Mediator.Subscription(m_comboActionDefinition.name, PerformComboActionEvent);
        }

        private void Start()
        {
            EnergyComboActionLibrary.AddEnergyComboActionToLibrary(m_comboActionDefinition);
        }

        private void OnEnable()
        {
            PerformComboActionSub.Subscribe();
        }

        private void OnDisable()
        {
            PerformComboActionSub.Unsubscribe();
        }

        private void PerformComboActionEvent(object[] a_args)
        {
            OnPreformComboAction.Invoke();
        }

        private void OnDrawGizmos()
        {

        }

        //[System.Serializable]
        //public class ActionEventAOE
        //{
        //    [HideInInspector]
        //    public Vector3 startPosition = Vector3.zero;
        //    public float startRadius;

        //    public AnimationCurve deltaPositionX;
        //    public AnimationCurve deltaPositionY;
        //    public AnimationCurve deltaPositionZ;
            
        //    public AnimationCurve deltaRadius;

        //    public Vector3 GetPositionAtProgress(float a_progress)
        //    {
        //        return startPosition + new Vector3(
        //            deltaPositionX.Evaluate(a_progress),
        //            deltaPositionY.Evaluate(a_progress),
        //            deltaPositionZ.Evaluate(a_progress));
        //    }

        //    public float GetRadiusAtProgress(float a_progress)
        //    {
        //        return startRadius + deltaRadius.Evaluate(a_progress);
        //    }
        //}

    }
}