using Frictionless;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private Vector3 _initialPosition;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _initialPosition = transform.position;
        
        ServiceFactory.Instance.Resolve<MessageRouter>().AddHandler<ToggleGravityMessage>(OnToggleGravity);
        ServiceFactory.Instance.Resolve<MessageRouter>().AddHandler<ResetStateMessage>(OnResetState);
    }

    private void OnDestroy()
    {
        ServiceFactory.Instance.Resolve<MessageRouter>().RemoveHandler<ToggleGravityMessage>(OnToggleGravity);
        ServiceFactory.Instance.Resolve<MessageRouter>().RemoveHandler<ResetStateMessage>(OnResetState);
    }

    private void OnToggleGravity(ToggleGravityMessage toggleGravityMessage)
    {
        _rigidbody.useGravity = !_rigidbody.useGravity;
        _rigidbody.velocity = Vector3.zero;
    }
    
    private void OnResetState(ResetStateMessage obj)
    {
        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;
        transform.position = _initialPosition;
    }
}
