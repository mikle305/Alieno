using UnityEngine;

namespace GamePlay.Other.Navigators
{
    public class CursorMovement : MonoBehaviour
    {
        /*
    [SerializeField] private Camera _camera;
    
    private Plane _plane = new Plane(Vector3.down, 0);

    void Update()
    { 
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        Vector3 pos = FindCursorPosition(ray);

        transform.position = pos;
    }

    private Vector3 FindCursorPosition(Ray ray)
    {
        if (_plane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        else
        {
            return Vector3.zero;
        }
    }
*/
    }
}
