using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace Unimpressive.Core
{
    /// <summary>
    /// Methods for intersections between primitives/solids
    /// </summary>
    public static class Intersections
    {
        /// <summary>
        /// Intersection test with ray and plane via offset and normal
        /// </summary>
        /// <param name="rayOrigin">Ray origin in world</param>
        /// <param name="rayDir">Normalized direction of the ray</param>
        /// <param name="planeCenter">Plane offset in world</param>
        /// <param name="planeNorm">Plane normal in world</param>
        /// <param name="isPoint">The intersection point in world space. 0 if no intersection.</param>
        /// <returns>Intersection happens or not</returns>
        public static bool PlaneRay(Vector3 rayOrigin, Vector3 rayDir, Vector3 planeCenter, Vector3 planeNorm, out Vector3 isPoint)
        {
            var rDotn = Vector3.Dot(Vector3.Normalize(rayDir), planeNorm);

            //parallel to plane or pointing away from plane?
            if (rDotn < 0.0000001)
            {
                isPoint = Vector3.Zero;
                return false;
            }

            var s = Vector3.Dot(planeNorm, planeCenter - rayOrigin) / rDotn;

            isPoint = rayOrigin + s * Vector3.Normalize(rayDir);

            return true;
        }

        /// <summary>
        /// Intersection test with ray and plane via plane transform
        /// </summary>
        /// <param name="rayOrigin">Ray origin in world</param>
        /// <param name="rayDir">Normalized direction of the ray</param>
        /// <param name="planeTr">Plane transformation. The XY plane is used which is looking at the Z+ forward camera (normal(0,0,-1))</param>
        /// <param name="isPoint">The intersection point in world space. 0 if no intersection.</param>
        /// <param name="pointOnPlane">The point in the plane's original space</param>
        /// <param name="doubleSided">If true ray is hitting the plane also when it's facing away from the ray</param>
        /// <returns>Intersection happens or not</returns>
        public static bool PlaneRay(Vector3 rayOrigin, Vector3 rayDir, Matrix4x4 planeTr, out Vector3 isPoint, out Vector3 pointOnPlane, bool doubleSided = true)
        {
            var norm = Vector3.TransformNormal(new Vector3(0, 0, 1), planeTr);
            var pos = planeTr.Translation;
            var ishit = PlaneRay(rayOrigin, rayDir, pos, norm, out var wpoint);
            if (!ishit && doubleSided) ishit = PlaneRay(rayOrigin, rayDir, pos, -norm, out wpoint);
            if (ishit)
            {
                isPoint = wpoint;
                Matrix4x4.Invert(planeTr, out var invPlaneTr);
                pointOnPlane = Vector3.Transform(wpoint, invPlaneTr);
                return true;
            }
            isPoint = Vector3.Zero;
            pointOnPlane = Vector3.Zero;
            return false;
        }

        /// <summary>
        /// AABB via minmax Intersects AABB via minmax in 2D
        /// </summary>
        /// <param name="b1min"></param>
        /// <param name="b1max"></param>
        /// <param name="b2min"></param>
        /// <param name="b2max"></param>
        /// <returns></returns>
        public static bool AabbAabb2D(Vector2 b1min, Vector2 b1max, Vector2 b2min, Vector2 b2max)
        {
            var b2minWi = b2min.X <= b1max.X && b2min.X >= b1min.X;
            var b1maxWi = b1max.X <= b2max.X && b1max.X >= b2min.X;
            var wi = b2minWi || b1maxWi;
            var b2minHi = b2min.Y <= b1max.Y && b2min.Y >= b1min.Y;
            var b1maxHi = b1max.Y <= b2max.Y && b1max.Y >= b2min.Y;
            var hi = b2minHi || b1maxHi;
            return wi && hi;
        }

        /// <summary>
        /// AABB via minmax Intersects AABB via minmax in 3D
        /// </summary>
        /// <param name="b1min"></param>
        /// <param name="b1max"></param>
        /// <param name="b2min"></param>
        /// <param name="b2max"></param>
        /// <returns></returns>
        public static bool AabbAabb3D(Vector3 b1min, Vector3 b1max, Vector3 b2min, Vector3 b2max)
        {
            var b2minWi = b2min.X <= b1max.X && b2min.X >= b1min.X;
            var b1maxWi = b1max.X <= b2max.X && b1max.X >= b2min.X;
            var wi = b2minWi || b1maxWi;
            var b2minHi = b2min.Y <= b1max.Y && b2min.Y >= b1min.Y;
            var b1maxHi = b1max.Y <= b2max.Y && b1max.Y >= b2min.Y;
            var hi = b2minHi || b1maxHi;
            var b2minDi = b2min.Z <= b1max.Z && b2min.Z >= b1min.Z;
            var b1maxDi = b1max.Z <= b2max.Z && b1max.Z >= b2min.Z;
            var di = b2minDi || b1maxDi;
            return wi && hi && di;
        }

        /// <summary>
        /// Is a point located in a box
        /// </summary>
        /// <param name="boxmin">Box bounds minimum</param>
        /// <param name="boxmax">Box bounds maximum</param>
        /// <param name="point">Point in question</param>
        /// <returns></returns>
        [Obsolete("Replaced with AabbPoint3D")]
        public static bool BoxPoint(Vector3 boxmin, Vector3 boxmax, Vector3 point)
        {
            return point.X >= boxmin.X && point.X <= boxmax.X &&
                   point.Y >= boxmin.Y && point.Y <= boxmax.Y &&
                   point.Z >= boxmin.Z && point.Z <= boxmax.Z;
        }

        /// <summary>
        /// Is a point located in a box
        /// </summary>
        /// <param name="boxmin">Box bounds minimum</param>
        /// <param name="boxmax">Box bounds maximum</param>
        /// <param name="point">Point in question</param>
        /// <returns></returns>
        public static bool AabbPoint3D(Vector3 boxmin, Vector3 boxmax, Vector3 point)
        {
            return point.X >= boxmin.X && point.X <= boxmax.X &&
                   point.Y >= boxmin.Y && point.Y <= boxmax.Y &&
                   point.Z >= boxmin.Z && point.Z <= boxmax.Z;
        }

        /// <summary>
        /// Limit points to the boundaries of a box
        /// </summary>
        /// <param name="boxmin">Box bounds minimum</param>
        /// <param name="boxmax">Box bounds maximum</param>
        /// <param name="point">Point in question</param>
        /// <returns>The point limited inside the boundaries of the box</returns>
        public static Vector3 BoxPointLimit(Vector3 boxmin, Vector3 boxmax, Vector3 point)
        {
            return new Vector3(
                Min(Max(point.X, boxmin.X), boxmax.X),
                Min(Max(point.Y, boxmin.Y), boxmax.Y),
                Min(Max(point.Z, boxmin.Z), boxmax.Z)
                );
        }
    }
}
