using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TwoHandGrabInteraction : XRGrabInteractable
{
    [SerializeField]
    public Transform _secondAttachTransform;

    protected override void Awake()
    {
        base.Awake();
        selectMode = InteractableSelectMode.Multiple;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (interactorsSelecting.Count == 1)
        {
            base.ProcessInteractable(updatePhase);
        }
        else if(interactorsSelecting.Count == 2 && 
            updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            ProcessDoublegrip();
        }        
    }

    private void ProcessDoublegrip()
    {
        Transform firstAttach = GetAttachTransform(null);
        Transform firstHand = interactorsSelecting[0].transform;
        Transform secondAttach = _secondAttachTransform;
        Transform secondHand = interactorsSelecting[1].transform;

        Vector3 directionBetweenHands = secondHand.position - firstHand.position;
        Vector3 directionBetweenAttaches = secondAttach.position - firstAttach.position;

        Quaternion rotationFromAttachToForward = Quaternion.FromToRotation(directionBetweenAttaches, transform.forward);
        Quaternion targetRotation = Quaternion.LookRotation(directionBetweenHands, firstHand.up);

        Vector3 worldDirectionFromHandleToBase = transform.position - firstAttach.position;
        Vector3 localDirectionFromHandleToBase = transform.InverseTransformDirection(worldDirectionFromHandleToBase);

        Vector3 targetPosition = firstHand.position + targetRotation * localDirectionFromHandleToBase;

        transform.SetPositionAndRotation(targetPosition, targetRotation);
    }

    protected override void Grab()
    {
        if(interactorsSelecting.Count == 1)
        {
            base.Grab();
        }        
    }

    protected override void Drop()
    {
        if (!isSelected)
        {
            base.Drop();
        }
    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        if(interactorsSelecting[0] == args.interactorObject)
        {
            base.OnActivated(args);
        }
    }

    protected override void OnDeactivated(DeactivateEventArgs args)
    {
        if(interactorsSelecting[0] == args.interactorObject)
        {
            base.OnDeactivated(args);
        }
    }
}
