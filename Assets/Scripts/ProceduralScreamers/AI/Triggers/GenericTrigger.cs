using System;
using UnityEngine;

namespace ProceduralScreamers.AI.Triggers
{
    public enum TriggerType
    {
        AllType
        
    }
    
    public class GenericTrigger : MonoBehaviour
    {
        [SerializeField] private TriggerType _triggerType;
        [SerializeField] private bool _canBeTriggeredAgain;

        private uint _id;

        private void OnTriggerEnter(Collider other)
        {
            throw new NotImplementedException();
        }
    }
}