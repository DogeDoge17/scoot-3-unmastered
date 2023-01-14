using UnityEngine;
public class OverheadViewAutoDisable : MonoBehaviour { void Start() { if (Application.platform == RuntimePlatform.PSP2) gameObject.SetActive(false); } }