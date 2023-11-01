using System.Collections;
using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

public class TestToSpline : MonoBehaviour
{
  [SerializeField]
  Transform goalLoc;
  Spline spl = new Spline();
  [SerializeField]
  SplineContainer container;

  BezierKnot knotToAdd = new BezierKnot(new float3(0, 0, 0));

  void Start()
  {
    container.Spline.Add(knotToAdd);
  }

  void Update()
  {
    transform.position = Vector3.MoveTowards(transform.position, goalLoc.position, 5 * Time.deltaTime);
  }
}
