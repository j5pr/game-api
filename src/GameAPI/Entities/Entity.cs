using GameAPI.Async;
using GameAPI.Async.Generic;
using GameAPI.Items;
using GameAPI.Tasks;
using GameAPI.Namespacing;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameAPI.Entities
{
    public partial class Entity<TType> : TaskedBehaviour<TType> where TType: Entity<TType>
    {
        [Header("Entity")]
        public Key Id = Key.Unique("entity");

        [Space(10)]
        [Header("Statistics")]
        public int Health = 1;
        public int MaxHealth = 1;
        public int Damage = 1;

        [Header("Movement")]
        public float Speed = 100f;
        public float RotationSpeed = 1f;
        public float InteractionRange = 0.5f;

        [Header("World")]
        public float WalkDrag = 1.0f;
        public float HaltDrag = 1.75f;

        [Header("Target")]
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

            void Move()
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

            void LookAt()
            {
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
        }

        public VoidAwaitable Rotate(Vector3 target)
        {
            LookingAt = target;
        
            return new VoidAwaitable(new Until(() => LookingAt == null).GetAwaiter());
        }

        public VoidAwaitable Rotate<TTarget>(TTarget target) where TTarget : Component =>
            Rotate(target.transform.position);

        public VoidAwaitable Move(Vector3 target, bool look = true)
        {
            Target = target;
            LookingAt = look ? target : LookingAt;

            return new VoidAwaitable(new Until(() => Target == null).GetAwaiter());   
        }

        public VoidAwaitable Move<TTarget>(TTarget target, bool look = true) where TTarget : Component =>
            Move(target.transform.position, look);

        public void LookAt(Vector3 target) =>
            transform.rotation = Quaternion.LookRotation((target - transform.position).normalized);

        public void LookAt<TTarget>(TTarget target) where TTarget : Component =>
            LookAt(target.transform.position);

        public void Teleport(Vector3 target) =>
            transform.position = target;

        public void Teleport<TTarget>(TTarget target) where TTarget : Component =>
            Teleport(target.transform.position);

        public bool IsAt(Vector3 target) =>
            (target - transform.position).sqrMagnitude <= InteractionRange * InteractionRange;

        public bool IsAt<TTarget>(TTarget target) where TTarget : Component =>
            IsAt(target.transform.position);

        public void Destroy() =>
            Destroy(gameObject);

        public TTarget? Nearest<TTarget>(List<TTarget>? targets = null) where TTarget: Component =>
        (
            from target in targets ?? new List<TTarget>(FindObjectsOfType<TTarget>())
            let distance = Vector2.Distance(transform.position, target.transform.position)
            where distance > 0
            orderby distance
            select target
        ).FirstOrDefault();
    }

    public partial class Entity<TType> : TaskedBehaviour<TType>
    {
        public static new Entity<TType> New(string name = "Entity") =>
            Derive(name).AddComponent<Entity<TType>>();

        protected static new GameObject Derive(string name = "Entity")
        {
            GameObject entity = TaskedBehaviour<TType>.Derive(name);

            entity.AddComponent<Rigidbody>();

            return entity;
        }
    }

    public class Entity : Entity<Entity> { }
}
