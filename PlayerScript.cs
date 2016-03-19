using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public string Name = "Juba";
    public float health = 100f;
    public float damange = 30f;
    public float range;

    private NavMeshAgent agent;
    //public Transform target;
    public Camera myCamera;
    public GameObject marker;

    
    void Awake () {
        marker = GameObject.Find("Target");
        agent = GetComponent<NavMeshAgent>();
    }

	void Update () {
        Move();
    }

    void Attack()
    {

    }
    void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            marker.SetActive(true);
            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 200))
            {
                marker.transform.position = hit.point;
            }
        }
        if(Vector3.Distance(marker.transform.position, transform.position) <= 1)
        {
            marker.SetActive(false);
        }

        agent.SetDestination(marker.transform.position);
    }
}
