using UnityEngine;
using Frictionless;

public class Ball : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private Vector3 _initialPosition;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _initialPosition = transform.position;
        
        MessageRouter messageRouter = ServiceFactory.Instance.Resolve<MessageRouter>();
            
        // Can add handler normally and remove them via the remove handler function as in OnDestroy
        messageRouter.AddHandler<DropMessage>(OnDrop);
        messageRouter.AddHandler<ResetStateMessage>(OnResetState);
        messageRouter.AddHandler<DestroyAllMessage>(OnDestroyAll);
    }

    void OnDestroy()
    {
        MessageRouter messageRouter = ServiceFactory.Instance.Resolve<MessageRouter>();

        messageRouter.RemoveHandler<DropMessage>(OnDrop);
        messageRouter.RemoveHandler<ResetStateMessage>(OnResetState);
        messageRouter.RemoveHandler<DestroyAllMessage>(OnDestroyAll);
    }
    
    private void OnDrop(DropMessage dropMessage)
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(new Vector3(0.0f, -Random.value * dropMessage.Force,0.0f));
    }
    
    private void OnResetState(ResetStateMessage obj)
    {
        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector3.zero;
        transform.position = _initialPosition;
    }
    
    private void OnDestroyAll(DestroyAllMessage destroyAllMessage)
    {
        Destroy(gameObject);
    }
}