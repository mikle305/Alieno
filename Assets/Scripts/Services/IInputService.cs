using UnityEngine;

namespace Services
{
    public interface IInputService
    {
        public Vector2 GetMoveDirection();
        public bool IsDashInvoked();
    }
}