﻿namespace Zinnia.Tracking.Follow.Modifier.Property.Rotation
{
    using UnityEngine;
    using Zinnia.Extension;

    /// <summary>
    /// Updates the rigidbody by applying force at the position difference between the source and the attachment point providing a torque rotation.
    /// </summary>
    public class RigidbodyForceAtPosition : PropertyModifier
    {
        [Tooltip("The point where the attachment was made.")]
        [SerializeField]
        private GameObject attachmentPoint;
        /// <summary>
        /// The point where the attachment was made.
        /// </summary>
        public GameObject AttachmentPoint
        {
            get
            {
                return attachmentPoint;
            }
            set
            {
                attachmentPoint = value;
            }
        }

        /// <summary>
        /// A cached version of the target rigidbody.
        /// </summary>
        protected Rigidbody cachedTargetRigidbody;
        /// <summary>
        /// A cached version of the target.
        /// </summary>
        protected GameObject cachedTarget;

        /// <summary>
        /// Clears <see cref="AttachmentPoint"/>.
        /// </summary>
        public virtual void ClearAttachmentPoint()
        {
            if (!this.IsValidState())
            {
                return;
            }

            AttachmentPoint = default;
        }

        /// <summary>
        /// Applies a force at the attachment point position to the target rigidbody creating torque for rotation.
        /// </summary>
        /// <param name="source">The source to utilize in the modification.</param>
        /// <param name="target">The target to modify.</param>
        /// <param name="offset">The offset of the target against the source when modifying.</param>
        protected override void DoModify(GameObject source, GameObject target, GameObject offset = null)
        {
            cachedTargetRigidbody = cachedTargetRigidbody == null || target != cachedTarget ? target.TryGetComponent<Rigidbody>(true) : cachedTargetRigidbody;
            cachedTarget = target;

            if (cachedTargetRigidbody == null || source == null || AttachmentPoint == null)
            {
                return;
            }

            Vector3 attachmentPointPosition = AttachmentPoint.transform.position;
            Vector3 rotationForce = source.transform.position - attachmentPointPosition;
            cachedTargetRigidbody.AddForceAtPosition(rotationForce, attachmentPointPosition, ForceMode.VelocityChange);
        }
    }
}