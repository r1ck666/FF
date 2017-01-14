using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
	public float distance = 5.0f;
	[SerializeField] private float horizontalAngle = 0;
	[SerializeField] private float verticalAngle = 70;
	[SerializeField] Transform lookTarget;
	public Transform LookTarget {
		get { return lookTarget; }
		set { lookTarget = value; }
	}
	public Vector3 offset = Vector3.zero;

	// Update is called once per frame
	void LateUpdate () {
		// カメラを位置と回転を更新する.
		if (lookTarget != null) {
			Vector3 lookPosition = lookTarget.position + offset;
			// 注視対象からの相対位置を求める.
			Vector3 relativePos = Quaternion.Euler(verticalAngle,horizontalAngle,0) *  new Vector3(0,0,-distance);

			// 注視対象の位置にオフセット加算した位置に移動させる.
			transform.position = lookPosition + relativePos ;

			// 注視対象を注視させる.
			transform.LookAt(lookPosition);

		}

	}
}
