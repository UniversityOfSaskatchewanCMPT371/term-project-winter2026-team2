using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LogicGameControllerTests
{
    GameObject go;

    GameObject confetti;

    LogicGameController lgc;

    ILogicGameModel lgm;

    [SetUp] 
    public void SetUp()
    {
        go = new GameObject();
        lgc = go.AddComponent<LogicGameController>();
    }

    // helper func
    public void valid_init()
    {
        // add logicgame model mock
        lgm = Substitute.For<ILogicGameModel>();

        confetti = new GameObject();
        confetti.AddComponent<ParticleSystem>();
        confetti.transform.SetParent(go.transform);

        lgc.ModelMock = lgm;
        lgc.Init();
    }
    // A Test behaves as an ordinary method
    [Test]
    public void Init()
    {
        // add logicgame model mock
        lgm = Substitute.For<ILogicGameModel>();

        confetti = new GameObject();
        confetti.AddComponent<ParticleSystem>();
        confetti.transform.SetParent(go.transform);

        lgc.ModelMock = lgm;
        lgc.Init();

    }

    [Test]
    public void HandleHover_Valid_occupied()
    {
        // hovered panel mock
        IPanel pMock = Substitute.For<IPanel>();
        pMock.IsOccupied().Returns(true);
        pMock.GridX = 0;
        pMock.GridY = 1;

        IPanel curPath = Substitute.For<IPanel>();
        curPath.GridX = 0;
        curPath.GridY = 0;

        confetti = new GameObject();
        confetti.AddComponent<ParticleSystem>();
        confetti.transform.SetParent(go.transform);
        // add logicgame model mock
        lgm = Substitute.For<ILogicGameModel>();

        // logic game mock returns panel mock
        lgm.GetPanel(0,1).Returns(pMock);

        lgc.ModelMock = lgm;

        lgc.Init();

        // add this panel to current path
        lgc.currentPathRight.Push(curPath);

        LogAssert.Expect(LogType.Log, "But the hovered panel is occupied!");
        lgc.isDraggingRight = true;
        lgc.HandleHover(0,1);

    }
    

    [Test]
    public void HandleHover_Valid_left()
    {
        // hovered panel mock
        IPanel pMock = Substitute.For<IPanel>();
        pMock.IsOccupied().Returns(false);
        pMock.GridX = 0;
        pMock.GridY = 0;

        IPanel curPath = Substitute.For<IPanel>();
        curPath.GridX = 1;
        curPath.GridY = 0;
        //non-null

        curPath.LeftNeighbor.Returns(pMock);
        pMock.RightNeighbor.Returns(curPath);

        confetti = new GameObject();
        confetti.AddComponent<ParticleSystem>();
        confetti.transform.SetParent(go.transform);
        // add logicgame model mock
        lgm = Substitute.For<ILogicGameModel>();

        // logic game mock returns panel mock
        lgm.GetPanel(0,0).Returns(pMock);

        lgc.ModelMock = lgm;

        lgc.Init();

        // add this panel to current path
        lgc.currentPathRight.Push(curPath);

        LogAssert.Expect(LogType.Log, "Moving left!");
        lgc.isDraggingRight = true;
        lgc.HandleHover(0,0);
    }

    [Test]
    public void HandleHover_Valid_right()
    {
        // hovered panel mock
        IPanel pMock = Substitute.For<IPanel>();
        pMock.IsOccupied().Returns(false);
        pMock.GridX = 1;
        pMock.GridY = 0;

        IPanel curPath = Substitute.For<IPanel>();
        curPath.GridX = 0;
        curPath.GridY = 0;
        //non-null

        curPath.RightNeighbor.Returns(pMock);
        pMock.LeftNeighbor.Returns(curPath);

        confetti = new GameObject();
        confetti.AddComponent<ParticleSystem>();
        confetti.transform.SetParent(go.transform);
        // add logicgame model mock
        lgm = Substitute.For<ILogicGameModel>();

        // logic game mock returns panel mock
        lgm.GetPanel(1,0).Returns(pMock);

        lgc.ModelMock = lgm;

        lgc.Init();

        // add this panel to current path
        lgc.currentPathRight.Push(curPath);

        LogAssert.Expect(LogType.Log, "Moving right!");
        lgc.isDraggingRight = true;
        lgc.HandleHover(1,0);
    }

    [Test]
    public void HandleHover_Valid_up()
    {
        // hovered panel mock
        IPanel pMock = Substitute.For<IPanel>();
        pMock.IsOccupied().Returns(false);
        pMock.GridX = 0;
        pMock.GridY = 0;

        IPanel curPath = Substitute.For<IPanel>();
        curPath.GridX = 0;
        curPath.GridY = 1;
        //non-null

        curPath.TopNeighbor.Returns(pMock);
        pMock.DownNeighbor.Returns(curPath);

        confetti = new GameObject();
        confetti.AddComponent<ParticleSystem>();
        confetti.transform.SetParent(go.transform);
        // add logicgame model mock
        lgm = Substitute.For<ILogicGameModel>();

        // logic game mock returns panel mock
        lgm.GetPanel(1,0).Returns(pMock);

        lgc.ModelMock = lgm;

        lgc.Init();

        // add this panel to current path
        lgc.currentPathRight.Push(curPath);

        LogAssert.Expect(LogType.Log, "Moving up!");
        lgc.isDraggingRight = true;
        lgc.HandleHover(1,0);
    }

    [Test]
    public void HandleHover_Valid_down()
    {
        // hovered panel mock
        IPanel pMock = Substitute.For<IPanel>();
        pMock.IsOccupied().Returns(false);
        pMock.GridX = 0;
        pMock.GridY = 1;

        IPanel curPath = Substitute.For<IPanel>();
        curPath.GridX = 0;
        curPath.GridY = 0;
        //non-null

        curPath.DownNeighbor.Returns(pMock);
        pMock.TopNeighbor.Returns(curPath);

        confetti = new GameObject();
        confetti.AddComponent<ParticleSystem>();
        confetti.transform.SetParent(go.transform);
        // add logicgame model mock
        lgm = Substitute.For<ILogicGameModel>();

        // logic game mock returns panel mock
        lgm.GetPanel(0,1).Returns(pMock);

        lgc.ModelMock = lgm;

        lgc.Init();

        // add this panel to current path
        lgc.currentPathRight.Push(curPath);

        LogAssert.Expect(LogType.Log, "Moving down!");
        lgc.isDraggingRight = true;
        lgc.HandleHover(0,1);
    }

    [Test]
    public void clearPath()
    {

        IPanel curPath = Substitute.For<IPanel>();
        curPath.GridX = 0;
        curPath.GridY = 0;

        confetti = new GameObject();
        confetti.AddComponent<ParticleSystem>();
        confetti.transform.SetParent(go.transform);
        // add logicgame model mock
        lgm = Substitute.For<ILogicGameModel>();

        // logic game mock returns panel mock

        lgc.ModelMock = lgm;

        lgc.Init();

        // add this panel to current path
        lgc.currentPathRight.Push(curPath);

        lgc.ClearPathRight();

        Assert.IsTrue(lgc.currentPathRight.Count == 0);
    }

    [Test]
    public void HandleUnhover()
    {
        IPanel pMock = Substitute.For<IPanel>();
        pMock.GridX = 0;
        pMock.GridY = 0;

        confetti = new GameObject();
        confetti.AddComponent<ParticleSystem>();
        confetti.transform.SetParent(go.transform);
        // add logicgame model mock
        lgm = Substitute.For<ILogicGameModel>();

        // logic game mock returns panel mock

        lgc.ModelMock = lgm;

        lgc.Init();

        // add this panel to current path
        lgc.currentPathRight.Push(pMock);

        lgc.ClearPathRight();

        Assert.IsTrue(lgc.currentPathRight.Count == 0);
    }
}
