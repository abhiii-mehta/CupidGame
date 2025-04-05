using UnityEngine;

public class BowBlendController : MonoBehaviour
{
    public SkinnedMeshRenderer bowMesh; // Drag your bow's SkinnedMeshRenderer here
    public int blendShapeIndex = 0; // Index of your blendshape (usually 0 if there's only one)
    public float blendSpeed = 5f;

    private float targetWeight = 0f;

    void Update()
    {
       
        if (Input.GetMouseButton(0))
        {
            targetWeight = 100f;
        }
       
        if (Input.GetMouseButtonUp(0))
        {
            targetWeight = 0f;
        }

        float currentWeight = bowMesh.GetBlendShapeWeight(blendShapeIndex);
        float newWeight = Mathf.Lerp(currentWeight, targetWeight, Time.deltaTime * blendSpeed);
        bowMesh.SetBlendShapeWeight(blendShapeIndex, newWeight);
    }
}
