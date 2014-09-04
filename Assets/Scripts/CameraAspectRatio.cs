using UnityEngine;
using System.Collections;

// from http://gamedesigntheory.blogspot.ie/2010/09/controlling-aspect-ratio-in-unity.html
public class CameraAspectRatio : MonoBehaviour {
	
	new Camera camera;
	
	public float CameraWidth = 920f;
	public float CameraHeight = 920f;
	
	void Start() {
		camera = GetComponent<Camera>();
		
		float targetAspect = CameraWidth / CameraHeight;
		float windowAspect = (float)Screen.width / (float)Screen.height;
		float scaleHeight = windowAspect / targetAspect;
		
		// if scaled height is less than current height, add letterbox
		if (scaleHeight < 1.0f) {  
			Rect rect = camera.rect;
			
			rect.width = 1.0f;
			rect.height = scaleHeight;
			rect.x = 0;
			rect.y = (1.0f - scaleHeight) / 2.0f;
			
			camera.rect = rect;
		} else { // add pillarbox
			float scalewidth = 1.0f / scaleHeight;
			
			Rect rect = camera.rect;
			
			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;
			
			camera.rect = rect;
		}
	}
}