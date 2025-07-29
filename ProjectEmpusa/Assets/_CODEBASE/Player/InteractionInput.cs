using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;


public class InteractionInput : MonoBehaviour
{
    public InputActionReference interactionAsset;
    [SerializeField] float interactionRadius;
    [SerializeField] LayerMask mask;
    [SerializeField] Camera cam;

    private RaycastHit[] sphereResults = new RaycastHit[20];
    private IInteractable closestInteractable;
    private IInteractable previousInteractable;

    private void Awake()
    {
        interactionAsset.action.performed += ActionButtonPressed;
    }

    void ActionButtonPressed(InputAction.CallbackContext cotext)
    {
        if(closestInteractable != null)
        {
            closestInteractable.Interact();
        }
    }

    private void FixedUpdate()
    {

        closestInteractable = null;

        int results = Physics.SphereCastNonAlloc(transform.position, interactionRadius, Vector3.up, sphereResults, Mathf.Infinity,mask.value);

        float closestDist = interactionRadius;

       

        for (int i = 0; i < results; i++)
        {
            float dist = Vector3.Distance(transform.position, sphereResults[i].transform.position);

            if (closestDist > dist)
            {
                closestDist = dist;
                closestInteractable = sphereResults[i].transform.GetComponent<IInteractable>();
            }
        }

        closestDist = interactionRadius;

        

        if (closestInteractable != previousInteractable)
        {
            closestInteractable?.Hover();
            previousInteractable?.HoverExit();
        }
            

        previousInteractable = closestInteractable;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,interactionRadius);
    }
#endif
}
