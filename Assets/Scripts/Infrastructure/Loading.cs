using Infrastructure;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private Image m_loading;

    private static Loading m_instance;

    private void Awake()
    {
        if (m_instance.GetEntityId() == GetEntityId())
        {
            Destroy(gameObject);
            return;
        }

        m_instance = this;
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);

        ServiceLocator.Register(this);
    }
    public void LoadScene(string nameScene)
    {   
        gameObject.SetActive(true);
        StartCoroutine(LoadSceneAsync(nameScene));
    }

    private IEnumerator LoadSceneAsync(string nameScene)
    {
        m_loading.fillAmount = 0;

        float delta = 1 - m_loading.fillAmount;
        const int steps = 10;

        for (var i = 0; i < steps; i++)
        {
            yield return new WaitForSecondsRealtime(0.2f);
            m_loading.fillAmount += (delta / steps);
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(nameScene);
        yield return operation;

        m_loading.fillAmount = 0.5f;
        

        gameObject.SetActive(false);
    }
}
