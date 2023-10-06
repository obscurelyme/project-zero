using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
  [Test]
  public void TestSubscribable()
  {
    //Assign
    var testStringSubscription = new Zero.Subscribable<string>();
    var testStringObserver = new Zero.Observer<string>();
    //Act
    var token = testStringSubscription.Subscribe(testStringObserver);
    testStringSubscription.Fire("Hello, World!");
    //Assert
    Assert.AreEqual("Hi", "Hello, World!");
  }
}
