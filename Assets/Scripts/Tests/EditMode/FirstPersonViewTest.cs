using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class FirstPersonViewTest
    {
        [Test]
        public void CalculateCameraXRotation()
        {
            var gameObject = new GameObject();
            var fpv = gameObject.AddComponent<FirstPersonView>();

            var result = fpv.CalculateCameraXRotation(100f, 0f);

            Assert.AreEqual(-90f, result);
        }
    }
}
