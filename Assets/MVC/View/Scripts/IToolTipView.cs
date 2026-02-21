using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface defining IToolTipView
/// for mocking in tests
/// </summary>
public interface IToolTipView
{
    void UpdateContent(IToolTipModel model);
    void setActive(bool active);
}
