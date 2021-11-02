using UnityEngine;
using System.Collections;

public class WaterMill : MonoBehaviour 
{
	public float Strength = 100f;
	public float distanceS = 10f;	
	public int Direction = 1;
    [SerializeField]
	private Transform millObj;
	private Rigidbody thisRd;
    [SerializeField]
	private Transform millRod;
    private Vector3 previous;
	private bool InZone;
    private int dir = 1;

    void Awake()
	{
		thisRd = millObj.GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
        //thisRd.AddTorque(new Vector3(0,0,0.1f*dir));
        if (InZone)
		{

            Vector3 directionTo = millRod.position - millObj.position;
			float distance = Vector3.Distance(millRod.position, millObj.position);
            int height = -1;
            if (millObj.position.y > millRod.position.y)
            {
                height = 1;
            }
            if (((((millRod.position - previous).x) / Time.deltaTime) / distance) < 0)
            {
                dir = 1;
            }
            else if (((((millRod.position - previous).x) / Time.deltaTime) / distance) > 0)
            {
                dir = -1;
            }
            float DistanceStr = (distanceS / distance) * Strength;
            thisRd.AddTorque(DistanceStr * (directionTo * Direction) * ((((millRod.position - previous).x) / Time.deltaTime) / distance * height), ForceMode.Force);
            //trans.transform.Rotate(new Vector3(0, 0, 1.5f));
            previous = millRod.position;
        }
        
    }

	//void OnTriggerEnter (Collider other)
	//{
	//	if (other.tag == "Player")
	//	{
	//		playerTrans = other.transform;
	//		InZone = true;
	//	}
	//}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("PlayerHitbox"))
        {
            //millRod = collision.transform;
            InZone = true;
        }
    }

    void OnTriggerExit (Collider other)
	{
	//	if (other.tag == "Magnet" && looseMagnet)
//		{
	//		magnetInZone = false;
		//}
	}
}






