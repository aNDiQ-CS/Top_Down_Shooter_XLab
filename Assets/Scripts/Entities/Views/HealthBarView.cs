using System;
using UnityEngine;
using UnityEngine.UI;

namespace Entities.Views
{
    internal class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Image m_bar;
        [SerializeField] private HealthComponent m_healthComponent;

        private void OnEnable()
        {
            m_healthComponent.ValueChanged += SetValue;
        }

        private void OnDisable()
        {
            m_healthComponent.ValueChanged -= SetValue;
        }      

        private void SetValue()
        {
            
        }
    }
}
