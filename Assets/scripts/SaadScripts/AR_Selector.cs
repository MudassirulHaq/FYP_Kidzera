using UnityEngine;
using UnityEngine.InputSystem;

public class AR_Selector : MonoBehaviour
{

    [SerializeField] private Camera arCamera;
    private int rayDistance = 1000;

    private void Awake()
    {
        InputActions inputActions = new InputActions();
        inputActions.Default.Enable();
        inputActions.Default.Select.performed += OnSelect;

    }

    private void OnSelect(InputAction.CallbackContext ctx)
    {
        //Debug.Log("OnSelect");
        Vector2 selectScreenPosition = ctx.ReadValue<Vector2>();
        Ray ray = arCamera.ScreenPointToRay(selectScreenPosition);

        Debug.DrawRay(ray.origin, ray.direction, Color.blue, 1);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, rayDistance))
        {
            Debug.Log($"Hit Object: {hit.transform.name}");
            Animal animalComponent = hit.transform.GetComponentInParent<Animal>();
            if( animalComponent != null )
            {
                animalComponent.OnSelected();
            }
        }
    }
}
