using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pyke
{
    public class ParticleSystemHandleComponent : MonoBehaviour
    {
        [SerializeField]
        ParticleSystem _particleSystem;

        Queue<ParticleSystem> _playableInstanceQueue = new Queue<ParticleSystem>();

        void Start()
        {
            _playableInstanceQueue.Enqueue(Instantiate(_particleSystem, _particleSystem.transform.position, _particleSystem.transform.rotation));
        }

        public void Play()
        {
            StartCoroutine(_play(_particleSystem.transform.position));
        }
        public void Play(Vector3 position)
        {
            StartCoroutine(_play(position));
        }

        IEnumerator _play(Vector3 position)
        {
            if (_playableInstanceQueue.Count == 0)
            {
                _playableInstanceQueue.Enqueue(Instantiate(_particleSystem, _particleSystem.transform.position, _particleSystem.transform.rotation));
            }

            var instantiatedParticleSystem = _playableInstanceQueue.Dequeue();
            instantiatedParticleSystem.transform.position = position;
            instantiatedParticleSystem.Play();
            while (instantiatedParticleSystem.isPlaying)
            {
                yield return null;
            }
            _playableInstanceQueue.Enqueue(instantiatedParticleSystem);
        }
    }
}
