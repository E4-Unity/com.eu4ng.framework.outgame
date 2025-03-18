using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Eu4ng.Framework.OutGame.Sample
{
    public class LoadingWidgetBase : UserWidget, ILoadingWidget
    {
        [Header("References")]
        [SerializeField] Image m_FadingImage;
        [SerializeField] RectTransform m_LoadingScreen;
        [SerializeField] TextMeshProUGUI m_LoadingStateText;
        [SerializeField] TextMeshProUGUI m_LoadingPercentageText;
        [SerializeField] Slider m_LoadingPercentageSlider;

        /* ILoadingWidget */
        public virtual void FadeOut(float duration)
        {
            var originalColor = m_FadingImage.color;
            m_FadingImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
            m_FadingImage.CrossFadeAlpha(1, duration, true);
        }

        public virtual void FadeIn(float duration)
        {
            var originalColor = m_FadingImage.color;
            m_FadingImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
            m_FadingImage.CrossFadeAlpha(0, duration, true);
        }

        public virtual void ShowLoadingScreen() => m_LoadingScreen.gameObject.SetActive(true);

        public virtual void HideLoadingScreen() => m_LoadingScreen.gameObject.SetActive(false);

        public virtual void UpdateLoadingProgress(float progress)
        {
            if (m_LoadingPercentageSlider != null) m_LoadingPercentageSlider.value = progress;
            if (m_LoadingPercentageText != null)
            {
                double percentage = Math.Round(progress * 100f, 1);
                m_LoadingPercentageText.text = percentage + "%";
            }
        }

        public virtual void UpdateLoadingState(string state) => m_LoadingStateText?.SetText(state);
    }
}
