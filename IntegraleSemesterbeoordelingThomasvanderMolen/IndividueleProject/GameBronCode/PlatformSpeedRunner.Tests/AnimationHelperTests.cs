using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlatformerSpeedRunner.Helper;
using PlatformerSpeedRunner.Objects;

namespace PlatformSpeedRunner.Tests
{
    [TestClass]
    public class AnimationHelperTests
    {
        AnimationHelper animationHelper;
        TextureList textureList;

        [TestInitialize]
        public void Setup()
        {
            animationHelper = new AnimationHelper();
            textureList = new TextureList();
        }

        [TestMethod]
        public void Helper_Can_Generate_Player_Animations_With_Correct_Animation_Prefix()
        {
            //Arrange
            animationHelper.CreateAnimation(textureList.playerRunningRight);
            //act
            AnimationObject runningRightAnimation = animationHelper.GetPlayerAnimation(PlatformerSpeedRunner.Enum.Animations.RunningRight);
            //Assert
            Assert.AreEqual(textureList.playerRunningRight, runningRightAnimation.animationPrefix);
        }

        [TestMethod]
        public void Helper_Can_Generate_Player_Animations_With_Correct_Animation_Length()
        {
            //Arrange
            animationHelper.CreateAnimation(textureList.playerRunningRight, 10);
            //act
            AnimationObject runningRightAnimation = animationHelper.GetPlayerAnimation(PlatformerSpeedRunner.Enum.Animations.RunningRight);
            //Assert
            Assert.AreEqual(10, runningRightAnimation.animationLength);
        }

        [TestMethod]
        public void Helper_Can_Generate_Player_Animations_With_Correct_Animation_Loop_Duration()
        {
            //Arrange
            animationHelper.CreateAnimation(textureList.playerRunningRight, animationLoopDuration: 5);
            //act
            AnimationObject runningRightAnimation = animationHelper.GetPlayerAnimation(PlatformerSpeedRunner.Enum.Animations.RunningRight);
            //Assert
            Assert.AreEqual(5, runningRightAnimation.animationLoopDuration);
        }

        [TestMethod]
        public void Helper_Returns_Default_Prefix_If_Length_Is_One()
        {
            //Arrange
            animationHelper.CreateAnimation(textureList.playerIdle);
            //act
            string defaultAnimation = animationHelper.GetAnimation(animationHelper.GetPlayerAnimation(PlatformerSpeedRunner.Enum.Animations.Idle));
            //Assert
            Assert.AreEqual(textureList.playerIdle, defaultAnimation);
        }

        [TestMethod]
        public void Helper_Returns_Correct_Animation_In_Order()
        {
            //Arrange
            animationHelper.CreateAnimation(textureList.playerRunningLeft, 5);
            //act
            animationHelper.GetAnimation(animationHelper.GetPlayerAnimation(PlatformerSpeedRunner.Enum.Animations.RunningLeft));
            string animation = animationHelper.GetAnimation(animationHelper.GetPlayerAnimation(PlatformerSpeedRunner.Enum.Animations.RunningLeft));
            //Assert
            Assert.AreEqual(textureList.playerRunningLeft + "3", animation);
        }

        [TestMethod]
        public void Helper_Duplicates_Frames_Based_On_Loop_Duration()
        {
            //Arrange
            animationHelper.CreateAnimation(textureList.playerFallingLeft, 5, 10);
            //act
            string animation = animationHelper.GetAnimation(animationHelper.GetPlayerAnimation(PlatformerSpeedRunner.Enum.Animations.FallingLeft));
            animation = animationHelper.GetAnimation(animationHelper.GetPlayerAnimation(PlatformerSpeedRunner.Enum.Animations.FallingLeft));
            //Assert
            Assert.AreEqual(textureList.playerFallingLeft + "1", animation);
        }

        [TestMethod]
        public void Helper_Loops_Back_To_First_Frame()
        {
            //Arrange
            animationHelper.CreateAnimation(textureList.playerFallingRight, 3);
            //act
            animationHelper.GetAnimation(animationHelper.GetPlayerAnimation(PlatformerSpeedRunner.Enum.Animations.FallingRight));
            animationHelper.GetAnimation(animationHelper.GetPlayerAnimation(PlatformerSpeedRunner.Enum.Animations.FallingRight));
            string animation = animationHelper.GetAnimation(animationHelper.GetPlayerAnimation(PlatformerSpeedRunner.Enum.Animations.FallingRight));
            //Assert
            Assert.AreEqual(textureList.playerFallingRight + "1", animation);
        }
    }
}
