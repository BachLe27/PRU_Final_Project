using UnityEngine;

namespace Assets.Scripts
{
    public class Magnet : MonoBehaviour
    {
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<Gold>(out Gold gold))
            {
                gold.SetTarget(transform.parent.position);
            }
        }
    }
}
