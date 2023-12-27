using UnityEngine;

namespace Services
{
    public class InputService : IInputService
    {
        public Vector2 GetMoveDirection() 
            => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        public bool IsDashInvoked()
            => Input.GetKey("space");
    }
}