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

    [Test]
    public void HandleHover_Valid_occupied()
    {
        // hovered panel mock
        IPanel pMock = Substitute.For<IPanel>();
        // add logicgame model mock
        lgm = Substitute.For<ILogicGameModel>();

        // logic game mock returns panel mock
        lgm.GetPanel(0,0).Returns(pMock);

        LogAssert.Expect(LogType.Log, "But the hovered panel is occupied!");
        lgc.HandleHover(0,0);

    }
    

}
