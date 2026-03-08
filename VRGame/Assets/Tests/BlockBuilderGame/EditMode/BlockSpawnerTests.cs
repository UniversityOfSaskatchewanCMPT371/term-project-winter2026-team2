using NUnit.Framework;
using UnityEngine;

namespace BlockBuilderGame.Tests
{
    /// <summary>
    /// Unit tests for the BlockSpawnerModel class
    /// </summary>
    public class BlockSpawnerTests
    {
        private GameObject gameObject;
        private BlockSpawnerModel model;

        /// <summary>
        /// Setup method to initialize a new BlockSpawnerModel before each test
        /// </summary>
        [SetUp]
        public void Setup()
        {
            gameObject = new GameObject("TestBlockSpawnerModel");
            model = gameObject.AddComponent<BlockSpawnerModel>();
        }

        /// <summary>
        /// Test getter and setter for BrickPrefabs property
        /// </summary>
        [Test]
        public void GetSetBrickPrefabs()
        {
            GameObject[] testPrefabs = new GameObject[4];
            for (int i = 0; i < 4; i++)
            {
                testPrefabs[i] = new GameObject("TestBrickPrefab" + i);
            }

            model.BrickPrefabs = testPrefabs;
            Assert.AreEqual(testPrefabs, model.BrickPrefabs, "Error! BrickPrefabs getter/setter not working correctly");
            Assert.AreEqual(4, model.BrickPrefabs.Length, "Error! BrickPrefabs should have 4 elements");

        }

        /// <summary>
        /// Test that BrickPrefabs validates array length
        /// </summary>
        [Test]
        public void BrickPrefabsGetSetArrayLength()
        {
            GameObject[] invalidPrefabs = new GameObject[3];
            
            Assert.Throws<UnityEngine.Assertions.AssertionException>(() =>
            {
                model.BrickPrefabs = invalidPrefabs;
            }, "Error! BrickPrefabs should throw assertion when array length is not 4");
        }

        /// <summary>
        /// Test that BrickPrefabs validates null input
        /// </summary>
        [Test]
        public void BrickPrefabsNullCheck()
        {
            Assert.Throws<UnityEngine.Assertions.AssertionException>(() =>
            {
                model.BrickPrefabs = null;
            }, "Error! BrickPrefabs should throw assertion when set to null");
        }

        /// <summary>
        /// Test for CurrentBrickIndex property getter and setter
        /// </summary>
        [Test]
        public void GetSetCurrentBrickIndex()
        {
            int testIndex = 2;
            model.CurrentBrickIndex = testIndex;
            Assert.AreEqual(testIndex, model.CurrentBrickIndex, "Error! CurrentBrickIndex getter/setter not working correctly");
        }

        /// <summary>
        /// Test non-negative for CurrentBrickIndex
        /// </summary>
        [Test]
        public void CurrentBrickIndex_NonNegative()
        {
            Assert.Throws<UnityEngine.Assertions.AssertionException>(() =>
            {
                model.CurrentBrickIndex = -1;
            }, "Error! CurrentBrickIndex should throw assertion when set to negative value");
        }

        /// <summary>
        /// Test for SpawnArea property getter and setter
        /// </summary>
        [Test]
        public void GetSetSpawnArea()
        {
            GameObject spawnAreaObject = new GameObject("TestSpawnArea");
            Transform testSpawnArea = spawnAreaObject.transform;
            
            model.SpawnArea = testSpawnArea;
            Assert.AreEqual(testSpawnArea, model.SpawnArea, "Error! SpawnArea getter/setter not working correctly");

            Object.DestroyImmediate(spawnAreaObject);
        }

        /// <summary>
        /// Test that SpawnArea validates null input
        /// </summary>
        [Test]
        public void SpawnAreaNullCheck()
        {
            Assert.Throws<UnityEngine.Assertions.AssertionException>(() =>
            {
                model.SpawnArea = null;
            }, "Error! SpawnArea should throw assertion when set to null");
        }

        /// <summary>
        /// Test getter and setter for SpawnHeight property
        /// </summary>
        [Test]
        public void GetSetSpawnHeight()
        {
            float testHeight = 2.5f;
            model.SpawnHeight = testHeight;
            Assert.AreEqual(testHeight, model.SpawnHeight, "Error! SpawnHeight getter/setter not working correctly");
        }

        /// <summary>
        /// Test that SpawnHeight accepts negative values
        /// </summary>
        [Test]
        public void SpawnHeight_NegativeValues()
        {
            float negativeHeight = -1.5f;
            model.SpawnHeight = negativeHeight;
            Assert.AreEqual(negativeHeight, model.SpawnHeight, "Error! SpawnHeight should accept negative values");
        }

        /// <summary>
        /// Test that BrickScale property can be set and retrieved correctly
        /// </summary>
        [Test]
        public void GetSetBrickScale()
        {
            float testScale = 3.0f;
            model.BrickScale = testScale;
            Assert.AreEqual(testScale, model.BrickScale, "Error! BrickScale getter/setter not working correctly");
        }

        /// <summary>
        /// Test that BrickScale validates positive values
        /// </summary>
        [Test]
        public void BrickScale_Positive()
        {
            Assert.Throws<UnityEngine.Assertions.AssertionException>(() =>
            {
                model.BrickScale = 0f;
            }, "Error! BrickScale should throw assertion when set to zero");

            Assert.Throws<UnityEngine.Assertions.AssertionException>(() =>
            {
                model.BrickScale = -1f;
            }, "Error! BrickScale should throw assertion when set to negative value");
        }
    }
}
