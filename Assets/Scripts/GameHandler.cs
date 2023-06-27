using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    /// Handles Camera and Movement ///

    // Camera //
    public CameraFollow cameraFollow;
    private Vector3 cameraFollowPosition;
    public GameObject Player_HQ;
    // Booleans //
    private bool isEdge = false;
    private bool isToggle = false;
    // Variables //
    private int i;
    private float zoom = 50f;

    private Vector3 lastKnownPos;

    // COMMANDS -> T, SPACE, Z


    // Start is called before the first frame update
    private void Start()
    {
        cameraFollow.SetUp(() => cameraFollowPosition, () => zoom);
    }
    private void Update()
    {
        // Camera //
        HandleManualMovement();
        HandleEdgeMovement();
        HandleMouseZoom();
        ToggleBetweenUnits();
        HandleQuickReturn();
    }
    private void HandleQuickReturn()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cameraFollowPosition = Player_HQ.transform.position;
        }
    }
    private void ToggleBetweenUnits()
    {
        // Toggle Camera For Units //
        if (Input.GetKeyDown(KeyCode.T))
        {
            //Toggle //
            isToggle = !isToggle;
        }
        if (isToggle)
        {
            // Groups and Units //
            RTSGroup[] GROUPS = GameObject.FindObjectsOfType<RTSGroup>();
            RTSUnit[] UNITS = GameObject.FindObjectsOfType<RTSUnit>();
            // Toggle Left and Right //
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (i > GROUPS.Length)
                {
                    i = 0;
                }
                i += 1;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (i < 0)
                {
                    i = GROUPS.Length;
                }
                i -= 1;
            }
            if(GROUPS.Length > 0)
            {
                // Curr Group //
                RTSGroup ToggleGroup = GROUPS[i];
                Vector3 GroupPos = ToggleGroup.GetComponent<Transform>().position;
                lastKnownPos = GroupPos;
                // Set Cam On Group //
                cameraFollow.SetCameraFollowPos(GroupPos);
                cameraFollow.SetCameraZoom(40f);
            }
            else
            {
                cameraFollowPosition = lastKnownPos;
                isToggle = !isToggle;
            }
        }
        else
        {
            cameraFollow.SetCameraFollowPos(cameraFollowPosition);
            cameraFollow.SetCameraZoom(zoom);
        }
    }
    private void HandleMouseZoom()
    {
        float zoomChangeAmount = 80f;
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            zoom -= zoomChangeAmount * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            zoom += zoomChangeAmount * Time.deltaTime;
        }
        if(Input.mouseScrollDelta.y > 0)
        {
            zoom -= zoomChangeAmount * Time.deltaTime * 10f;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            zoom += zoomChangeAmount * Time.deltaTime * 10f;
        }
        zoom = Mathf.Clamp(zoom, 20f, 80f);
    }
    private void HandleManualMovement()
    {
        // Camera Controls //
        float moveAmount = 50f;
        if (Input.GetKey(KeyCode.W))
        {
            cameraFollowPosition.y += moveAmount * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            cameraFollowPosition.y -= moveAmount * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            cameraFollowPosition.x += moveAmount * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            cameraFollowPosition.x -= moveAmount * Time.deltaTime;
        }
    }
    private void HandleEdgeMovement()
    {
        float moveAmount = 50f;
        // Edge Scrolling //
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isEdge = !isEdge;
        }
        if (isEdge)
        {
            float edgeSize = 50f;
            if (Input.mousePosition.x > Screen.width - edgeSize)
            {
                cameraFollowPosition.x += moveAmount * Time.deltaTime;
            }
            if (Input.mousePosition.x < edgeSize)
            {
                cameraFollowPosition.x -= moveAmount * Time.deltaTime;
            }
            if (Input.mousePosition.y > Screen.height - edgeSize)
            {
                cameraFollowPosition.y += moveAmount * Time.deltaTime;
            }
            if (Input.mousePosition.y < edgeSize)
            {
                cameraFollowPosition.y -= moveAmount * Time.deltaTime;
            }
        }
    }
    private void ZoomIn()
    {
        zoom -= 30f;
        if(zoom < 20f)
        {
            zoom = 20f;
        }
    }
    private void ZoomOut()
    {
        zoom += 30f;
        if (zoom < 80f)
        {
            zoom = 80f;
        }
    }
}
