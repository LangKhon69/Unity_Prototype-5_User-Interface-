using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(this.name);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.AddTorque(Vector3.right * 2f, ForceMode.Impulse);
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            rb.AddTorque(Vector3.right * -2f, ForceMode.Impulse);
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 2f);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {

                Debug.Log("Point: " + hit.point);
                Debug.Log("Distance: "+hit.distance);
                Debug.Log("Normal: "+hit.normal);
                    Debug.Log("Name: "+hit.collider.name);
                    Debug.Log("Tag: "+hit.collider.tag);
                    Debug.Log("Position: "+hit.transform.position);
                    Debug.Log(hit.collider.GetComponent<Rigidbody>());
                Debug.Log("Collider name :" + hit.collider.name + " Transform name: " + hit.transform.name);
                }
         }
      

    }

}
