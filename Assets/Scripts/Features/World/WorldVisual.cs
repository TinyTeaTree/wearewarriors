using System.Collections.Generic;
using System.Collections;
using Core;
using Services;
using UnityEngine;

namespace Game
{
    public class WorldVisual : BaseVisual<World>, IWorldBounds
    {
        [SerializeField] private Transform _avatarStartSpot;
        [SerializeField] private Transform _cameraStartSpot;
        [SerializeField] private List<Transform> animalStartPoints;
        
        [SerializeField] private BaseSoundDesign ambientSound;
        [SerializeField] private BaseSoundDesign music;
        [SerializeField] private float minMusicInterval;
        [SerializeField] private float maxMusicInterval;
        
        [SerializeField] private Transform topAnchor;
        [SerializeField] private Transform bottomAnchor;
        [SerializeField] private Transform leftAnchor;
        [SerializeField] private Transform rightAnchor;
        
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

        public Vector3 TopBounds => topAnchor.position;
        public Vector3 RightBounds => rightAnchor.position;
        public Vector3 LeftBounds => leftAnchor.position;
        public Vector3 BottomBounds => bottomAnchor.position;
    }
}