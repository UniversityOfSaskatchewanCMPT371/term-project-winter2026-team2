using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;
using System.Text.RegularExpressions;
using System;

public class BaseModel : Model
{
    public override void Init()
    {

    }
}

public class ModelTests
{
    private GameObject go;
    private BaseModel model;

    [SetUp]
    public void Setup()
    {
        go = new GameObject();
        model = go.AddComponent<BaseModel>();
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(go);
    }

    // A Test behaves as an ordinary method
    [Test]
    public void Instantiation()
    {
        Assert.NotNull(model, "'model' field cannot be null");

        model.Start();
    }
}