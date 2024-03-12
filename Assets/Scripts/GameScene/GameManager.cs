using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WHACKGAME
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager INSTANCE { get; private set; }

        [Header("Game Settings")]
        [SerializeField] private float m_gameMaximumTime = 60.0f;
        [SerializeField] private SO_GameContent m_gameContentSO = null;

        private float m_elapsedTime = 0.0f;
        private bool m_isGameStart = false;

        //=======================================================

        private void Awake()
        {
            if(INSTANCE != null && INSTANCE != this)
            {
                Destroy(this);
                return;
            }

            INSTANCE = this;
        }

        private void Update()
        {
            if (!this.m_isGameStart) return;

            this.m_elapsedTime += Time.deltaTime;

            InGameUI.INSTANCE.UpdateTimerText(this.m_elapsedTime);

            if(this.m_elapsedTime >= this.m_gameMaximumTime)
            {
                this.StopGame();
            }
        }

        //=======================================================

        public void StartGame()
        {
            this.m_elapsedTime = 0.0f;
            this.m_isGameStart = true;
        }

        public void StopGame()
        {
            this.m_isGameStart = false;
        }

        //=======================================================
    }
}