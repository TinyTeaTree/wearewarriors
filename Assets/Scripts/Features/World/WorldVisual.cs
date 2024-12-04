using System.Collections.Generic;
using System.Collections;
using Core;
using Services;
using UnityEngine;

namespace Game
{
    public class WorldVisual : BaseVisual<World>
    {
        [SerializeField] private Transform _avatarStartSpot;
        [SerializeField] private Transform _cameraStartSpot;
        [SerializeField] private List<Transform> animalStartPoints;

        
        [SerializeField] private BaseSoundDesign ambientSound;
        [SerializeField] private BaseSoundDesign music;
        [SerializeField] private float minMusicInterval;
        [SerializeField] private float maxMusicInterval;
        
        public Transform AvatarStartSpot => _avatarStartSpot;
        public Transform CameraStartSpot => _cameraStartSpot;

        public List<Transform> AnimalStartPoints => animalStartPoints;

        public void StartWorldSounds()
        {
            DJ.Play(ambientSound);
            StartCoroutine(MusicRoutine());
        }

        IEnumerator MusicRoutine()
        {
            while (true)
            {
                DJ.PlayMusic(music);
                var player = DJ.GetPlayer(music);
                var duration = player.Clip.length;
                yield return new WaitForSeconds(duration);

                var interval = Random.Range(minMusicInterval, maxMusicInterval);
                yield return new WaitForSeconds(interval);
            }
        }
    }
}