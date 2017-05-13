using UnityEngine;

namespace Es.InkPainter.Sample
{
	[RequireComponent(typeof(Collider), typeof(MeshRenderer))]
	public class CollisionPainter : MonoBehaviour
	{
		public Gradient brushColors;
		public float movePaintIntervals;

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

			if (GameController.instance.playerScript.moveTimer < movePaintIntervals)
				return;
			GameController.instance.playerScript.moveTimer = 0;


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
				}
			}
		}
	}
}