using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D cursor;
    public Texture2D cursorClicked;
    private CursorControls controls;
    private bool isCursorVisible = true;
    private Camera mainCamera;
    

    void Awake()
    {
        controls = new CursorControls();
        ChangeCursor(cursor);
        Cursor.lockState = CursorLockMode.Confined;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    private void Start()
    {
        controls.Mouse.Click.started += _ => StartedClick();
        controls.Mouse.Click.canceled += _ => EndedClick();
    }

    private void StartedClick()
    {
        ChangeCursor(cursorClicked);
    }

    private void EndedClick()
    {
        ChangeCursor(cursor);
        DetectObject();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void ChangeCursor(Texture2D cursorType)
    {
        Vector2 hotSpot = new Vector2(cursorType.width / 3, cursorType.height / 3);
        Cursor.SetCursor(cursorType, hotSpot, CursorMode.Auto);
    }

    public void ShowCursor()
    {
        if (isCursorVisible) return;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isCursorVisible = true;
    }

    public void HideCursor()
    {
        if (!isCursorVisible) return;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        isCursorVisible = false;
    }

    private void DetectObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(controls.Mouse.Position.ReadValue<Vector2>());
        RaycastHit[] hits = Physics.RaycastAll(ray, 200f);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("QuizItem"))
                {
                    Debug.Log("Hit Quiz object: " + hit.collider.tag);
                }
                else
                {
                    Debug.Log("Hit object: " + hit.collider.tag);
                }
            }


        }
    }
}
