using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace WHACKGAME
{
    public class MainMenuHandler : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Button m_startGameBTN = null;
        [SerializeField] private Button m_quitGameBTN = null;

        [Header("Game Scene")]
        [SerializeField] private string m_gameSceneName = "";

        //=======================================================

        private void OnEnable()
        {
            this.m_startGameBTN.onClick.AddListener(this.StartGameButtonAction);
            this.m_quitGameBTN.onClick.AddListener(this.QuitGameButtonAction);
        }

        private void OnDisable()
        {
            this.m_startGameBTN.onClick.RemoveListener(this.StartGameButtonAction);
            this.m_quitGameBTN.onClick.RemoveListener(this.QuitGameButtonAction);
        }

        //=======================================================

        private void StartGameButtonAction()
        {
            SceneManager.LoadScene(this.m_gameSceneName);
        }

        private void QuitGameButtonAction()
        {
            Application.Quit();
        }

        //=======================================================
    }
}