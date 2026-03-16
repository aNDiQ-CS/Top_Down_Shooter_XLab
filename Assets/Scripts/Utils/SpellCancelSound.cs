using Magic.Systems;
using System;
using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(AudioSource))]
    public class SpellCancelSound : MonoBehaviour
    {
        [SerializeField] private MagicSystem m_spellSystem;
        [SerializeField] private AudioSource m_soundSource;

        private void OnEnable()
        {
            m_spellSystem.SpellCancelled += PlaySound;
        }

        private void OnDisable()
        {
            m_spellSystem.SpellCancelled -= PlaySound;
        }

        private void PlaySound()
        {
            m_soundSource.Play();
        }
    }
}

