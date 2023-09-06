using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class AnimationCurveBehaviour : MonoBehaviour
{
    public AnimationCurve animCurve;
    public GameObject animObject;
    public Vector3 animTargetPos;

    [Space(10)] 
    public GameObject normalObject;
    public Vector3 normalTargetPos;

    [Space(10)] 
    public float targetOffset = .0f;
    public bool isStarted = false;


    [Button("Set Target Pos")]
    private void SetTargetPos()
    {
        
        animTargetPos = animObject.transform.position;
        animTargetPos.x += targetOffset;

        normalTargetPos = normalObject.transform.position;
        normalTargetPos.x += targetOffset;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || isStarted)
        {
            isStarted = true;

            MoveNormal();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(MoveAnimCurve());
        }
    }

    private void MoveNormal()
    {
        if (Vector3.Distance(normalObject.transform.position, normalTargetPos) > 0.1f)
        {
            normalObject.transform.Translate(Vector3.right * 15f * Time.deltaTime);
        }
    }

    private IEnumerator MoveAnimCurve()
    {
        float timer = .0f;
        float activeTime = 3f;

        
        while (timer < activeTime)
        {
            animObject.transform.position = new Vector3(animCurve.Evaluate(timer / activeTime) * animTargetPos.x,
                animObject.transform.position.y,
                animObject.transform.position.z);

            yield return null;

            timer += Time.deltaTime / activeTime;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(animTargetPos, 0.1f);
        Gizmos.DrawLine(animObject.transform.position, animTargetPos);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(normalTargetPos, 0.1f);
        Gizmos.DrawLine(normalObject.transform.position, normalTargetPos);
    }
}