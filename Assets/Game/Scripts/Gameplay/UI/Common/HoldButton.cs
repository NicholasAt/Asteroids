using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Game.Scripts.Gameplay.UI.Common
{
    public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsPress { get; private set; }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsPress = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsPress = false;
        }

        private void OnDisable()
        {
            IsPress = false;
        }
    }
}