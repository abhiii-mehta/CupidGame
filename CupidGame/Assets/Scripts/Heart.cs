using UnityEngine;

public class Heart : MonoBehaviour
{
    public enum HeartColor
    {
        Red,
        Blue,
        Purple
    }

    public enum MaskType
    {
        BothEyesOpen,
        OneEyeOpen,
        NoEyesOpen
    }

    public HeartColor heartColor;
    public MaskType maskType;
}
