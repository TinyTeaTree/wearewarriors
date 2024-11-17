using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Services;
using UnityEngine;

namespace Game
{
    public class SheepsVisual : BaseVisual<Sheeps>
    {
        [SerializeField] private SheepVisual _sheepPrefab;
        [SerializeField] private Transform _startPos;

        private List<SheepVisual> _sheeps = new();
        
        public Transform AvatarTransform { get; set; }
        
        public void CreateSheep()
        {
            var sheep = Summoner.CreateAsset(_sheepPrefab, transform);
            sheep.SetFeature(Feature);
            sheep.transform.position = _startPos.position;
            sheep.transform.rotation = _startPos.rotation;
            
            _sheeps.Add(sheep);

            StartCoroutine(SheepRoutine(sheep));
        }
        
        private IEnumerator SheepRoutine(SheepVisual sheep)
        {
            while (true)
            {
                var idleDuration = sheep.GetIdleDuration();
                yield return new WaitForSeconds(idleDuration * 0.1f);

                float timePassed = 0;
                while (timePassed <= idleDuration * 0.9f)
                {
                    var checkDistance = Vector3.Distance(AvatarTransform.position, sheep.transform.position);
                    if (checkDistance < sheep.DistanceTolerance)
                    {
                        break;
                    }

                    timePassed += Time.deltaTime;
                    yield return null;
                }
                
                var distance = Vector3.Distance(AvatarTransform.position, sheep.transform.position);

                Vector2 direction;
                float speedFactor = 1f;
                float durationFactor = 1f;

                if (distance > sheep.DistanceMagnet) 
                {
                    //Get Attracted To Player
                    direction = (AvatarTransform.position - sheep.transform.position).XZ() + new Vector2(Random.value - 0.5f, Random.value - 0.5f).normalized;
                    speedFactor = 0.5f;
                    durationFactor = 2f;
                }
                else if (distance < sheep.DistanceTolerance)
                {
                    //Get Away from Player
                    direction = (sheep.transform.position - AvatarTransform.position).XZ() + new Vector2(Random.value - 0.5f, Random.value - 0.5f);
                    speedFactor = 2f;
                    durationFactor = 0.5f;
                }
                else
                {
                    //Go Random Direction
                    direction = new Vector2(Random.value - 0.5f, Random.value - 0.5f).normalized;
                    speedFactor = 1f;
                    durationFactor = 1f;
                }

                sheep.MoveTowardsDirection(direction, speedFactor);
                
                yield return new WaitForSeconds(sheep.GetMoveDuration() * durationFactor);

                sheep.Idle();
            }
        }
    }
}