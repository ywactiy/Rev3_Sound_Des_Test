using UnityEngine;

public sealed class Footsteps_Start : MonoBehaviour
{
    [SerializeField] private string _movementStartedEventId = default;
    [SerializeField] private string _movementEndedEventId = default;

    private Rigidbody _rigidbody;
    private Vector3 _previousVelocity;

    private bool _hasStarted = false;
    private bool _hasEnded = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var currentVelocity = _rigidbody.velocity;

        if (_hasStarted == false && _hasEnded == true)
        {
            if (Mathf.Approximately(currentVelocity.magnitude, 0) && _previousVelocity.magnitude > 0)
            {
                _hasStarted = true;
                _hasEnded = false;

                AkSoundEngine.PostEvent(_movementStartedEventId, gameObject);
            }
        }

        if (_hasEnded == false && _hasStarted == true)
        {
            if (currentVelocity.magnitude > 0 && Mathf.Approximately(_previousVelocity.magnitude, 0))
            {
                _hasStarted = false;
                _hasEnded = true;

                AkSoundEngine.PostEvent(_movementEndedEventId, gameObject);
            }
        }

        _previousVelocity = _rigidbody.velocity;
    }
}