using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour
{
    public GameObject launcher;

    private bool isAiming;
    private float sphereRadius;

    public GameObject prefabBall;
    public GameObject activeBall;
    public float speedMultiplier = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        launcher.SetActive(false);
        isAiming = false;
        sphereRadius = this.GetComponent<SphereCollider>().radius;
        activeBall = null;
    }

    void OnMouseEnter()
    {
        launcher.SetActive(true);
    }

    void OnMouseExit()
    {
        launcher.SetActive(false);
    }

    void OnMouseDown()
    {
        isAiming = true;
        activeBall = Instantiate(prefabBall) as GameObject;
        activeBall.transform.position = launcher.transform.position;
        activeBall.GetComponent<Rigidbody>().isKinematic = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isAiming)
            return;

        Vector3 mousePositionScreen = Input.mousePosition;
        mousePositionScreen.z = -Camera.main.transform.position.z;
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);
        Vector3 dragVector = mousePositionWorld - launcher.transform.position;

        if (dragVector.magnitude > sphereRadius)
        {
            dragVector.Normalize();
            dragVector *= sphereRadius;
        }

        activeBall.transform.position = launcher.transform.position + dragVector;

        if (Input.GetMouseButtonUp(0))
        {
            isAiming = false;
            Rigidbody rb = activeBall.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.velocity = -dragVector * speedMultiplier;
        }

    }
}
