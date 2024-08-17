using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    public UnityEvent OnArrive { get; private set; }
    [HideInInspector] public float Speed { get; set; }

    private Rigidbody2D _rb;
    private Vector3 _target;

    private void Awake() {
        OnArrive = new UnityEvent();
        _rb = GetComponent<Rigidbody2D>();

        _target = transform.position;
    }

    private Vector3 Direction { get {   Vector3 dir = (_target - transform.position);
                                        dir.z = 0;
                                        return dir.normalized; } }
    private float TargetDistance { get { return Vector3.Distance(_target, transform.position); } }

    public void SetTargetPoint(Vector3 target, UnityAction affterArrive = null) {
        OnArrive.RemoveAllListeners();

        if(affterArrive != null)
            OnArrive.AddListener(affterArrive);

        _target = target;
        StartCoroutine(MoveTo());
    }

    IEnumerator MoveTo() {
        while(TargetDistance > 0.1f) {
            _rb.velocity = Direction * Speed;
            transform.right = Direction;
            yield return null;
        }
        _rb.velocity = Vector2.zero;
        OnArrive.Invoke();
        OnArrive.RemoveAllListeners();
    }
}
