using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameTimeline.TImeLines
{
    [Serializable]
    public class LightsFixTimelineController
    {
        [SerializeField] private bool _findElectricalPanelTriggered;
        [SerializeField] private bool _electricalPanelFoundTriggered;
        [SerializeField] private bool _fusesTriggeredFound;
        [FormerlySerializedAs("_insertFusesTriggered")] [SerializeField] private bool _fusesInsertedTriggered;

        [NonSerialized] private Action _onFindElectricalPanel;
        [NonSerialized] private Action _onElectricalPanelFound;
        [NonSerialized] private Action _onFusesFound;
        [NonSerialized] private Action _onFusesInserted;

        // === Trigger Methods ===

        public void TriggerFindElectricalPanel()
        {
            if (_findElectricalPanelTriggered) return;

            _findElectricalPanelTriggered = true;
            _onFindElectricalPanel?.Invoke();
            _onFindElectricalPanel = null;
        }
        
        public void TriggerElectricalPanelFound()
        {
            if (_electricalPanelFoundTriggered) return;

            _electricalPanelFoundTriggered = true;
            _onElectricalPanelFound?.Invoke();
            _onElectricalPanelFound = null;
        }

        public void TriggerFusesFound()
        {
            if (_fusesTriggeredFound) return;

            _fusesTriggeredFound = true;
            _onFusesFound?.Invoke();
            _onFusesFound = null;
        }

        public void TriggerFusesInserted()
        {
            Debug.Log("fuses inserted");
            
            if (_fusesInsertedTriggered) return;

            _fusesInsertedTriggered = true;
            _onFusesInserted?.Invoke();
            _onFusesInserted = null;
        }

        // === Subscribe Methods ===

        public void SubscribeOnFindElectricalPanel(Action callback)
        {
            if (_findElectricalPanelTriggered)
            {
                callback?.Invoke();
                return;
            }

            _onFindElectricalPanel += callback;
        }
        
        public void SubscribeOnElectricalPanelFound(Action callback)
        {
            if (_electricalPanelFoundTriggered)
            {
                callback?.Invoke();
                return;
            }

            _onElectricalPanelFound += callback;
        }

        public void SubscribeOnFusesFound(Action callback)
        {
            if (_fusesTriggeredFound)
            {
                callback?.Invoke();
                return;
            }

            _onFusesFound += callback;
        }

        public void SubscribeOnFusesInserted(Action callback)
        {
            if (_fusesInsertedTriggered)
            {
                callback?.Invoke();
                return;
            }

            _onFusesInserted += callback;
        }
    }
}
