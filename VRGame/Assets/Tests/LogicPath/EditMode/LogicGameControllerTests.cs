using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LogicGameControllerTests
{
    GameObject go;
    LogicGameController lgc;

    ILogicGameModel lgm;

    [SetUp] 
    public void SetUp()
    {
        go = new GameObject();
        lgc = go.AddComponent<LogicGameController>();


        
    }
    // A Test behaves as an ordinary method
    [Test]
    public void Init()
    {
        // add logicgame model mock
        lgm = Substitute.For<ILogicGameModel>();
        lgc.ModelMock = lgm;
        lgc.Init();
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator LogicGameControllerWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
