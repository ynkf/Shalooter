using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class PlayerMovementCalculationsTest
    {
        [Test]
        public void CalculateAcceleration()
        {
            var playerVelocity = new Vector3(0f, -0.2f, 0.6f);
            var wishDir = new Vector3(0f, 0f, 1f);
            float wishSpeed = 7f;
            float accel = 14f;

            Vector3 result = PlayerMovementCalculations.CalculateAcceleration(playerVelocity, wishDir, wishSpeed, accel);
            var expected = new Vector3(0f, -0.2f, 7f);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CalculateFriction_Air()
        {
            var playerVelocity = new Vector3(0f, -0.2f, 7f);
            float runDeacceleration = 10f;
            float groundFriction = 6f;
            float baseFriction = 1f;
            bool isGrounded = false;

            Vector3 result = PlayerMovementCalculations.CalculateFricition(
                playerVelocity,
                runDeacceleration,
                groundFriction,
                isGrounded,
                baseFriction
            );

            var expected = new Vector3(0f, -0.2f, 7f);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CalculateAirControl()
        {
            var playerVelocity = new Vector3(0f, -0.2f, 0.6f);
            var wishDir = new Vector3(0f, 0f, 1f);
            float airControlPrecision = 0.3f;
            float wishSpeed = 7f;

            Vector3 result = PlayerMovementCalculations.CalculateAirControl(playerVelocity, wishDir, airControlPrecision, wishSpeed, 1f);

            var expected = new Vector3(0f, -0.2f, 0.6f);

            Assert.AreEqual(expected, result);
        }
    }
}
