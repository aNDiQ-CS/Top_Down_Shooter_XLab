using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuView : MonoBehaviour
    {
        public event Action PlayClicked;
        public event Action ExitClicked;

        [SerializeField] private Button m_playButton;
        [SerializeField] private Button m_exitButton;

        private void OnEnable()
        {
            m_playButton.onClick.AddListener(OnPlayClicked);
            m_exitButton.onClick.AddListener(OnExitClicked);
        }

        private void OnDisable()
        {
            m_playButton.onClick.AddListener(OnPlayClicked);
            m_exitButton.onClick.AddListener(OnExitClicked);
        }

        private void OnPlayClicked()
        {
            PlayClicked?.Invoke();
        }

        private void OnExitClicked()
        {
            ExitClicked?.Invoke();
        }        
       
    }
}
