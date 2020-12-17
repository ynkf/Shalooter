using UnityEngine;

public static class PlayerMovementCalculations
{
    public static Vector3 CalculateAcceleration(Vector3 playerVelocity, Vector3 wishDir, float wishSpeed, float accel)
    {
        float currentSpeed = Vector3.Dot(playerVelocity, wishDir);
        float addSpeed = wishSpeed - currentSpeed;

        if (addSpeed >= 0)
        {
            float accelSpeed = accel * Time.deltaTime * wishSpeed;

            if (accelSpeed > addSpeed)
            {
                accelSpeed = addSpeed;
            }

            playerVelocity.x += accelSpeed * wishDir.x;
            playerVelocity.z += accelSpeed * wishDir.z;
        }

        return playerVelocity;
    }

    public static Vector3 CalculateFricition(Vector3 playerVelocity, float runDeacceleration, float groundFriction, bool isGrounded, float baseFriction)
    {
        Vector3 velocity = playerVelocity;
        velocity.y = 0.0f;

        float speed = velocity.magnitude;
        float drop = 0.0f;

        if (isGrounded)
        {
            float control = speed < runDeacceleration ? runDeacceleration : speed;
            drop = control * groundFriction * Time.deltaTime * baseFriction;
        }

        float newSpeed = speed - drop;

        if (newSpeed < 0)
        {
            newSpeed = 0;
        }

        if (speed > 0)
        {
            newSpeed /= speed;
        }

        playerVelocity.x *= newSpeed;
        playerVelocity.z *= newSpeed;

        return playerVelocity;
    }

    public static Vector3 CalculateAirControl(Vector3 playerVelocity, Vector3 wishDir, float airControlPrecision, float wishSpeed, float movementCommand)
    {
        float zSpeed;
        float Speed;
        float dot;
        float multiplier;

        if (Mathf.Abs(movementCommand) < 0.001 || Mathf.Abs(wishSpeed) < 0.001)
        {
            return playerVelocity;
        }

        zSpeed = playerVelocity.y;
        playerVelocity.y = 0;

        Speed = playerVelocity.magnitude;
        playerVelocity.Normalize();

        dot = Vector3.Dot(playerVelocity, wishDir);
        multiplier = 32;
        multiplier *= airControlPrecision * dot * dot * Time.deltaTime;

        if (dot > 0)
        {
            playerVelocity.x = playerVelocity.x * Speed + wishDir.x * multiplier;
            playerVelocity.y = playerVelocity.y * Speed + wishDir.y * multiplier;
            playerVelocity.z = playerVelocity.z * Speed + wishDir.z * multiplier;

            playerVelocity.Normalize();
        }

        playerVelocity.x *= Speed;
        playerVelocity.y = zSpeed;
        playerVelocity.z *= Speed;

        return playerVelocity;
    }
}
