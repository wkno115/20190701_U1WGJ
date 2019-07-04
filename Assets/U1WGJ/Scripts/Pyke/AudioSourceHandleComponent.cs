using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pyke
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceHandleComponent : MonoBehaviour
    {
        AudioSource _audioSource;

        Queue<AudioSource> _playableAudioSources = new Queue<AudioSource>();

        void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void SetAudioClip(AudioClip audioClip)
        {
            _audioSource.clip = audioClip;
        }
        public void Play()
        {
            StartCoroutine(_play());
        }

        IEnumerator _play()
        {
            var playAudioSource = _playableAudioSources.Count == 0 ? Object.Instantiate(_audioSource) : _playableAudioSources.Dequeue();
            playAudioSource.Play();

            while (playAudioSource.isPlaying)
            {
                yield return null;
            }

            _playableAudioSources.Enqueue(playAudioSource);
        }

        void OnDestroy()
        {
        }
    }
}
