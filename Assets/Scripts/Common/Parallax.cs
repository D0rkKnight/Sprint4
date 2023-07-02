using UnityEngine;

[System.Serializable]
public struct ParallaxLayer
{
    public Transform background;
    public float parallaxScale;
}

public class Parallax : MonoBehaviour
{
    public ParallaxLayer[] layers;
    public float smoothing = 1f;

    private Transform cameraTransform;
    private Vector3 previousCameraPosition;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
    }

    private void Update()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            float parallax = (previousCameraPosition.x - cameraTransform.position.x) * layers[i].parallaxScale;
            float backgroundTargetPosX = layers[i].background.position.x + parallax;
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, layers[i].background.position.y, layers[i].background.position.z);
            layers[i].background.position = Vector3.Lerp(layers[i].background.position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        previousCameraPosition = cameraTransform.position;
    }
}
