using NUnit.Framework;
using UnityEngine;

namespace BlockBuilderGame.Tests
{
    /// <summary>
    /// Unit tests for the BlockModel class, which implements the IBlockModel interface
    /// </summary>
    public class BlockModelTests
    {
        private GameObject gameObject;
        private BlockModel blockModel;

        [SetUp]
        public void Setup()
        {
            gameObject = new GameObject("TestBlockModel");
            blockModel = gameObject.AddComponent<BlockModel>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(gameObject);
        }

        /// <summary>
        /// Test the initialization of the BlockModel instance
        /// </summary>
        [Test]
        public void TestBlockModelInitialization()
        {
            blockModel.initialization();
            Assert.IsNotNull(blockModel, "Error! BlockModel instance is null after initialization");
            Assert.AreEqual(string.Empty, blockModel.BlockType, "Error! BlockType should be initialized to empty string");
            Assert.AreEqual(Vector3.zero, blockModel.Position, "Error! Position should be initialized to Vector3.zero");
            Assert.AreEqual(Quaternion.identity, blockModel.Rotation, "Error! Rotation should be initialized to Quaternion.identity");
            /// Initialization test passed successfully
            Debug.Log("BlockModel initialized successfully with default values");
        }

        /// <summary>
        /// Test the getter and setter for the BlockType property
        /// </summary>
        [Test] public void GetSetBlockType()
        {
            string testBlockType = "bevel_lq_brick_1x1";
            blockModel.BlockType = testBlockType;
            Assert.AreEqual(testBlockType, blockModel.BlockType, "Error! BlockType getter/setter not working correctly");
            /// BlockType getter/setter test passed successfully
            Debug.Log("BlockType getter/setter working correctly");
        }

        /// <summary>
        /// Test the getter and setter for the Position property
        /// </summary>
        [Test] public void GetSetPosition()
        {
            Vector3 testPosition = new Vector3(1, 2, 3);
            blockModel.Position = testPosition;
            Assert.AreEqual(testPosition, blockModel.Position, "Error! Position getter/setter not working correctly");
            /// Position getter/setter test passed successfully
            Debug.Log("Position getter/setter working correctly");
        }

        /// <summary>
        /// Test the getter and setter for the Rotation property
        /// </summary>
        [Test] public void GetSetRotation()
        {
            Quaternion testRotation = Quaternion.Euler(45, 90, 0);
            blockModel.Rotation = testRotation;
            Assert.AreEqual(testRotation, blockModel.Rotation, "Error! Rotation getter/setter not working correctly");
            /// Rotation getter/setter test passed successfully
            Debug.Log("Rotation getter/setter working correctly");
        }
    }
}
