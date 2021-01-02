﻿namespace PixelPlatformer
{
    using UnityEngine;

    public class CameraController : MonoBehaviour
    {
        public float constYPos;
        public float minXPos;
        public float interpVelocity;
        public float minDistance;
        public float followDistance;
        public GameObject target;
        public Vector3 offset;
        Vector3 targetPos;
        
        private void Start()
        {
            targetPos = transform.position;
        }

        private void FixedUpdate()
        {
            if (target)
            {
                Vector3 posNoZ = transform.position;
                posNoZ.z = target.transform.position.z;

                Vector3 targetDirection = (target.transform.position - posNoZ);

                interpVelocity = targetDirection.magnitude * 5f;

                targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);
                
                targetPos.y = constYPos;

                if (targetPos.x < minXPos)
                    targetPos.x = minXPos;
                transform.position = Vector3.Lerp(transform.position, targetPos + offset, 1f);
            }
        }
    }
}
