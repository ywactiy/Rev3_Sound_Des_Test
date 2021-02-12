using UnityEngine;

namespace TestTask
{
    public sealed class FootstepsSoundPlayer : MonoBehaviour
    {
        [SerializeField] private AK.Wwise.Event _footStepsStart;
        [SerializeField] private AK.Wwise.Event _footStepsEnd;

        private Vector3 _previousVelocity;

        private bool _hasStarted = false;
        private bool _hasEnded = true;

        internal void ReportVelocity(Vector3 currentVelocity)
        {
            if (_hasStarted == false && _hasEnded == true)
            {
                if (currentVelocity.magnitude > 0 && Mathf.Approximately(_previousVelocity.magnitude, 0))
                {
                    _hasStarted = true;
                    _hasEnded = false;

                    _footStepsStart.Post(gameObject);
                }
            }

            if (_hasEnded == false && _hasStarted == true)
            {
                if (Mathf.Approximately(currentVelocity.magnitude, 0) && _previousVelocity.magnitude > 0)
                {
                    _hasStarted = false;
                    _hasEnded = true;

                    _footStepsEnd.Post(gameObject);
                }
            }

            _previousVelocity = currentVelocity;
        }
    }
}