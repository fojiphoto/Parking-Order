using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hurdle : MonoBehaviour
{
    [SerializeField] private GameObject ReferenceObj;
    [SerializeField] private Vector3 TargetPos;
    private Vector3 PreviousPos;
    [SerializeField] private bool isRotate=false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            if (isRotate)
            {
                PreviousPos = ReferenceObj.transform.eulerAngles;
                ReferenceObj.transform.DORotate(TargetPos, 1);
            }
            else 
            {
                PreviousPos = ReferenceObj.transform.position;
                ReferenceObj.transform.DOMove(TargetPos, 1);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            if (isRotate)
            {
                ReferenceObj.transform.DORotate(PreviousPos, 1);
            }
            else 
            {
            ReferenceObj.transform.DOMove(PreviousPos,1);
            }
        }
    }
}
