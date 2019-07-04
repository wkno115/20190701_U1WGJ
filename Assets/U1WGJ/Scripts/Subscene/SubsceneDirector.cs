﻿using Play;
using Result;
using System.Collections;
using System.Collections.Generic;
using Title;
using UnityEngine;

namespace Subscene
{
    public class SubsceneDirector : MonoBehaviour
    {
        [SerializeField]
        TitleSubsceneDirector _titleSubsceneDirector;
        [SerializeField]
        PlaySubsceneDirector _playSubsceneDirector;
        [SerializeField]
        ResultSubsceneDirector _resultSubsceneDirector;


        IEnumerator Start()
        {
            PlayResult? playResult = null;
            while (true)
            {
                foreach (var _ in _titleSubsceneDirector.Run())
                {
                    yield return null;
                }
                foreach (var result in _playSubsceneDirector.Run())
                {
                    playResult = result;
                    yield return null;
                }
                foreach (var _ in _resultSubsceneDirector.Run(playResult))
                {
                    yield return null;
                }
            }
        }
    }
}