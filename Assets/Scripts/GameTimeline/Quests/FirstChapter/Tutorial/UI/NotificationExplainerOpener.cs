using System;
using GameTimeline.Quests.Presenters;
using UHFPS.Runtime;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace GameTimeline.Quests.FirstChapter.Tutorial.UI
{
    public class NotificationExplainerOpener : IDisposable
    {
        private GameObject _uiObject;
        private GameObject _newNotificationPopup;

        public NotificationExplainerOpener(
            [Inject(Id = "_notificationsUi")] 
            GameObject uiOpener
        )
        {
            _uiObject = uiOpener;
        }

        public void DrawUi(string text)
        {
            // Создаём копию оригинального UI-объекта
            _newNotificationPopup = Object.Instantiate(_uiObject, _uiObject.transform.parent);

            // Получаем скрипт с уведомлением
            var notification = _newNotificationPopup.GetComponent<ObjectiveNotification>();
            if (notification != null)
            {
                // Показываем уведомление на 5 секунд
                notification.ShowNotification(
                    text, 
                    5f
                );
            }
        }

        public void Dispose()
        {
            if (_newNotificationPopup != null)
            {
                Object.Destroy(_newNotificationPopup);
                _newNotificationPopup = null;
            }

            _uiObject = null;
        }
    }
}