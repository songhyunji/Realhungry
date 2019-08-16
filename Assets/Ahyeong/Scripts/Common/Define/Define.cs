﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MyFloatEvent : UnityEvent<float> { }

[System.Serializable]
public class MyBoolEvent : UnityEvent<bool> { }

[System.Serializable]
public class MyIntEvent : UnityEvent<int> { }