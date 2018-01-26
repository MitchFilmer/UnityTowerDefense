
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float panSpeed = 15f;
	public float scrollSpeed = 100f;

    void Update ()
    {

        Vector3 pos = transform.position;

        if(Input.GetKey("w"))
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += panSpeed * Time.deltaTime;
        }

		float scroll = Input.GetAxis("Mouse ScrollWheel");
		pos.y -= scroll * scrollSpeed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -25.0f, 25.0f);
		pos.y = Mathf.Clamp(pos.y, 25.0f, 70.0f);
        pos.z = Mathf.Clamp(pos.z, -40.0f, 15.0f);


        transform.position = pos;

    }
}
