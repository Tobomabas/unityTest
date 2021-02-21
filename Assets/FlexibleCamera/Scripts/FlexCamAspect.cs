using UnityEngine;

namespace FlexibleCamera
{
	public class FlexCamAspect : MonoBehaviour
	{
		public enum RatioMode { FixedShortest, FixedVertical, FixedHorizontal }

		[Tooltip ("Does not need to be set! It will automatically try to use the FlexCam script on the same gameObject. Otherwise it will reference the static accessor.")] public FlexCam Cam;
		[Tooltip ("FixedVertical: Standard Unity behavior. Changes the bahavior to either keep the width or height constant, or keep the shortest constant.")] public RatioMode RatioType = RatioMode.FixedShortest;
		
		void Awake ()
		{
			if (Cam != null) return;
			Cam = GetComponent<FlexCam> ();
			if (Cam == null) Cam = FlexCam.Main;
			if (Cam == null) enabled = false;
		}

		void OnDisable ()
		{
			Cam.AspectModifier = 1;
		}

		void Update ()
		{
			if (RatioType == RatioMode.FixedShortest) Cam.AspectModifier = Mathf.Max (1 / Cam.Cam.aspect, 1);
			else if (RatioType == RatioMode.FixedHorizontal) Cam.AspectModifier = 1 / Cam.Cam.aspect;
			else Cam.AspectModifier = 1;
		}
	}
}
