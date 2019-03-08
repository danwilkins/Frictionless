using System;
using Frictionless;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private Vector3 _initialPosition;

    private Action _unregisterMessages;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _initialPosition = transform.position;

        MessageRouter messageRouter = ServiceFactory.Instance.Resolve<MessageRouter>();
        
        // Can also use the overload and pass in an action which caches the removing handlers
        messageRouter.AddHandler<ToggleGravityMessage>(OnToggleGravity, ref _unregisterMessages);
        messageRouter.AddHandler<ResetStateMessage>(OnResetState, ref _unregisterMessages);
        messageRouter.AddHandler<DestroyAllMessage>(OnDestroyAll, ref _unregisterMessages);
    }

    private void OnDestroy()
    {
        _unregisterMessages?.Invoke();
    }

    private void OnToggleGravity(ToggleGravityMessage toggleGravityMessage)
    {
        _rigidbody.useGravity = !_rigidbody.useGravity;
        _rigidbody.velocity = Vector3.zero;
    }

    private void OnResetState(ResetStateMessage resetStateMessage)
    {
        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;
        transform.position = _initialPosition;
    }
    
    private void OnDestroyAll(DestroyAllMessage destroyAllMessage)
    {
        Destroy(gameObject);
    }
}