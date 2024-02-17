using System;
using UnityEngine;

namespace Game
{
    public class AudioSrcManager : MonoBehaviour
    {
        public static AudioSrcManager Instance { get; private set; }
        public AudioSource sfxSource;
        public AudioSource bgmSource;
        public AudioSource speechSource;
        
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != this)
            {
                Destroy(gameObject);
                Instance = this;
            }
            DontDestroyOnLoad(gameObject);
        }

        /// <summary> 播放音效，只播放一次 </summary>
        /// <param name="sfxName"> 音效文件名 </param>
        public void PlaySFX(string sfxName)
        {
            sfxSource.PlayOneShot(Resources.Load<AudioClip>("Audio/SFXs/"+sfxName));
        }
        
        /// <summary> 循环播放背景音乐 </summary>
        /// <param name="bgmName"> </param>
        /// <param name="loop"> </param>
        public void PlayBgm(string bgmName,bool loop=true)
        {
            bgmSource.loop = loop;
            bgmSource.clip = Resources.Load<AudioClip>("Audio/BGM/"+bgmName);
            bgmSource.Play();
        }

        public void StopBgm()
        {
            bgmSource.Stop();
        }

        public void PlaySpeech(string speechName)
        {
            speechSource.Stop();
            speechSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Speech/"+speechName));
        }
        
        public void StopSpeech()
        {
            speechSource.Stop();
        }
        
        private void OnDestroy()
        {
            Destroy(Instance);
        }
    }
}
