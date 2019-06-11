using UnityEngine;

public class BallPointRotator : MonoBehaviour {
    [SerializeField] private Canvas canvas;
    private float canvasScale;
    private float planeDistance;
    [SerializeField] private Transform pointer;
    [SerializeField] private float scale = 100;

    [SerializeField]  private Vector2 xLimit = default(Vector2);
    [SerializeField] private Vector2 yLimit = default(Vector2);

	private void Start() {
		ScaleChildren();
	}
    public void ScaleChildren() {
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.renderMode = RenderMode.WorldSpace;
        canvasScale = canvas.transform.localScale.z;
        planeDistance = (canvas.transform.position - canvas.worldCamera.transform.position).magnitude;
        for(var i = 0; i < transform.childCount; i++) {
            ScaleObject(transform.GetChild(i));
        }
    }

    private void ScaleObject(Transform targetTransform) {
        ScaleObject(targetTransform, canvas.worldCamera.transform, planeDistance, canvasScale);
        for(var i = 0; i < targetTransform.childCount; i++) {
            ScaleObject(targetTransform.GetChild(i));
        }
    }

    public static void ScaleObject(Transform targetTransform, Transform cameraTransform, float referencePlaneDistance,
                                   float canvasScale) {
        // axial z distance between [me] <-> [camera]
        var dist = targetTransform.position.z - cameraTransform.position.z;
        var pScale = targetTransform.parent.lossyScale;
        // the lossy scale I want to achieve at some point.
        var targetScale = dist / referencePlaneDistance;
        // my resulting local scale - by figuring out how much more/less scale I need based on my parents
        var localScale = targetScale / (pScale.z / canvasScale);
        // the delta between now an my future scale - which is useful to scale all sizes appropriately
        Vector2 scaleDelta = targetTransform.localScale - targetTransform.localScale * localScale;
        // multiply the change - which keeps respect of the original scale modification
        // however: this method then only works once, since we lose the scale context on the next time!
        targetTransform.localScale *= localScale;
        // finally touch the position to move at the accurate pinpoint again.
        var localPosition = targetTransform.localPosition;
        localPosition.Scale(new Vector3(1 - scaleDelta.x, 1 - scaleDelta.y, 1));
        targetTransform.localPosition = localPosition;
    }

    private void Update() {
        var xRot = (pointer.localPosition.y - yLimit.x) / (yLimit.y - yLimit.x) - 0.5f;
        var yRot = (pointer.localPosition.x - xLimit.x) / (xLimit.y - xLimit.x) - 0.5f;
        transform.rotation = Quaternion.Euler(xRot * scale * 2, yRot * scale * 2, 0);
        // transform.localPosition = new Vector3(-yRot * scale * 10, -xRot * scale * 10, transform.localPosition.z);
    }
}
