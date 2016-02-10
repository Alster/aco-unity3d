[System.Serializable]
public class Wiggle
{
    public float speed = 1;
    public float amplitude = 2;
    public int octaves = 4;

    float cueVel = 0;

    public float current { get; private set; }
    float destination;
    int currentTime = 0;

    public void Update()
    {
        // if number of frames played since last change of direction > octaves create a new destination
        if (currentTime >= octaves)
        {
            currentTime = 0;
            destination = UnityEngine.Random.Range(-amplitude, amplitude);
            //print("new Vector Generated: " + destination);
        }

        // smoothly moves the object to the random destination

        currentTime++;
        //transform.localPosition = Vector3.SmoothDamp(transform.localPosition, destination, ref cueVel, speed);
        current = UnityEngine.Mathf.SmoothDamp(current, destination, ref cueVel, speed);
    }
}