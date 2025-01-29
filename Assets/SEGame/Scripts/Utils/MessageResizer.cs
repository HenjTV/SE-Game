using TMPro;
using UnityEngine;

namespace SEGame
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class MessageResizer : MonoBehaviour
    {
        [SerializeField] private float m_offsetHeight = 2f;
        [SerializeField] private float m_scaleFontSize = 0.75f;
        
        [SerializeField] private TextMeshProUGUI m_tMPtext;
        [SerializeField] private RectTransform m_selfRectTransform;
        
        public void SetMessage(string message)
        {
            if (message == null)
                message = string.Empty;
            
            var countRow = (int)(message.Length * m_tMPtext.fontSize / 1.5 / m_selfRectTransform.rect.width) + 1;
            
            m_selfRectTransform.sizeDelta = new Vector2(m_selfRectTransform.rect.width, m_tMPtext.fontSize * m_scaleFontSize * countRow + m_offsetHeight * 2);
            m_tMPtext.text = message;
        }
        
    }
}

