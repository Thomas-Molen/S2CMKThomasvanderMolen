using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlatformerSpeedRunner.Helper;
using PlatformerSpeedRunner.Objects;

namespace PlatformSpeedRunner.Tests
{
    [TestClass]
    public class AnimationHelperTests
    {
        AnimationHelper animationHelper;

        [TestInitialize]
        public void Setup()
        {
            animationHelper = new AnimationHelper();
        }

        [TestMethod]
        public void Helper_Can_Generate_Animation_Objects()
        {
            //Arrange
            string prefix = "WalkingLeft";
            int length = 5;
            int duration = 15;
            //act
            var animation = animationHelper.CreateAnimation(prefix, length, duration);
            
            //Assert
            Assert.IsInstanceOfType(animation, new AnimationObject(prefix, length, duration).GetType());
        }

        [TestMethod]
        public void Helper_Can_Return_Correct_Default_Animation()
        {
            //Arrange
            string prefix = "WalkingLeft";
            int length = 1;
            int duration = 5;
            //act
            AnimationObject animation = animationHelper.CreateAnimation(prefix, length, duration);
            string returnedAnimation = animationHelper.GetAnimation(animation);

            //Assert
            Assert.AreEqual(prefix, returnedAnimation);
        }

        [TestMethod]
        public void Animation_Returns_Correct_Animation_In_Order()
        {
            //Arrange
            string prefix = "WalkingLeft";
            int length = 10;
            int duration = 1;
            string returnedAnimation = prefix;
            //act
            AnimationObject animation = animationHelper.CreateAnimation(prefix, length, duration);
            animationHelper.GetAnimation(animation);
            returnedAnimation = animationHelper.GetAnimation(animation);
            //Assert
            Assert.AreEqual(prefix + 3, returnedAnimation);
        }

        [TestMethod]
        public void Animation_Will_Duplicate_Frames_For_Longer_Duration()
        {
            //Arrange
            string prefix = "WalkingLeft";
            int length = 10;
            int duration = 50;
            string returnedAnimation = prefix;
            //act
            AnimationObject animation = animationHelper.CreateAnimation(prefix, length, duration);
            animationHelper.GetAnimation(animation);
            returnedAnimation = animationHelper.GetAnimation(animation);
            //Assert
            Assert.AreEqual(prefix + 1, returnedAnimation);
        }

        [TestMethod]
        public void Animation_Will_Loop_Back_To_Start()
        {
            //Arrange
            string prefix = "WalkingLeft";
            int length = 3;
            int duration = 1;
            string returnedAnimation = prefix;
            //act
            AnimationObject animation = animationHelper.CreateAnimation(prefix, length, duration);
            animationHelper.GetAnimation(animation);
            animationHelper.GetAnimation(animation);
            returnedAnimation = animationHelper.GetAnimation(animation);
            //Assert
            Assert.AreEqual(prefix + 1, returnedAnimation);
        }
    }
}
