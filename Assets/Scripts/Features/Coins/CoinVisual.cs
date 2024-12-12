using System;
using System.Collections;
using System.Threading.Tasks;
using Core;
using Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class CoinVisual : BaseVisual<Coins>
    {
        [SerializeField, Range(1, 5)] private int volume;
        [SerializeField] private Rigidbody body;
        [SerializeField] private BaseSoundDesign dropSound;
        [SerializeField] private BaseSoundDesign pickSound;

        public int Volume => volume;
        
        public bool IsPickable { get; private set; }

        public void Fly()
        {
            IsPickable = false;
            
            body.isKinematic = false;
            var force = Vector3.up * 4f + (Vector3.right * Random.value) + (Vector3.left * Random.value) + (Vector3.forward * Random.value) + (Vector3.back * Random.value);
            force = force.normalized * Random.Range(0.5f, 1.2f);

            body.AddForce(force, ForceMode.Impulse);
            
            body.AddTorque(
                (Random.value - 0.5f) * 20f, 
                (Random.value - 0.5f) * 20f, 
                (Random.value - 0.5f) * 20f);

            Task.Delay(TimeSpan.FromSeconds(2f)).ContinueWith(_ =>
            {
                IsPickable = true;
            });

            StartCoroutine(CheckDropRoutine());
        }

        public void Pick()
        {
            DJ.Play(pickSound);
        }

        IEnumerator CheckDropRoutine()
        {
            yield return null; //Have Body chance to accumulate speed
            while (body.velocity.magnitude > 0.1f || body.GetAccumulatedForce().magnitude > 0.01f) //This can be improved
            {
                yield return null;
            }

            DJ.Play(dropSound);
        }
    }
}