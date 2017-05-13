using UnityEngine;

namespace Es.InkPainter.Sample
{
	[RequireComponent(typeof(Collider), typeof(MeshRenderer))]
	public class CollisionPainter : MonoBehaviour
	{
		public Gradient brushColors;
		public float movePaintIntervals;

		//public GameObject TestPrefab;
		public ParticleSystem SplatDropPS;

		[SerializeField]
		private Brush brush = null;

		[SerializeField]
		private int wait = 3;

		private int numberUpdatesToSkip;

		public void Awake()
		{
			GetComponent<MeshRenderer>().material.color = brush.Color;
		}

		public void FixedUpdate()
		{
			++numberUpdatesToSkip;
		}

		public void OnCollisionStay(Collision collision)
		{
			//Debug.Log ("Got Collision");
			if(numberUpdatesToSkip < wait)
				return;
			numberUpdatesToSkip = 0;

			if (GameController.instance.playerScript.moveObjectsPaintTimer < movePaintIntervals || GameController.instance.playerScript.mud <= 0)
				return;
			GameController.instance.playerScript.moveObjectsPaintTimer = 0;


			Debug.Log (collision.contacts.Length);

			foreach(var p in collision.contacts)
			{
				var canvas = p.otherCollider.GetComponent<InkCanvas>();
				//Debug.Log ("Got surf " + canvas.gameObject.name);
				if (canvas != null)
				{
					brush.Color = brushColors.Evaluate (Random.Range(0f,1f));
					brush.Scale = Random.Range (0.01f, 0.1f) * canvas.UVScale;
					canvas.Paint (brush, p.point);
					//Debug.Log ("Painted");

					SplatDropPS.transform.position = p.point;
					SplatDropPS.Emit (3);

//					Ray ray = new Ray (transform.position, (p.point - transform.position).normalized);
//					RaycastHit outHit;
//					if (Physics.Raycast (ray, out outHit, 5f, 1 << 14))
//					{
//						Debug.DrawRay (p.point, outHit.normal);
//						Instantiate(TestPrefab, p.point, Quaternion.LookRotation (outHit.normal));
//					}
				}
			}
		}
	}
}