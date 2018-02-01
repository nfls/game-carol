using System.Collections.Generic;
using UnityEngine;

public class HeavenController : MonoBehaviour {

	private static Queue <GameObject> snow_flakes_1 = new Queue <GameObject> ();
	private static Queue <GameObject> snow_flakes_2 = new Queue <GameObject> ();
	private static Queue <GameObject> snow_flakes_3 = new Queue <GameObject> ();
	private static Queue <GameObject> snow_flakes_4 = new Queue <GameObject> ();
	private static Queue <GameObject> snow_flakes_5 = new Queue <GameObject> ();
	private static Queue <GameObject> ginger_mans = new Queue <GameObject> ();

	private static GameObject prototype_snow_flake_1;
	private static GameObject prototype_snow_flake_2;
	private static GameObject prototype_snow_flake_3;
	private static GameObject prototype_snow_flake_4;
	private static GameObject prototype_snow_flake_5;
	private static GameObject prototype_ginger_man;

	public static void Init () {

		prototype_snow_flake_1 = (GameObject) Resources.Load ("Prefabs/Snow Flake 1");
		prototype_snow_flake_2 = (GameObject) Resources.Load ("Prefabs/Snow Flake 2");
		prototype_snow_flake_3 = (GameObject) Resources.Load ("Prefabs/Snow Flake 3");
		prototype_snow_flake_4 = (GameObject) Resources.Load ("Prefabs/Snow Flake 4");
		prototype_snow_flake_5 = (GameObject) Resources.Load ("Prefabs/Snow Flake 5");
		prototype_ginger_man = (GameObject)Resources.Load ("Prefabs/Ginger Man");

		snow_flakes_1.Clear ();
		snow_flakes_2.Clear ();
		snow_flakes_3.Clear ();
		snow_flakes_4.Clear ();
		snow_flakes_5.Clear ();
		ginger_mans.Clear ();

		for (int i = 0; i < 10; i ++) {

			GameObject snow_flake_1 = Instantiate (prototype_snow_flake_1);
			GameObject snow_flake_2 = Instantiate (prototype_snow_flake_2);
			GameObject snow_flake_3 = Instantiate (prototype_snow_flake_3);
			GameObject snow_flake_4 = Instantiate (prototype_snow_flake_4);
			GameObject snow_flake_5 = Instantiate (prototype_snow_flake_5);
			GameObject ginger_man = Instantiate (prototype_ginger_man);

			ResetObject (snow_flake_1);
			ResetObject (snow_flake_2);
			ResetObject (snow_flake_3);
			ResetObject (snow_flake_4);
			ResetObject (snow_flake_5);
			ResetObject (ginger_man);

			snow_flakes_1.Enqueue (snow_flake_1);
			snow_flakes_2.Enqueue (snow_flake_2);
			snow_flakes_3.Enqueue (snow_flake_3);
			snow_flakes_4.Enqueue (snow_flake_4);
			snow_flakes_5.Enqueue (snow_flake_5);
			ginger_mans.Enqueue (ginger_man);
		}
	}

	public static GameObject GetSnowFlake () {

		GameObject snow_flake = null;
		float r = UnityEngine.Random.value;

		if (r < 0.2f) {
			if (snow_flakes_1.Count > 0) {
				snow_flake = snow_flakes_1.Dequeue ();
			} else {
				snow_flake = Instantiate (prototype_snow_flake_1);
				ResetObject (snow_flake);
			}
		} else if (r < 0.4f) {
			if (snow_flakes_2.Count > 0) {
				snow_flake = snow_flakes_2.Dequeue ();
			} else {
				snow_flake = Instantiate (prototype_snow_flake_2);
				ResetObject (snow_flake);
			}
		} else if (r < 0.6f) {
			if (snow_flakes_3.Count > 0) {
				snow_flake = snow_flakes_3.Dequeue ();
			} else {
				snow_flake = Instantiate (prototype_snow_flake_3);
				ResetObject (snow_flake);
			}
		} else if (r < 0.8f) {
			if (snow_flakes_4.Count > 0) {
				snow_flake = snow_flakes_4.Dequeue ();
			} else {
				snow_flake = Instantiate (prototype_snow_flake_4);
				ResetObject (snow_flake);
			}
		} else {
			if (snow_flakes_5.Count > 0) {
				snow_flake = snow_flakes_5.Dequeue ();
			} else {
				snow_flake = Instantiate (prototype_snow_flake_5);
				ResetObject (snow_flake);
			}
		}

		ActivateObject (snow_flake);

		return snow_flake;
	}

	public static GameObject GetGingerMan () {
	
		GameObject ginger_man = null;

		if (ginger_mans.Count > 0) {
			ginger_man = ginger_mans.Dequeue ();
		} else {
			ginger_man = Instantiate (prototype_ginger_man);
			ResetObject (ginger_man);
		}

		ActivateObject (ginger_man);

		return ginger_man;
	}

	private static void ResetObject (GameObject mObject) {

		mObject.transform.position.Set (-100, 100, 0);
		mObject.transform.rotation.eulerAngles.Set (0, 0, 0);
		mObject.GetComponent <Rigidbody2D> ().angularVelocity = 0;
		mObject.GetComponent <Rigidbody2D> ().velocity = Vector2.zero;
		mObject.SetActive (false);
	}

	public static void RecycleSnowFlake (GameObject snow_flake) {

		int type = snow_flake.GetComponent <SnowFlakeController> ().type;

		ResetObject (snow_flake);

		if (type == 1) {
			snow_flakes_1.Enqueue (snow_flake);
		} else if (type == 2) {
			snow_flakes_2.Enqueue (snow_flake);
		} else if (type == 3) {
			snow_flakes_3.Enqueue (snow_flake);
		} else if (type == 4) {
			snow_flakes_4.Enqueue (snow_flake);
		} else if (type == 5) {
			snow_flakes_5.Enqueue (snow_flake);
		}
	}

	public static void RecycleGingerMan (GameObject ginger_man) {
	
		ResetObject (ginger_man);
		ginger_mans.Enqueue (ginger_man);
	}

	private static void ActivateObject (GameObject mObject) {

		mObject.SetActive (true);
	}
}