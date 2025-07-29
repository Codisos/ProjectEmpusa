using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using Support;

public class ViewSensor : Sensor
{
    [Header("SENSOR")]
    [SerializeField] float viewDistance;
    [Range(0, 360)] [SerializeField] float fov;

    [Header("FILTERS")]
    [SerializeField] LayerMask targetedLayers;
    [SerializeField] LayerMask enviromentLayers;

    [Header("SETTINGS")]
    [SerializeField] float updateEverySeconds = .5f;

    private IEnumerator findCorutine;
    private Collider[] collidersInViewRadius = new Collider[50];  //limit max number of Colliders in the list
    public List<Transform> avalibleTargets;
    public List<Transform> oldTargets;

    public float ViewDistance => viewDistance;
    public float FOV => fov;

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        avalibleTargets = new List<Transform>();
        oldTargets = new List<Transform>();
        StartSensor();
    }

    public void StopSensor()
    {
        if (findCorutine != null)
        {
            StopCoroutine(findCorutine);
            findCorutine = null;
        }
    }

    public void StartSensor()
    {
        if (findCorutine == null)
        {
            findCorutine = FindTargets();
            StartCoroutine(findCorutine);
        }

    }

    private IEnumerator FindTargets()
    {
        WaitForSeconds wait = new WaitForSeconds(updateEverySeconds);   //caching the wait (should lead to better performence it these cases)

        while (true)
        {
            yield return wait;
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        oldTargets = new List<Transform>(avalibleTargets);  //dont like this, get rid of the NEW somehow, stejne to blbne to porovnani, opravit
        avalibleTargets.Clear();

        int targets = Physics.OverlapSphereNonAlloc(transform.position, viewDistance, collidersInViewRadius, targetedLayers);

        for (int i = 0; i < targets; i++)
        {
            Transform target = collidersInViewRadius[i].transform;
            Vector3 targetPos = collidersInViewRadius[i].bounds.center;

            if (avalibleTargets.Contains(target.parent) || avalibleTargets.Contains(target)) continue;    //prevent repeated matches by leaving out one loop of for

            Vector3 dirToTarget = (targetPos - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToTarget) < fov / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, targetPos);

                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, enviromentLayers)) //test if match is hidden behind something
                {
                    avalibleTargets.Add(target);
                    InvokeEnterEvents(target);
                }
            }
        }

        //if any targets exited do an exit event
        if (oldTargets.Count > 0)
        {
            for (int i = 0; i < oldTargets.Count; i++)
            {
                Debug.Log(oldTargets[i].name + " exit");
                InvokeExitEvents(oldTargets[i]);
            }
            oldTargets.Clear();
        }
    }


    public Vector3 DirFromAngle(float angle, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angle += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }


}

