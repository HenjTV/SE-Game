
using SkyForge.MVVM;
using UnityEngine;

namespace SEGame
{
    public class UILogView : View
    {
        [SerializeField] private Transform m_messageContainer;
        [SerializeField] private MessageResizer m_messageViewPrefab;

        public void LogMessageInView(string message)
        {
            CreateMessageViewInContainer(message);
        }
        
        private void CreateMessageViewInContainer(string message)
        {
            var messageView = Instantiate(m_messageViewPrefab, m_messageContainer);
            messageView.SetMessage(message);
            
            if (m_messageContainer.childCount > 0)
            {
                var lastChild = m_messageContainer.GetChild(m_messageContainer.childCount - 1).GetComponent<RectTransform>();
                
                var messageRect = messageView.GetComponent<RectTransform>();
                messageRect.anchoredPosition = new Vector2(0, lastChild.anchoredPosition.y - lastChild.rect.height);
                
                UpAllMessageViewInContainer();
            }
        }

        private void UpAllMessageViewInContainer()
        {
            var lastChild = m_messageContainer.GetChild(m_messageContainer.childCount - 1).GetComponent<RectTransform>();

            for (var i = 0; i < m_messageContainer.childCount; i++)
            {
                var child = m_messageContainer.GetChild(i).GetComponent<RectTransform>();
                child.anchoredPosition = new Vector2(0, child.anchoredPosition.y + lastChild.rect.height);
            }
            
            var containerRect = m_messageContainer.GetComponent<RectTransform>();
            var firstChild = m_messageContainer.GetChild(0).GetComponent<RectTransform>();
            
            if (firstChild.anchoredPosition.y + firstChild.rect.height > containerRect.rect.height + 10)
            {
                firstChild.gameObject.SetActive(false);
                Destroy(m_messageContainer.GetChild(0).gameObject);
            }
        }

    }
}


