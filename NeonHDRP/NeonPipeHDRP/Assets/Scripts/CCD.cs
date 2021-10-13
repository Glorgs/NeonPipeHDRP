using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCD : MonoBehaviour
{
    public Transform[] Bones; //De la pointe en 0 puis remonter la chaine
    public Transform Target;
    public Vector3 TargetNormal;
    private Transform TargetTransform;

    public int NbBonesLimit = 100;
    public int NbCycles = 5;
    public float SmoothTrack = 0.8f;
    public float MaxBlendingWithAnimation = 0.5f;
    public float TouchDistance = 0.1f;

    public bool Touch = false;

    private float blending = 0.0f;
    private float newBlending = 0.0f;

    private Vector3 targetCCD;
    private Vector3 newTargetCCD;

    protected virtual bool defineNewTarget(Vector3 lastTarget, out Vector3 newTarget, Vector3 lastTargetNormal, out Vector3 newTargetNormal, out Transform newTransform)
    {
        newTarget = lastTarget;
        newTargetNormal = lastTargetNormal;
        newTransform = null;

        if (Target != null)
        {
            newTarget = Target.position;
            newTargetNormal = Target.up;
            newTransform = Target;
            return true;
        }

        return false;
    }

    protected virtual float defineNewBlendingWithAnim(bool foundNewTarget)
    {
        if (!foundNewTarget)
            return 0.0f;
        return MaxBlendingWithAnimation;
    }

    // Late pour passer après l'animation
    void LateUpdate()
    {
        //Determine les nouveaux targets et blendings
        bool targetFound = defineNewTarget(newTargetCCD, out newTargetCCD, TargetNormal, out TargetNormal, out TargetTransform);
        newBlending = defineNewBlendingWithAnim(targetFound);

        //Smooth les params
        blending = Mathf.Lerp(blending, newBlending, 0.1f);
        targetCCD = Vector3.Lerp(targetCCD, newTargetCCD, 1 - Mathf.Pow(1 - SmoothTrack, Time.deltaTime));

        //CCD
        for (int i = 0; i < NbCycles; i++)
        {
            doCCD(targetCCD, blending);
        }

        //Est ce qu'on touche
        Touch = targetFound && (Bones[0].position - targetCCD).magnitude < TouchDistance;
    }

    private void doCCD(Vector3 targetPos, float blending)
    {
        for (int i = 1; i < Bones.Length && i <= NbBonesLimit; i++)
        {
            Vector3 directionActuelle = (Bones[0].position - Bones[i].position).normalized;
            Vector3 directionDesiree = (targetPos - Bones[i].position).normalized;
            Quaternion rotation = Quaternion.FromToRotation(directionActuelle, directionDesiree.normalized);
            Bones[i].rotation = Quaternion.Lerp(Bones[i].rotation, (rotation) * Bones[i].rotation, blending);
        }
    }

    public Vector3 getEndChainPosition()
    {
        return Bones[0].position;
    }

    public Vector3 getStartChainPosition()
    {
        return Bones[Bones.Length - 1].position;
    }
}
