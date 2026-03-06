using Infrastructure;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuView : MonoBehaviour
    {
        public event Action PlayClicked;
        public event Action ExitClicked;

        [SerializeField] private Loading m_loading;
        [SerializeField] private Button m_playButton;
        [SerializeField] private Button m_exitButton;

        private void OnEnable()
        {
            m_playButton.onClick.AddListener(OnPlayClick);
            m_exitButton.onClick.AddListener(OnExitClick);
        }

        private void OnDisable()
        {
            m_playButton.onClick.RemoveListener(OnPlayClick);
            m_exitButton.onClick.RemoveListener(OnExitClick);
        }

        private void OnPlayClick()
        {
            m_loading.LoadScene(GlobalConstants.Scenes.Game);
            //SceneManager.LoadScene(GlobalConstants.Scenes.Game);
            //PlayClicked?.Invoke();
        }

        private void OnExitClick()
        {
            
            ExitClicked?.Invoke();
        }
    }
}