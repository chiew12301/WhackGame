using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WHACKGAME
{
    public class InGameUI : MonoBehaviour
    {
        public static InGameUI INSTANCE { get; private set; }

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI m_timerText = null;
        [SerializeField] private TextMeshProUGUI m_scoreText = null;

        //=======================================================

        private void Awake()
        {
            if (INSTANCE != null && INSTANCE != this)
            {
                Destroy(this);
                return;
            }

            INSTANCE = this;
        }

        //=======================================================

        public void UpdateTimerText(float time)
        {
            this.m_timerText.text = time.ToString("0");
        }

        public void UpdateScoreText(int score)
        {
            this.m_scoreText.text = "Score:" + score;
        }

        //=======================================================
    }
}