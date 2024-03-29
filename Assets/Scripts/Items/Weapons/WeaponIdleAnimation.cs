using UnityEngine;

public class WeaponIdleAnimation : MonoBehaviour
{
    public bool isFloating = true;
    [SerializeField] float floatSpeed = 0.005f;
    [SerializeField] float floatRate = 0.5f;

    private bool goingUp = true;
    private float floatTimer;

    // Update is called once per frame
    void Update()
    {
        if (isFloating)
        {
            floatTimer += Time.deltaTime;

            Vector3 moveDirection = new Vector3(0.0f, floatSpeed, 0);

            transform.Translate(moveDirection);

            if (goingUp && floatTimer >= floatRate)
            {
                goingUp = false;
                floatTimer = 0;
                floatSpeed = -floatSpeed;
            }

            else if (!goingUp && floatTimer >= floatRate)
            {
                goingUp = true;
                floatTimer = 0;
                floatSpeed = +floatSpeed;
            }
        }
    }
}
