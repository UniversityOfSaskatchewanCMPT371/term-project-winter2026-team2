using System;
using NUnit.Framework;
using UnityEngine;

public class EMModelTemplateEditModeTest
{
    [Test]
    public void Test01_Instantiate()
    {
        GameObject go = new GameObject();
        ModelTemplate model = go.AddComponent<ModelTemplate>();
    }

    [Test]
    public void Test02_GetExample()
    {
        GameObject go = new GameObject();
        ModelTemplate model = go.AddComponent<ModelTemplate>();

        // Test to see if GetExample() will return an int value
        Assert.NotNull(model.GetExample(), "Expected an int value to be returned, but got null");
    }

    [Test]
    public void Test03_SetExample()
    {
        GameObject go = new GameObject();
        ModelTemplate model = go.AddComponent<ModelTemplate>();

        int Expected;
        int Input;
        int Result;

        // Test to see if SetExample() will set Example to specified value
        Expected = 10;
        model.SetExample(Expected);
        Result = model.GetExample();
        Assert.AreEqual(Result, Expected, $"Expected value to be {Expected}, but got {Result}");
        
        // Test to see if SetExample() will thrown an exception on a negative parameter
        Input = -1;
        Assert.Throws<ArgumentOutOfRangeException>(() => model.SetExample(Input), "Expected exception to be thrown, but none was thrown on a negative value");
    }

    [Test]
    public void Test04_IncrementExample()
    {
        GameObject go = new GameObject();
        ModelTemplate model = go.AddComponent<ModelTemplate>();

        int Expected;
        int Result;

        // Test to see if IncrementExample() will increment Example by 1
        Expected = 1;
        model.IncrementExample();
        Result = model.GetExample();
        Assert.AreEqual(Result, Expected, $"Expected value to be {Expected}, but got {Result}");

        // Test to see if IncrementExample() will throw an error when Example is set to int.MaxValue()
        model.SetExample(int.MaxValue);
        Assert.Throws<InvalidOperationException>(() => model.IncrementExample(), "Expected exception to be thrown, but none was thrown on attempt to increment past int.MaxValue");
    }
}
