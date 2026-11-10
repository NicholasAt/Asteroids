using Assets.Game.Scripts.Data.Models.ShipProgress;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Ship
{
    public class ShipView : MonoBehaviour
    {
        [SerializeField] private float _blinkInterval = 0.1f;

        [SerializeField] private GameObject _body;
        [SerializeField] private ParticleSystem _invulnerabilityParticles;

        private ShipProgress _shipProgress;
        private float _blinkElapsedTime;

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
                _blinkElapsedTime += Time.deltaTime;
                if (_blinkElapsedTime >= _blinkInterval)
                {
                    _blinkElapsedTime -= _blinkInterval;
                    _body.SetActive(!_body.activeInHierarchy);
                }
            }
            else
                _body.SetActive(true);

            if (isPlay && !_invulnerabilityParticles.isPlaying)
                _invulnerabilityParticles.Play();
            else if (isPlay == false && _invulnerabilityParticles.isPlaying)
                _invulnerabilityParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}