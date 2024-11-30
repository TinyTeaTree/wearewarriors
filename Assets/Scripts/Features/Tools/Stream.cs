using System.Collections;
using UnityEngine;

public class Stream : MonoBehaviour
{
    public LineRenderer line;
    public ParticleSystem ps;

    private Vector3 startPos;

    [SerializeField] private float accel0;
    [SerializeField] private float accel1;
    [SerializeField] private float maxSpeed;
    
    private float speed0;
    private float speed1;

    private float forwardSpeed;

    private Vector3 forward;

    public void Begin(Vector3 forward)
    {
        this.forward = forward;
        forwardSpeed = Random.Range(0.1f, 0.4f);
        ps.gameObject.SetActive(false);
        
        startPos = transform.position;
        MoveToPosition(0, startPos);
        MoveToPosition(1, startPos);

        line.endWidth = line.endWidth * Random.Range(0.3f, 1.1f);
        
        StartCoroutine(StreamRoutine());
    }

    private IEnumerator StreamRoutine()
    {
        var target = GetTarget();
        
        while (!ReachedPosition(1, target))
        {
            if (forwardSpeed > 0f)
            {
                MoveTowards(0, line.GetPosition(0) + forward, forwardSpeed * Time.deltaTime);
                MoveTowards(1, line.GetPosition(1) + forward, forwardSpeed * Time.deltaTime);
                forwardSpeed = Mathf.MoveTowards(forwardSpeed, 0, Time.deltaTime);

                target = GetTarget();
            }

            speed0 += accel0 * Time.deltaTime;
            speed1 += accel1 * Time.deltaTime;

            speed0 = Mathf.Min(speed0, maxSpeed);
            speed1 = Mathf.Min(speed1, maxSpeed);
            
            MoveTowards(0, target, speed0 * Time.deltaTime);
            MoveTowards(1, target, speed1 * Time.deltaTime);
            
            yield return null;
        }

        ps.transform.position = target;
        ps.gameObject.SetActive(true);
        ps.Play();
        
        while (!ReachedPosition(0, target))
        {
            speed0 += accel0 * Time.deltaTime;

            speed0 = Mathf.Min(speed0, maxSpeed);
            
            MoveTowards(0, target, speed0 * Time.deltaTime);
            
            line.endWidth += Time.deltaTime;
            
            yield return null;
        }

        line.enabled = false;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private Vector3 GetTarget()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up*10, Vector3.down);
        
        Physics.Raycast(ray, out hit, 200.0f, LayerMask.GetMask("Floor", "GardenPlot"));

        var end = hit.collider == null ? ray.GetPoint(2f) : hit.point;
        
        return end;
    }

    private void MoveToPosition(int index, Vector3 position)
    {
        line.SetPosition(index, position);
    }

    private void MoveTowards(int index, Vector3 pos, float by)
    {
        var current = line.GetPosition(index);
        current = Vector3.MoveTowards(current, pos, by);
        line.SetPosition(index, current);
    }

    private bool ReachedPosition(int index, Vector3 pos)
    {
        var current = line.GetPosition(index);
        return Vector3.Distance(current, pos) < 0.1f;
    }
}
