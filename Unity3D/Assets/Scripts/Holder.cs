using UnityEngine;
using System.Collections;

public class Holder : MonoBehaviour
{
    public Transform holding;
    private Animator animator;
    private UnityEngine.AI.NavMeshAgent navigation;
    private Vector3 target;
    private Vector3 offset;

    public Transform elbowHint;

    void Start()
    {
        animator = GetComponent<Animator>();
        navigation = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = transform.position;
        offset = transform.InverseTransformPoint(holding.position);
    }

    void Update()
    {
        Vector3 position = transform.TransformPoint(offset + Vector3.forward * Mathf.Sin(Time.time * 3f) * 0.5f);
        // position.y += Mathf.Sin(Time.time * 3f) * 0.5f;
        holding.position = position;

        if (navigation != null)
        {
            navigation.destination = target;
            if (Vector3.Distance(transform.position, target) < 1f)
            {
                target.x = Random.Range(-5f, 5f);
                target.z = Random.Range(-5f, 5f);
            }

            if (animator != null)
            {
                animator.SetFloat("BlendWalk", navigation.velocity.magnitude);
                animator.speed = 0.5f;
            }
        }
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (animator != null)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
            animator.SetIKPosition(AvatarIKGoal.RightHand, holding.position);

            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
            animator.SetIKRotation(AvatarIKGoal.RightHand, holding.rotation);

            if (elbowHint)
            {
                animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1f);
                animator.SetIKHintPosition(AvatarIKHint.RightElbow, elbowHint.position);
            }
        }
    }
}
