using UnityEngine;

public class Gunrotation : MonoBehaviour
{
    //declaring the x and y sensitivity
    public float sensX;
    public float sensY;

    //declaring the use of another object's orientation
    public Transform orientation;

    //declaring both the x and y rotations
    float xRotation;
    float yRotation;

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = -Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}