using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IToolTipTrigger
{
    event Action HoverEntered;
    event Action HoverExited;
}
