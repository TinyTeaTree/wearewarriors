using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : MonoBehaviour
{
    public LineRenderer line;
    public ParticleSystem particle;

    private Vector3 target;

    private Coroutine routine;

    private void Start()
    {
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);
    }

    public void Begin()
    {
        routine = StartCoroutine(StreamRoutine());
        StartCoroutine(UpdateParticle());
    }

    public void End()
    {
        StopCoroutine(routine);
        routine = StartCoroutine(EndRoutine());
    }

    private IEnumerator EndRoutine()
    {
        while (!ReachedPosition(0, target))
        {
            AnimateToPosition(0, target);
            AnimateToPosition(1, target);

            yield return null;
        }
        
        Destroy(gameObject);
    }

    private IEnumerator StreamRoutine()
    {
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);
        
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            target = GetTarget();
            MoveToPosition(0, transform.position);
            AnimateToPosition(1, target);
            yield return null;
        }
    }

    private Vector3 GetTarget()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up*10, Vector3.down);
        
        Physics.Raycast(ray, out hit, 200.0f, LayerMask.GetMask("Floor"));

        var end = hit.collider == null ? ray.GetPoint(2f) : hit.point;
        
        return end;
    }

    private void MoveToPosition(int index, Vector3 position)
    {
        line.SetPosition(index, position);
    }

    private void AnimateToPosition(int index, Vector3 pos)
    {
        var current = line.GetPosition(index);
        current = Vector3.MoveTowards(current, pos, Time.deltaTime * 1.75f);
        line.SetPosition(index, current);
    }

    private bool ReachedPosition(int index, Vector3 pos)
    {
        var current = line.GetPosition(index);
        return Vector3.Distance(current, pos) < 0.1f;
    }

    private IEnumerator UpdateParticle()
    {
        while (true)
        {
            particle.transform.position = target;
            if (ReachedPosition(1, target))
            {
                particle.gameObject.SetActive(true);
            }
            else
            {
                particle.gameObject.SetActive(false);

            }
            yield return null;
        }
    }
}
