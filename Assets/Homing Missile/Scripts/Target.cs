using UnityEngine;

namespace Tarodev
{
    public class Target : MonoBehaviour
    {
        private Rigidbody _rb;

        public Rigidbody Rb
        {
            get { return _rb; }
        }

        private void Start()
        {
            _rb = gameObject.GetComponent<Rigidbody>();
        }
    }
}
