using System.Collections;
using Frictionless;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class RoutingTest
    {
        [UnityTest]
        public IEnumerator TestMessageRouting()
        {
            var loadScene = SceneManager.LoadSceneAsync("Tests");

            while (loadScene.isDone == false)
            {
                yield return null;
            }

            const float epsilon = 0.1f;

            Ball[] balls = GameObject.FindObjectsOfType<Ball>();

            Assert.IsFalse(balls == null || balls.Length == 0,
                "Failed to find objects to test - the scene isn't setup correctly for this test.");

            Vector3[] startPositions = new Vector3[balls.Length];

            for (int i = 0; i < balls.Length; i++)
            {
                startPositions[i] = balls[i].transform.position;
            }

            for (int i = 0; i < balls.Length; i++)
            {
                Assert.IsFalse((startPositions[i] - balls[i].transform.position).magnitude > epsilon,
                    "Balls are prematurely in motion - this should not have happened until a message was routed!");
            }

            ServiceFactory.Instance.Resolve<MessageRouter>().RaiseMessage(new DropMessage() {Force = 500.0f});

            yield return new WaitForSeconds(2.0f);

            for (int i = 0; i < balls.Length; i++)
            {
                Assert.IsFalse((startPositions[i] - balls[i].transform.position).magnitude <= epsilon,
                    "Failed to verify that positions have changed!");
            }
        }
    }
}