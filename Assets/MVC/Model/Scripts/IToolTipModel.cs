using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface defining ToolTipModel
/// for mocking in tests
/// </summary>
public interface IToolTipModel
{
    string Title { get; set; }
    string Description { get; set; }
}
