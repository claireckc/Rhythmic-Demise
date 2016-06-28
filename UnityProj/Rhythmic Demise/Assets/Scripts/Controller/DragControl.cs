using UnityEngine;
using System.Collections;

public class DragControl : MonoBehaviour {
    private Vector2 swipePos, currentPos, previousFrame;
    private Vector3 camPos;
    private float vertExtent,horExtent, camWidth;
    private float maxY, minY;
    private float maxX, minX;
    private Renderer map;

    // Use this for initialization
    void Start() {
        //background boundary
        map = GetComponent<Renderer>();
        maxY = map.bounds.size.y / 2 + map.transform.position.y;
        minY = map.transform.position.y - map.bounds.size.y / 2;

        maxX = map.bounds.size.x / 2 + map.transform.position.x;
        minX = map.transform.position.x - map.bounds.size.x / 2;

        //half of the camera viewport(height)
        vertExtent = Camera.main.orthographicSize;
        camWidth = (vertExtent * 2) * Camera.main.aspect;
        horExtent = camWidth / 2;
    }

    // Update is called once per frame
    void Update() {

    }

    void OnMouseDown()
    {
        previousFrame = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    void OnMouseDrag()
    {
        currentPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if (previousFrame != currentPos)
        {
            swipePos = currentPos - previousFrame;
            swipePos.Normalize();
            Camera.main.transform.Translate(new Vector3(-swipePos.x, 0));
           // Camera.main.transform.Translate(new Vector3(0, -swipePos.y));
            camPos = Camera.main.transform.position;

            /*if (camPos.y + vertExtent > maxY || camPos.y - vertExtent < minY)
            {
                camPos.y = Mathf.Clamp(camPos.y, minY + vertExtent, maxY - vertExtent);
                Camera.main.transform.position = camPos;
            }*/

            if (camPos.x + horExtent > maxX || camPos.x - horExtent < minX)
            {
                camPos.x = Mathf.Clamp(camPos.x, minX + horExtent, maxX - horExtent);
                Camera.main.transform.position = camPos;
            }

            previousFrame = currentPos;
        }
    }
}
