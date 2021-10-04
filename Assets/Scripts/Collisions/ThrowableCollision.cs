using System;
using Objects;
using UnityEngine;

namespace Collisions
{
    public class ThrowableCollision : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag.Equals("Waiter"))
            {
                Waiter.Instance.DropItem();
                Destroy(transform.gameObject);
            }

            if (other.CompareTag("Border"))
            {
                Destroy(transform.gameObject);
            }
        }
    }
}
