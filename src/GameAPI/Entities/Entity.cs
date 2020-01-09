using System.Collections.Generic;
using UnityEngine;
using GameAPI.Items;
using GameAPI.Tasks;

namespace GameAPI.Entities
{
    public partial class Entity<TType> : TaskedBehaviour<TType> where TType: Entity<TType>
    {
        public int Health = 1;
        public int MaxHealth = 1;
        public int Damage = 1;
        public float Speed = 100f;
        public float RotationSpeed = 1f;
        public float InteractionRange = 0.5f;

        public float WalkDrag = 1.0f;
        public float HaltDrag = 1.75f;
        public Vector3? Target = null;
        public Vector3? LookingAt = null;

        public readonly Inventory Inventory = new Inventory();

#nullable disable // If it's null, a runtime error should be present

        protected Rigidbody body;

#nullable restore

        public void Start()
        {
            body = GetComponent<Rigidbody>();
        }

        public void Update()
        {
            if (Health <= 0)
                Destroy(gameObject);

            if (Health > MaxHealth)
                Health = MaxHealth;

            TaskLoop();

            Move();
            LookAt();
        }

        private void LookAt() {
            if (LookingAt != null) {
                Quaternion direction = Quaternion.LookRotation(((Vector3)LookingAt - transform.position).normalized);

                transform.rotation = Quaternion.Slerp(transform.rotation, direction, Time.deltaTime * RotationSpeed);
                
                if (!direction.Equals(transform.rotation.normalized))
                    return;
            }

            if (Target != null && Target == LookingAt) // Lock on until target position is reached
                return;
            
            LookingAt = null;
        }

        private void Move()
        {
            if (Target != null && !IsAt((Vector3)Target))
            {
                body.drag = WalkDrag;
                body.AddForce(((Vector3)Target - transform.position).normalized * Speed * Time.deltaTime);

                return;
            }
            
            body.drag = HaltDrag;
            Target = null;
        }

        public void RotateTowards(Vector3 target) =>
            LookingAt = target;

        public void RotateTowards<TTarget>(TTarget target) where TTarget : Component =>
            RotateTowards(target.transform.position);

        public void MoveTowards(Vector3 target, bool look = true) => 
            (Target, LookingAt) = (target, look ? target : LookingAt);

        public void MoveTowards<TTarget>(TTarget target, bool look = true) where TTarget : Component =>
            MoveTowards(target.transform.position, look);

        public void LookAt(Vector3 target) =>
            transform.rotation = Quaternion.LookRotation((target - transform.position).normalized);

        public void LookAt<TTarget>(TTarget target) where TTarget : Component =>
            LookAt(target.transform.position);

        public void TeleportTo(Vector3 target) =>
            transform.position = target;

        public void TeleportTo<TTarget>(TTarget target) where TTarget : Component =>
            TeleportTo(target.transform.position);

        public bool IsAt(Vector3 target) =>
            (target - transform.position).sqrMagnitude <= InteractionRange * InteractionRange;

        public bool IsAt<TTarget>(TTarget target) where TTarget : Component =>
            IsAt(target.transform.position);

        public TTarget? Nearest<TTarget>(List<TTarget>? targets = null) where TTarget: Component
        {
            TTarget? closest = null;
            float minimumDistance = Mathf.Infinity;

            foreach (TTarget target in targets ?? new List<TTarget>(FindObjectsOfType<TTarget>()))
            {
                if (target.transform == transform)
                    continue;

                float distance = Vector2.Distance(transform.position, target.transform.position);

                if (distance < minimumDistance)
                {
                    closest = target;
                    minimumDistance = distance;
                }
            }

            return closest;
        }
    }

    public partial class Entity<TType> : TaskedBehaviour<TType>
    {
        public static new Entity New(string name = "Entity")
        {
            return Derive(name).AddComponent<Entity>();
        }

        protected static new GameObject Derive(string name = "Entity")
        {
            GameObject entity = TaskedBehaviour<Entity>.Derive(name);

            entity.AddComponent<Rigidbody>();

            return entity;
        }
    }

    public class Entity : Entity<Entity> { }
}
