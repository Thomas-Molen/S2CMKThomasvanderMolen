using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using PlatformerSpeedRunner;
using PlatformerSpeedRunner.Camera;
using PlatformerSpeedRunner.Objects;

namespace PlatformSpeedRunner.Tests
{
    [TestClass]
    public class CameraHelperTests
    {
        CameraHelper camera;

        [TestInitialize]
        public void Setup()
        {
            camera = new CameraHelper();
        }

        [TestMethod]
        public void Camera_transform_will_Be_Default_With_No_Player()
        {
            //Arrange
            Matrix expectedTransform = Matrix.CreateTranslation(0, 0, 0);
            //act
            camera.Follow();
            //Assert
            Assert.AreEqual(expectedTransform, camera.transform);
        }

        [TestMethod]
        public void Camera_transform_will_Be_Zero_With_Null_Player()
        {
            //Arrange
            Player player = null;
            Matrix expectedTransform = new Matrix();
            
            //act
            camera.Follow(player);
            //Assert
            Assert.AreEqual(expectedTransform, camera.transform);
        }

        [TestMethod]
        public void Camera_Can_Follow_Vector2()
        {
            //Arrange
            Vector2 cameraPosition = new Vector2(20, 20);
            Matrix expectedMatrix = Matrix.CreateTranslation(new Vector3(-cameraPosition.X + Program.width/2, -cameraPosition.Y + Program.height/2, 0));

            //act
            camera.Follow(cameraPosition);

            //Assert
            Assert.AreEqual(expectedMatrix, camera.transform);
        }

        [TestMethod]
        public void Camera_Can_Give_Camera_Based_Position()
        {
            //Arrange
            Vector2 HUDPosition = new Vector2(100, 100);
            Vector2 cameraPosition = new Vector2(20, 20);

            //act
            camera.Follow(cameraPosition);
            
            Vector2 cameraBasedHUDPosition = camera.GetCameraBasedPosition(HUDPosition);
            //Assert
            Assert.AreEqual((cameraPosition + HUDPosition) - new Vector2(Program.width/2, Program.height/2), cameraBasedHUDPosition);
        }
    }
}
