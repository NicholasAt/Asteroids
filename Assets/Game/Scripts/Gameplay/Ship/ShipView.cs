using Assets.Game.Scripts.Data.Models.ShipProgress;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Ship
{
    public class ShipView : MonoBehaviour
    {
        private float CD = 0.1f;
        [SerializeField] private GameObject _body;
        [SerializeField] private ParticleSystem _ps;

        private ShipProgress _shipProgress;
        private float _currentCD;

        public void Init(ShipProgress shipProgress)
        {
            _shipProgress = shipProgress;
        }
        private void LateUpdate()
        {
            UpdateEffect();
        }

        private void UpdateEffect()
        {
            bool isPlay = _shipProgress.IsInvulnerable;
            if (isPlay)
            {
                _currentCD += Time.deltaTime;
                if (_currentCD >= CD)
                {
                    _currentCD -= CD;
                    _body.SetActive(!_body.activeInHierarchy);
                }
            }
            else
                _body.SetActive(true);

            if (isPlay && !_ps.isPlaying)
                _ps.Play();
            else if (isPlay == false && _ps.isPlaying)
                _ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}