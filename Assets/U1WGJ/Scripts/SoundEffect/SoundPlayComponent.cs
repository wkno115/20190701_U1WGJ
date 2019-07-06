using Pyke;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayComponent : MonoBehaviour
{
    static SoundPlayComponent _soundPlayComponent;
    public static SoundPlayComponent Instance => _soundPlayComponent;

    [SerializeField]
    AudioSourceHandleComponent _cannonFireSe;
    [SerializeField]
    AudioSourceHandleComponent _projectileHitSe;
    [SerializeField]
    AudioSourceHandleComponent _puzzleMoveSe;
    [SerializeField]
    AudioSourceHandleComponent _sphereSe;

    [SerializeField]
    AudioSource _bgmA;
    [SerializeField]
    AudioSource _bgmB;
    [SerializeField]
    AudioSource _bgmC;

    Dictionary<int, AudioSourceHandleComponent> _indexToASHandler = new Dictionary<int, AudioSourceHandleComponent>();

    void Awake()
    {
        foreach (var item in FindObjectsOfType<SoundPlayComponent>())
        {
            if (item != this)
            {
                Destroy(gameObject);
            }
        }
        _soundPlayComponent = this; 
    }

    public void PlayCannonFireSe()
    {
        _cannonFireSe.Play();
    }
    public void PlayProjectileHitSe()
    {
        _projectileHitSe.Play();
    }
    public void PlayPuzzleMoveSe()
    {
        _puzzleMoveSe.Play();
    }
    public void PlaySphereSe()
    {
        _sphereSe.Play();
    }
    public void PlayBgmA()
    {
        _bgmA.Play();
    }
}
