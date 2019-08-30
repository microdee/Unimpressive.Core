﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;

#pragma warning disable CS1591
namespace Unimpressive.Core
{
    public static class NumericsSwizzle
    {
        #region Vec2_from_Vec2
        #region GetAxis
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 xx(this Vector2 a) => new Vector2(a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 xy(this Vector2 a) => new Vector2(a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 yx(this Vector2 a) => new Vector2(a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 yy(this Vector2 a) => new Vector2(a.Y, a.Y);
        #endregion

        #region SetAxis
        public static Vector2 xy(this Vector2 a, Vector2 b) { var aa = a; aa.X = b.X; aa.Y = b.Y; return aa; }
        public static Vector2 yx(this Vector2 a, Vector2 b) { var aa = a; aa.Y = b.X; aa.X = b.Y; return aa; }
        #endregion
        #endregion

        #region Vec2_from_Vec3
        #region GetAxis
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 xx(this Vector3 a) => new Vector2(a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 xy(this Vector3 a) => new Vector2(a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 xz(this Vector3 a) => new Vector2(a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 yx(this Vector3 a) => new Vector2(a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 yy(this Vector3 a) => new Vector2(a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 yz(this Vector3 a) => new Vector2(a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 zx(this Vector3 a) => new Vector2(a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 zy(this Vector3 a) => new Vector2(a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 zz(this Vector3 a) => new Vector2(a.Z, a.Z);
        #endregion

        #region SetAxis
        public static Vector3 xy(this Vector3 a, Vector2 b) { var aa = a; aa.X = b.X; aa.Y = b.Y; return aa; }
        public static Vector3 xz(this Vector3 a, Vector2 b) { var aa = a; aa.X = b.X; aa.Z = b.Y; return aa; }
        public static Vector3 yx(this Vector3 a, Vector2 b) { var aa = a; aa.Y = b.X; aa.X = b.Y; return aa; }
        public static Vector3 zx(this Vector3 a, Vector2 b) { var aa = a; aa.Z = b.X; aa.X = b.Y; return aa; }
        public static Vector3 yz(this Vector3 a, Vector2 b) { var aa = a; aa.Y = b.X; aa.Z = b.Y; return aa; }
        public static Vector3 zy(this Vector3 a, Vector2 b) { var aa = a; aa.Z = b.X; aa.Y = b.Y; return aa; }
        #endregion
        #endregion

        #region Vec2_from_Vec4
        #region GetAxis
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 xx(this Vector4 a) => new Vector2(a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 xy(this Vector4 a) => new Vector2(a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 xz(this Vector4 a) => new Vector2(a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 xw(this Vector4 a) => new Vector2(a.X, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 yx(this Vector4 a) => new Vector2(a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 yy(this Vector4 a) => new Vector2(a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 yz(this Vector4 a) => new Vector2(a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 yw(this Vector4 a) => new Vector2(a.Y, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 zx(this Vector4 a) => new Vector2(a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 zy(this Vector4 a) => new Vector2(a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 zz(this Vector4 a) => new Vector2(a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 zw(this Vector4 a) => new Vector2(a.Z, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 wx(this Vector4 a) => new Vector2(a.W, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 wy(this Vector4 a) => new Vector2(a.W, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 wz(this Vector4 a) => new Vector2(a.W, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector2 ww(this Vector4 a) => new Vector2(a.W, a.W);
        #endregion

        #region SetAxis
        public static Vector4 xy(this Vector4 a, Vector2 b) { var aa = a; aa.X = b.X; aa.Y = b.Y; return aa; }
        public static Vector4 xz(this Vector4 a, Vector2 b) { var aa = a; aa.X = b.X; aa.Z = b.Y; return aa; }
        public static Vector4 xw(this Vector4 a, Vector2 b) { var aa = a; aa.X = b.X; aa.W = b.Y; return aa; }
        public static Vector4 yx(this Vector4 a, Vector2 b) { var aa = a; aa.Y = b.X; aa.X = b.Y; return aa; }
        public static Vector4 zx(this Vector4 a, Vector2 b) { var aa = a; aa.Z = b.X; aa.X = b.Y; return aa; }
        public static Vector4 wx(this Vector4 a, Vector2 b) { var aa = a; aa.W = b.X; aa.X = b.Y; return aa; }
        public static Vector4 yz(this Vector4 a, Vector2 b) { var aa = a; aa.Y = b.X; aa.Z = b.Y; return aa; }
        public static Vector4 yw(this Vector4 a, Vector2 b) { var aa = a; aa.Y = b.X; aa.W = b.Y; return aa; }
        public static Vector4 zy(this Vector4 a, Vector2 b) { var aa = a; aa.Z = b.X; aa.Y = b.Y; return aa; }
        public static Vector4 wy(this Vector4 a, Vector2 b) { var aa = a; aa.W = b.X; aa.Y = b.Y; return aa; }
        public static Vector4 zw(this Vector4 a, Vector2 b) { var aa = a; aa.Z = b.X; aa.W = b.Y; return aa; }
        public static Vector4 wz(this Vector4 a, Vector2 b) { var aa = a; aa.W = b.X; aa.Z = b.Y; return aa; }
        #endregion
        #endregion

        #region Vec3_from_Vec2
        #region GetAxis
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xxx(this Vector2 a) => new Vector3(a.X, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xxy(this Vector2 a) => new Vector3(a.X, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xyx(this Vector2 a) => new Vector3(a.X, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xyy(this Vector2 a) => new Vector3(a.X, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yxx(this Vector2 a) => new Vector3(a.Y, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yxy(this Vector2 a) => new Vector3(a.Y, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yyx(this Vector2 a) => new Vector3(a.Y, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yyy(this Vector2 a) => new Vector3(a.Y, a.Y, a.Y);
        #endregion

        #region SetAxis

        #endregion
        #endregion

        #region Vec3_from_Vec3
        #region GetAxis
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xxx(this Vector3 a) => new Vector3(a.X, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xxy(this Vector3 a) => new Vector3(a.X, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xxz(this Vector3 a) => new Vector3(a.X, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xyx(this Vector3 a) => new Vector3(a.X, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xyy(this Vector3 a) => new Vector3(a.X, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xyz(this Vector3 a) => new Vector3(a.X, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xzx(this Vector3 a) => new Vector3(a.X, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xzy(this Vector3 a) => new Vector3(a.X, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xzz(this Vector3 a) => new Vector3(a.X, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yxx(this Vector3 a) => new Vector3(a.Y, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yxy(this Vector3 a) => new Vector3(a.Y, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yxz(this Vector3 a) => new Vector3(a.Y, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yyx(this Vector3 a) => new Vector3(a.Y, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yyy(this Vector3 a) => new Vector3(a.Y, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yyz(this Vector3 a) => new Vector3(a.Y, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yzx(this Vector3 a) => new Vector3(a.Y, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yzy(this Vector3 a) => new Vector3(a.Y, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yzz(this Vector3 a) => new Vector3(a.Y, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zxx(this Vector3 a) => new Vector3(a.Z, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zxy(this Vector3 a) => new Vector3(a.Z, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zxz(this Vector3 a) => new Vector3(a.Z, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zyx(this Vector3 a) => new Vector3(a.Z, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zyy(this Vector3 a) => new Vector3(a.Z, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zyz(this Vector3 a) => new Vector3(a.Z, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zzx(this Vector3 a) => new Vector3(a.Z, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zzy(this Vector3 a) => new Vector3(a.Z, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zzz(this Vector3 a) => new Vector3(a.Z, a.Z, a.Z);
        #endregion

        #region SetAxis
        public static Vector3 xyz(this Vector3 a, Vector3 b) { var aa = a; aa.X = b.X; aa.Y = b.Y; aa.Z = b.Z; return aa; }
        public static Vector3 xzy(this Vector3 a, Vector3 b) { var aa = a; aa.X = b.X; aa.Z = b.Y; aa.Y = b.Z; return aa; }
        public static Vector3 yxz(this Vector3 a, Vector3 b) { var aa = a; aa.Y = b.X; aa.X = b.Y; aa.Z = b.Z; return aa; }
        public static Vector3 zxy(this Vector3 a, Vector3 b) { var aa = a; aa.Z = b.X; aa.X = b.Y; aa.Y = b.Z; return aa; }
        public static Vector3 yzx(this Vector3 a, Vector3 b) { var aa = a; aa.Y = b.X; aa.Z = b.Y; aa.X = b.Z; return aa; }
        public static Vector3 zyx(this Vector3 a, Vector3 b) { var aa = a; aa.Z = b.X; aa.Y = b.Y; aa.X = b.Z; return aa; }
        #endregion
        #endregion

        #region Vec3_from_Vec4
        #region GetAxis
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xxx(this Vector4 a) => new Vector3(a.X, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xxy(this Vector4 a) => new Vector3(a.X, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xxz(this Vector4 a) => new Vector3(a.X, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xxw(this Vector4 a) => new Vector3(a.X, a.X, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xyx(this Vector4 a) => new Vector3(a.X, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xyy(this Vector4 a) => new Vector3(a.X, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xyz(this Vector4 a) => new Vector3(a.X, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xyw(this Vector4 a) => new Vector3(a.X, a.Y, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xzx(this Vector4 a) => new Vector3(a.X, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xzy(this Vector4 a) => new Vector3(a.X, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xzz(this Vector4 a) => new Vector3(a.X, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xzw(this Vector4 a) => new Vector3(a.X, a.Z, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xwx(this Vector4 a) => new Vector3(a.X, a.W, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xwy(this Vector4 a) => new Vector3(a.X, a.W, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xwz(this Vector4 a) => new Vector3(a.X, a.W, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 xww(this Vector4 a) => new Vector3(a.X, a.W, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yxx(this Vector4 a) => new Vector3(a.Y, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yxy(this Vector4 a) => new Vector3(a.Y, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yxz(this Vector4 a) => new Vector3(a.Y, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yxw(this Vector4 a) => new Vector3(a.Y, a.X, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yyx(this Vector4 a) => new Vector3(a.Y, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yyy(this Vector4 a) => new Vector3(a.Y, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yyz(this Vector4 a) => new Vector3(a.Y, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yyw(this Vector4 a) => new Vector3(a.Y, a.Y, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yzx(this Vector4 a) => new Vector3(a.Y, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yzy(this Vector4 a) => new Vector3(a.Y, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yzz(this Vector4 a) => new Vector3(a.Y, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yzw(this Vector4 a) => new Vector3(a.Y, a.Z, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 ywx(this Vector4 a) => new Vector3(a.Y, a.W, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 ywy(this Vector4 a) => new Vector3(a.Y, a.W, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 ywz(this Vector4 a) => new Vector3(a.Y, a.W, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 yww(this Vector4 a) => new Vector3(a.Y, a.W, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zxx(this Vector4 a) => new Vector3(a.Z, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zxy(this Vector4 a) => new Vector3(a.Z, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zxz(this Vector4 a) => new Vector3(a.Z, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zxw(this Vector4 a) => new Vector3(a.Z, a.X, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zyx(this Vector4 a) => new Vector3(a.Z, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zyy(this Vector4 a) => new Vector3(a.Z, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zyz(this Vector4 a) => new Vector3(a.Z, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zyw(this Vector4 a) => new Vector3(a.Z, a.Y, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zzx(this Vector4 a) => new Vector3(a.Z, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zzy(this Vector4 a) => new Vector3(a.Z, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zzz(this Vector4 a) => new Vector3(a.Z, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zzw(this Vector4 a) => new Vector3(a.Z, a.Z, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zwx(this Vector4 a) => new Vector3(a.Z, a.W, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zwy(this Vector4 a) => new Vector3(a.Z, a.W, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zwz(this Vector4 a) => new Vector3(a.Z, a.W, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 zww(this Vector4 a) => new Vector3(a.Z, a.W, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 wxx(this Vector4 a) => new Vector3(a.W, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 wxy(this Vector4 a) => new Vector3(a.W, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 wxz(this Vector4 a) => new Vector3(a.W, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 wxw(this Vector4 a) => new Vector3(a.W, a.X, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 wyx(this Vector4 a) => new Vector3(a.W, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 wyy(this Vector4 a) => new Vector3(a.W, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 wyz(this Vector4 a) => new Vector3(a.W, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 wyw(this Vector4 a) => new Vector3(a.W, a.Y, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 wzx(this Vector4 a) => new Vector3(a.W, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 wzy(this Vector4 a) => new Vector3(a.W, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 wzz(this Vector4 a) => new Vector3(a.W, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 wzw(this Vector4 a) => new Vector3(a.W, a.Z, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 wwx(this Vector4 a) => new Vector3(a.W, a.W, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 wwy(this Vector4 a) => new Vector3(a.W, a.W, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 wwz(this Vector4 a) => new Vector3(a.W, a.W, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector3 www(this Vector4 a) => new Vector3(a.W, a.W, a.W);
        #endregion

        #region SetAxis
        public static Vector4 xyz(this Vector4 a, Vector3 b) { var aa = a; aa.X = b.X; aa.Y = b.Y; aa.Z = b.Z; return aa; }
        public static Vector4 xyw(this Vector4 a, Vector3 b) { var aa = a; aa.X = b.X; aa.Y = b.Y; aa.W = b.Z; return aa; }
        public static Vector4 xzy(this Vector4 a, Vector3 b) { var aa = a; aa.X = b.X; aa.Z = b.Y; aa.Y = b.Z; return aa; }
        public static Vector4 xwy(this Vector4 a, Vector3 b) { var aa = a; aa.X = b.X; aa.W = b.Y; aa.Y = b.Z; return aa; }
        public static Vector4 xzw(this Vector4 a, Vector3 b) { var aa = a; aa.X = b.X; aa.Z = b.Y; aa.W = b.Z; return aa; }
        public static Vector4 xwz(this Vector4 a, Vector3 b) { var aa = a; aa.X = b.X; aa.W = b.Y; aa.Z = b.Z; return aa; }
        public static Vector4 yxz(this Vector4 a, Vector3 b) { var aa = a; aa.Y = b.X; aa.X = b.Y; aa.Z = b.Z; return aa; }
        public static Vector4 yxw(this Vector4 a, Vector3 b) { var aa = a; aa.Y = b.X; aa.X = b.Y; aa.W = b.Z; return aa; }
        public static Vector4 zxy(this Vector4 a, Vector3 b) { var aa = a; aa.Z = b.X; aa.X = b.Y; aa.Y = b.Z; return aa; }
        public static Vector4 wxy(this Vector4 a, Vector3 b) { var aa = a; aa.W = b.X; aa.X = b.Y; aa.Y = b.Z; return aa; }
        public static Vector4 zxw(this Vector4 a, Vector3 b) { var aa = a; aa.Z = b.X; aa.X = b.Y; aa.W = b.Z; return aa; }
        public static Vector4 wxz(this Vector4 a, Vector3 b) { var aa = a; aa.W = b.X; aa.X = b.Y; aa.Z = b.Z; return aa; }
        public static Vector4 yzx(this Vector4 a, Vector3 b) { var aa = a; aa.Y = b.X; aa.Z = b.Y; aa.X = b.Z; return aa; }
        public static Vector4 ywx(this Vector4 a, Vector3 b) { var aa = a; aa.Y = b.X; aa.W = b.Y; aa.X = b.Z; return aa; }
        public static Vector4 zyx(this Vector4 a, Vector3 b) { var aa = a; aa.Z = b.X; aa.Y = b.Y; aa.X = b.Z; return aa; }
        public static Vector4 wyx(this Vector4 a, Vector3 b) { var aa = a; aa.W = b.X; aa.Y = b.Y; aa.X = b.Z; return aa; }
        public static Vector4 zwx(this Vector4 a, Vector3 b) { var aa = a; aa.Z = b.X; aa.W = b.Y; aa.X = b.Z; return aa; }
        public static Vector4 wzx(this Vector4 a, Vector3 b) { var aa = a; aa.W = b.X; aa.Z = b.Y; aa.X = b.Z; return aa; }
        public static Vector4 yzw(this Vector4 a, Vector3 b) { var aa = a; aa.Y = b.X; aa.Z = b.Y; aa.W = b.Z; return aa; }
        public static Vector4 ywz(this Vector4 a, Vector3 b) { var aa = a; aa.Y = b.X; aa.W = b.Y; aa.Z = b.Z; return aa; }
        public static Vector4 zyw(this Vector4 a, Vector3 b) { var aa = a; aa.Z = b.X; aa.Y = b.Y; aa.W = b.Z; return aa; }
        public static Vector4 wyz(this Vector4 a, Vector3 b) { var aa = a; aa.W = b.X; aa.Y = b.Y; aa.Z = b.Z; return aa; }
        public static Vector4 zwy(this Vector4 a, Vector3 b) { var aa = a; aa.Z = b.X; aa.W = b.Y; aa.Y = b.Z; return aa; }
        public static Vector4 wzy(this Vector4 a, Vector3 b) { var aa = a; aa.W = b.X; aa.Z = b.Y; aa.Y = b.Z; return aa; }
        #endregion
        #endregion

        #region Vec4_from_Vec2
        #region GetAxis
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxxx(this Vector2 a) => new Vector4(a.X, a.X, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxxy(this Vector2 a) => new Vector4(a.X, a.X, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxyx(this Vector2 a) => new Vector4(a.X, a.X, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxyy(this Vector2 a) => new Vector4(a.X, a.X, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyxx(this Vector2 a) => new Vector4(a.X, a.Y, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyxy(this Vector2 a) => new Vector4(a.X, a.Y, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyyx(this Vector2 a) => new Vector4(a.X, a.Y, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyyy(this Vector2 a) => new Vector4(a.X, a.Y, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxxx(this Vector2 a) => new Vector4(a.Y, a.X, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxxy(this Vector2 a) => new Vector4(a.Y, a.X, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxyx(this Vector2 a) => new Vector4(a.Y, a.X, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxyy(this Vector2 a) => new Vector4(a.Y, a.X, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyxx(this Vector2 a) => new Vector4(a.Y, a.Y, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyxy(this Vector2 a) => new Vector4(a.Y, a.Y, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyyx(this Vector2 a) => new Vector4(a.Y, a.Y, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyyy(this Vector2 a) => new Vector4(a.Y, a.Y, a.Y, a.Y);
        #endregion

        #region SetAxis

        #endregion
        #endregion

        #region Vec4_from_Vec3
        #region GetAxis
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxxx(this Vector3 a) => new Vector4(a.X, a.X, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxxy(this Vector3 a) => new Vector4(a.X, a.X, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxxz(this Vector3 a) => new Vector4(a.X, a.X, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxyx(this Vector3 a) => new Vector4(a.X, a.X, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxyy(this Vector3 a) => new Vector4(a.X, a.X, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxyz(this Vector3 a) => new Vector4(a.X, a.X, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxzx(this Vector3 a) => new Vector4(a.X, a.X, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxzy(this Vector3 a) => new Vector4(a.X, a.X, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxzz(this Vector3 a) => new Vector4(a.X, a.X, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyxx(this Vector3 a) => new Vector4(a.X, a.Y, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyxy(this Vector3 a) => new Vector4(a.X, a.Y, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyxz(this Vector3 a) => new Vector4(a.X, a.Y, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyyx(this Vector3 a) => new Vector4(a.X, a.Y, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyyy(this Vector3 a) => new Vector4(a.X, a.Y, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyyz(this Vector3 a) => new Vector4(a.X, a.Y, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyzx(this Vector3 a) => new Vector4(a.X, a.Y, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyzy(this Vector3 a) => new Vector4(a.X, a.Y, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyzz(this Vector3 a) => new Vector4(a.X, a.Y, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzxx(this Vector3 a) => new Vector4(a.X, a.Z, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzxy(this Vector3 a) => new Vector4(a.X, a.Z, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzxz(this Vector3 a) => new Vector4(a.X, a.Z, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzyx(this Vector3 a) => new Vector4(a.X, a.Z, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzyy(this Vector3 a) => new Vector4(a.X, a.Z, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzyz(this Vector3 a) => new Vector4(a.X, a.Z, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzzx(this Vector3 a) => new Vector4(a.X, a.Z, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzzy(this Vector3 a) => new Vector4(a.X, a.Z, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzzz(this Vector3 a) => new Vector4(a.X, a.Z, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxxx(this Vector3 a) => new Vector4(a.Y, a.X, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxxy(this Vector3 a) => new Vector4(a.Y, a.X, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxxz(this Vector3 a) => new Vector4(a.Y, a.X, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxyx(this Vector3 a) => new Vector4(a.Y, a.X, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxyy(this Vector3 a) => new Vector4(a.Y, a.X, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxyz(this Vector3 a) => new Vector4(a.Y, a.X, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxzx(this Vector3 a) => new Vector4(a.Y, a.X, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxzy(this Vector3 a) => new Vector4(a.Y, a.X, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxzz(this Vector3 a) => new Vector4(a.Y, a.X, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyxx(this Vector3 a) => new Vector4(a.Y, a.Y, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyxy(this Vector3 a) => new Vector4(a.Y, a.Y, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyxz(this Vector3 a) => new Vector4(a.Y, a.Y, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyyx(this Vector3 a) => new Vector4(a.Y, a.Y, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyyy(this Vector3 a) => new Vector4(a.Y, a.Y, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyyz(this Vector3 a) => new Vector4(a.Y, a.Y, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyzx(this Vector3 a) => new Vector4(a.Y, a.Y, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyzy(this Vector3 a) => new Vector4(a.Y, a.Y, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyzz(this Vector3 a) => new Vector4(a.Y, a.Y, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzxx(this Vector3 a) => new Vector4(a.Y, a.Z, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzxy(this Vector3 a) => new Vector4(a.Y, a.Z, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzxz(this Vector3 a) => new Vector4(a.Y, a.Z, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzyx(this Vector3 a) => new Vector4(a.Y, a.Z, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzyy(this Vector3 a) => new Vector4(a.Y, a.Z, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzyz(this Vector3 a) => new Vector4(a.Y, a.Z, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzzx(this Vector3 a) => new Vector4(a.Y, a.Z, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzzy(this Vector3 a) => new Vector4(a.Y, a.Z, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzzz(this Vector3 a) => new Vector4(a.Y, a.Z, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxxx(this Vector3 a) => new Vector4(a.Z, a.X, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxxy(this Vector3 a) => new Vector4(a.Z, a.X, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxxz(this Vector3 a) => new Vector4(a.Z, a.X, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxyx(this Vector3 a) => new Vector4(a.Z, a.X, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxyy(this Vector3 a) => new Vector4(a.Z, a.X, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxyz(this Vector3 a) => new Vector4(a.Z, a.X, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxzx(this Vector3 a) => new Vector4(a.Z, a.X, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxzy(this Vector3 a) => new Vector4(a.Z, a.X, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxzz(this Vector3 a) => new Vector4(a.Z, a.X, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zyxx(this Vector3 a) => new Vector4(a.Z, a.Y, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zyxy(this Vector3 a) => new Vector4(a.Z, a.Y, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zyxz(this Vector3 a) => new Vector4(a.Z, a.Y, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zyyx(this Vector3 a) => new Vector4(a.Z, a.Y, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zyyy(this Vector3 a) => new Vector4(a.Z, a.Y, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zyyz(this Vector3 a) => new Vector4(a.Z, a.Y, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zyzx(this Vector3 a) => new Vector4(a.Z, a.Y, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zyzy(this Vector3 a) => new Vector4(a.Z, a.Y, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zyzz(this Vector3 a) => new Vector4(a.Z, a.Y, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzxx(this Vector3 a) => new Vector4(a.Z, a.Z, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzxy(this Vector3 a) => new Vector4(a.Z, a.Z, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzxz(this Vector3 a) => new Vector4(a.Z, a.Z, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzyx(this Vector3 a) => new Vector4(a.Z, a.Z, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzyy(this Vector3 a) => new Vector4(a.Z, a.Z, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzyz(this Vector3 a) => new Vector4(a.Z, a.Z, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzzx(this Vector3 a) => new Vector4(a.Z, a.Z, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzzy(this Vector3 a) => new Vector4(a.Z, a.Z, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzzz(this Vector3 a) => new Vector4(a.Z, a.Z, a.Z, a.Z);
        #endregion

        #region SetAxis

        #endregion
        #endregion

        #region Vec4_from_Vec4
        #region GetAxis
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxxx(this Vector4 a) => new Vector4(a.X, a.X, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxxy(this Vector4 a) => new Vector4(a.X, a.X, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxxz(this Vector4 a) => new Vector4(a.X, a.X, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxxw(this Vector4 a) => new Vector4(a.X, a.X, a.X, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxyx(this Vector4 a) => new Vector4(a.X, a.X, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxyy(this Vector4 a) => new Vector4(a.X, a.X, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxyz(this Vector4 a) => new Vector4(a.X, a.X, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxyw(this Vector4 a) => new Vector4(a.X, a.X, a.Y, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxzx(this Vector4 a) => new Vector4(a.X, a.X, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxzy(this Vector4 a) => new Vector4(a.X, a.X, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxzz(this Vector4 a) => new Vector4(a.X, a.X, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxzw(this Vector4 a) => new Vector4(a.X, a.X, a.Z, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxwx(this Vector4 a) => new Vector4(a.X, a.X, a.W, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxwy(this Vector4 a) => new Vector4(a.X, a.X, a.W, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxwz(this Vector4 a) => new Vector4(a.X, a.X, a.W, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xxww(this Vector4 a) => new Vector4(a.X, a.X, a.W, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyxx(this Vector4 a) => new Vector4(a.X, a.Y, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyxy(this Vector4 a) => new Vector4(a.X, a.Y, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyxz(this Vector4 a) => new Vector4(a.X, a.Y, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyxw(this Vector4 a) => new Vector4(a.X, a.Y, a.X, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyyx(this Vector4 a) => new Vector4(a.X, a.Y, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyyy(this Vector4 a) => new Vector4(a.X, a.Y, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyyz(this Vector4 a) => new Vector4(a.X, a.Y, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyyw(this Vector4 a) => new Vector4(a.X, a.Y, a.Y, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyzx(this Vector4 a) => new Vector4(a.X, a.Y, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyzy(this Vector4 a) => new Vector4(a.X, a.Y, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyzz(this Vector4 a) => new Vector4(a.X, a.Y, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyzw(this Vector4 a) => new Vector4(a.X, a.Y, a.Z, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xywx(this Vector4 a) => new Vector4(a.X, a.Y, a.W, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xywy(this Vector4 a) => new Vector4(a.X, a.Y, a.W, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xywz(this Vector4 a) => new Vector4(a.X, a.Y, a.W, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xyww(this Vector4 a) => new Vector4(a.X, a.Y, a.W, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzxx(this Vector4 a) => new Vector4(a.X, a.Z, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzxy(this Vector4 a) => new Vector4(a.X, a.Z, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzxz(this Vector4 a) => new Vector4(a.X, a.Z, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzxw(this Vector4 a) => new Vector4(a.X, a.Z, a.X, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzyx(this Vector4 a) => new Vector4(a.X, a.Z, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzyy(this Vector4 a) => new Vector4(a.X, a.Z, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzyz(this Vector4 a) => new Vector4(a.X, a.Z, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzyw(this Vector4 a) => new Vector4(a.X, a.Z, a.Y, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzzx(this Vector4 a) => new Vector4(a.X, a.Z, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzzy(this Vector4 a) => new Vector4(a.X, a.Z, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzzz(this Vector4 a) => new Vector4(a.X, a.Z, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzzw(this Vector4 a) => new Vector4(a.X, a.Z, a.Z, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzwx(this Vector4 a) => new Vector4(a.X, a.Z, a.W, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzwy(this Vector4 a) => new Vector4(a.X, a.Z, a.W, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzwz(this Vector4 a) => new Vector4(a.X, a.Z, a.W, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xzww(this Vector4 a) => new Vector4(a.X, a.Z, a.W, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xwxx(this Vector4 a) => new Vector4(a.X, a.W, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xwxy(this Vector4 a) => new Vector4(a.X, a.W, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xwxz(this Vector4 a) => new Vector4(a.X, a.W, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xwxw(this Vector4 a) => new Vector4(a.X, a.W, a.X, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xwyx(this Vector4 a) => new Vector4(a.X, a.W, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xwyy(this Vector4 a) => new Vector4(a.X, a.W, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xwyz(this Vector4 a) => new Vector4(a.X, a.W, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xwyw(this Vector4 a) => new Vector4(a.X, a.W, a.Y, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xwzx(this Vector4 a) => new Vector4(a.X, a.W, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xwzy(this Vector4 a) => new Vector4(a.X, a.W, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xwzz(this Vector4 a) => new Vector4(a.X, a.W, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xwzw(this Vector4 a) => new Vector4(a.X, a.W, a.Z, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xwwx(this Vector4 a) => new Vector4(a.X, a.W, a.W, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xwwy(this Vector4 a) => new Vector4(a.X, a.W, a.W, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xwwz(this Vector4 a) => new Vector4(a.X, a.W, a.W, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 xwww(this Vector4 a) => new Vector4(a.X, a.W, a.W, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxxx(this Vector4 a) => new Vector4(a.Y, a.X, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxxy(this Vector4 a) => new Vector4(a.Y, a.X, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxxz(this Vector4 a) => new Vector4(a.Y, a.X, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxxw(this Vector4 a) => new Vector4(a.Y, a.X, a.X, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxyx(this Vector4 a) => new Vector4(a.Y, a.X, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxyy(this Vector4 a) => new Vector4(a.Y, a.X, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxyz(this Vector4 a) => new Vector4(a.Y, a.X, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxyw(this Vector4 a) => new Vector4(a.Y, a.X, a.Y, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxzx(this Vector4 a) => new Vector4(a.Y, a.X, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxzy(this Vector4 a) => new Vector4(a.Y, a.X, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxzz(this Vector4 a) => new Vector4(a.Y, a.X, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxzw(this Vector4 a) => new Vector4(a.Y, a.X, a.Z, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxwx(this Vector4 a) => new Vector4(a.Y, a.X, a.W, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxwy(this Vector4 a) => new Vector4(a.Y, a.X, a.W, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxwz(this Vector4 a) => new Vector4(a.Y, a.X, a.W, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yxww(this Vector4 a) => new Vector4(a.Y, a.X, a.W, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyxx(this Vector4 a) => new Vector4(a.Y, a.Y, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyxy(this Vector4 a) => new Vector4(a.Y, a.Y, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyxz(this Vector4 a) => new Vector4(a.Y, a.Y, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyxw(this Vector4 a) => new Vector4(a.Y, a.Y, a.X, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyyx(this Vector4 a) => new Vector4(a.Y, a.Y, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyyy(this Vector4 a) => new Vector4(a.Y, a.Y, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyyz(this Vector4 a) => new Vector4(a.Y, a.Y, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyyw(this Vector4 a) => new Vector4(a.Y, a.Y, a.Y, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyzx(this Vector4 a) => new Vector4(a.Y, a.Y, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyzy(this Vector4 a) => new Vector4(a.Y, a.Y, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyzz(this Vector4 a) => new Vector4(a.Y, a.Y, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyzw(this Vector4 a) => new Vector4(a.Y, a.Y, a.Z, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yywx(this Vector4 a) => new Vector4(a.Y, a.Y, a.W, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yywy(this Vector4 a) => new Vector4(a.Y, a.Y, a.W, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yywz(this Vector4 a) => new Vector4(a.Y, a.Y, a.W, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yyww(this Vector4 a) => new Vector4(a.Y, a.Y, a.W, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzxx(this Vector4 a) => new Vector4(a.Y, a.Z, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzxy(this Vector4 a) => new Vector4(a.Y, a.Z, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzxz(this Vector4 a) => new Vector4(a.Y, a.Z, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzxw(this Vector4 a) => new Vector4(a.Y, a.Z, a.X, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzyx(this Vector4 a) => new Vector4(a.Y, a.Z, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzyy(this Vector4 a) => new Vector4(a.Y, a.Z, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzyz(this Vector4 a) => new Vector4(a.Y, a.Z, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzyw(this Vector4 a) => new Vector4(a.Y, a.Z, a.Y, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzzx(this Vector4 a) => new Vector4(a.Y, a.Z, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzzy(this Vector4 a) => new Vector4(a.Y, a.Z, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzzz(this Vector4 a) => new Vector4(a.Y, a.Z, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzzw(this Vector4 a) => new Vector4(a.Y, a.Z, a.Z, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzwx(this Vector4 a) => new Vector4(a.Y, a.Z, a.W, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzwy(this Vector4 a) => new Vector4(a.Y, a.Z, a.W, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzwz(this Vector4 a) => new Vector4(a.Y, a.Z, a.W, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 yzww(this Vector4 a) => new Vector4(a.Y, a.Z, a.W, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 ywxx(this Vector4 a) => new Vector4(a.Y, a.W, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 ywxy(this Vector4 a) => new Vector4(a.Y, a.W, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 ywxz(this Vector4 a) => new Vector4(a.Y, a.W, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 ywxw(this Vector4 a) => new Vector4(a.Y, a.W, a.X, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 ywyx(this Vector4 a) => new Vector4(a.Y, a.W, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 ywyy(this Vector4 a) => new Vector4(a.Y, a.W, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 ywyz(this Vector4 a) => new Vector4(a.Y, a.W, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 ywyw(this Vector4 a) => new Vector4(a.Y, a.W, a.Y, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 ywzx(this Vector4 a) => new Vector4(a.Y, a.W, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 ywzy(this Vector4 a) => new Vector4(a.Y, a.W, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 ywzz(this Vector4 a) => new Vector4(a.Y, a.W, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 ywzw(this Vector4 a) => new Vector4(a.Y, a.W, a.Z, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 ywwx(this Vector4 a) => new Vector4(a.Y, a.W, a.W, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 ywwy(this Vector4 a) => new Vector4(a.Y, a.W, a.W, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 ywwz(this Vector4 a) => new Vector4(a.Y, a.W, a.W, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 ywww(this Vector4 a) => new Vector4(a.Y, a.W, a.W, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxxx(this Vector4 a) => new Vector4(a.Z, a.X, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxxy(this Vector4 a) => new Vector4(a.Z, a.X, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxxz(this Vector4 a) => new Vector4(a.Z, a.X, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxxw(this Vector4 a) => new Vector4(a.Z, a.X, a.X, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxyx(this Vector4 a) => new Vector4(a.Z, a.X, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxyy(this Vector4 a) => new Vector4(a.Z, a.X, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxyz(this Vector4 a) => new Vector4(a.Z, a.X, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxyw(this Vector4 a) => new Vector4(a.Z, a.X, a.Y, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxzx(this Vector4 a) => new Vector4(a.Z, a.X, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxzy(this Vector4 a) => new Vector4(a.Z, a.X, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxzz(this Vector4 a) => new Vector4(a.Z, a.X, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxzw(this Vector4 a) => new Vector4(a.Z, a.X, a.Z, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxwx(this Vector4 a) => new Vector4(a.Z, a.X, a.W, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxwy(this Vector4 a) => new Vector4(a.Z, a.X, a.W, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxwz(this Vector4 a) => new Vector4(a.Z, a.X, a.W, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zxww(this Vector4 a) => new Vector4(a.Z, a.X, a.W, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zyxx(this Vector4 a) => new Vector4(a.Z, a.Y, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zyxy(this Vector4 a) => new Vector4(a.Z, a.Y, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zyxz(this Vector4 a) => new Vector4(a.Z, a.Y, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zyxw(this Vector4 a) => new Vector4(a.Z, a.Y, a.X, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zyyx(this Vector4 a) => new Vector4(a.Z, a.Y, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zyyy(this Vector4 a) => new Vector4(a.Z, a.Y, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zyyz(this Vector4 a) => new Vector4(a.Z, a.Y, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zyyw(this Vector4 a) => new Vector4(a.Z, a.Y, a.Y, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zyzx(this Vector4 a) => new Vector4(a.Z, a.Y, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zyzy(this Vector4 a) => new Vector4(a.Z, a.Y, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zyzz(this Vector4 a) => new Vector4(a.Z, a.Y, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zyzw(this Vector4 a) => new Vector4(a.Z, a.Y, a.Z, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zywx(this Vector4 a) => new Vector4(a.Z, a.Y, a.W, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zywy(this Vector4 a) => new Vector4(a.Z, a.Y, a.W, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zywz(this Vector4 a) => new Vector4(a.Z, a.Y, a.W, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zyww(this Vector4 a) => new Vector4(a.Z, a.Y, a.W, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzxx(this Vector4 a) => new Vector4(a.Z, a.Z, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzxy(this Vector4 a) => new Vector4(a.Z, a.Z, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzxz(this Vector4 a) => new Vector4(a.Z, a.Z, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzxw(this Vector4 a) => new Vector4(a.Z, a.Z, a.X, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzyx(this Vector4 a) => new Vector4(a.Z, a.Z, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzyy(this Vector4 a) => new Vector4(a.Z, a.Z, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzyz(this Vector4 a) => new Vector4(a.Z, a.Z, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzyw(this Vector4 a) => new Vector4(a.Z, a.Z, a.Y, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzzx(this Vector4 a) => new Vector4(a.Z, a.Z, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzzy(this Vector4 a) => new Vector4(a.Z, a.Z, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzzz(this Vector4 a) => new Vector4(a.Z, a.Z, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzzw(this Vector4 a) => new Vector4(a.Z, a.Z, a.Z, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzwx(this Vector4 a) => new Vector4(a.Z, a.Z, a.W, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzwy(this Vector4 a) => new Vector4(a.Z, a.Z, a.W, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzwz(this Vector4 a) => new Vector4(a.Z, a.Z, a.W, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zzww(this Vector4 a) => new Vector4(a.Z, a.Z, a.W, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zwxx(this Vector4 a) => new Vector4(a.Z, a.W, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zwxy(this Vector4 a) => new Vector4(a.Z, a.W, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zwxz(this Vector4 a) => new Vector4(a.Z, a.W, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zwxw(this Vector4 a) => new Vector4(a.Z, a.W, a.X, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zwyx(this Vector4 a) => new Vector4(a.Z, a.W, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zwyy(this Vector4 a) => new Vector4(a.Z, a.W, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zwyz(this Vector4 a) => new Vector4(a.Z, a.W, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zwyw(this Vector4 a) => new Vector4(a.Z, a.W, a.Y, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zwzx(this Vector4 a) => new Vector4(a.Z, a.W, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zwzy(this Vector4 a) => new Vector4(a.Z, a.W, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zwzz(this Vector4 a) => new Vector4(a.Z, a.W, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zwzw(this Vector4 a) => new Vector4(a.Z, a.W, a.Z, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zwwx(this Vector4 a) => new Vector4(a.Z, a.W, a.W, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zwwy(this Vector4 a) => new Vector4(a.Z, a.W, a.W, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zwwz(this Vector4 a) => new Vector4(a.Z, a.W, a.W, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 zwww(this Vector4 a) => new Vector4(a.Z, a.W, a.W, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wxxx(this Vector4 a) => new Vector4(a.W, a.X, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wxxy(this Vector4 a) => new Vector4(a.W, a.X, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wxxz(this Vector4 a) => new Vector4(a.W, a.X, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wxxw(this Vector4 a) => new Vector4(a.W, a.X, a.X, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wxyx(this Vector4 a) => new Vector4(a.W, a.X, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wxyy(this Vector4 a) => new Vector4(a.W, a.X, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wxyz(this Vector4 a) => new Vector4(a.W, a.X, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wxyw(this Vector4 a) => new Vector4(a.W, a.X, a.Y, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wxzx(this Vector4 a) => new Vector4(a.W, a.X, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wxzy(this Vector4 a) => new Vector4(a.W, a.X, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wxzz(this Vector4 a) => new Vector4(a.W, a.X, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wxzw(this Vector4 a) => new Vector4(a.W, a.X, a.Z, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wxwx(this Vector4 a) => new Vector4(a.W, a.X, a.W, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wxwy(this Vector4 a) => new Vector4(a.W, a.X, a.W, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wxwz(this Vector4 a) => new Vector4(a.W, a.X, a.W, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wxww(this Vector4 a) => new Vector4(a.W, a.X, a.W, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wyxx(this Vector4 a) => new Vector4(a.W, a.Y, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wyxy(this Vector4 a) => new Vector4(a.W, a.Y, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wyxz(this Vector4 a) => new Vector4(a.W, a.Y, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wyxw(this Vector4 a) => new Vector4(a.W, a.Y, a.X, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wyyx(this Vector4 a) => new Vector4(a.W, a.Y, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wyyy(this Vector4 a) => new Vector4(a.W, a.Y, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wyyz(this Vector4 a) => new Vector4(a.W, a.Y, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wyyw(this Vector4 a) => new Vector4(a.W, a.Y, a.Y, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wyzx(this Vector4 a) => new Vector4(a.W, a.Y, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wyzy(this Vector4 a) => new Vector4(a.W, a.Y, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wyzz(this Vector4 a) => new Vector4(a.W, a.Y, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wyzw(this Vector4 a) => new Vector4(a.W, a.Y, a.Z, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wywx(this Vector4 a) => new Vector4(a.W, a.Y, a.W, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wywy(this Vector4 a) => new Vector4(a.W, a.Y, a.W, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wywz(this Vector4 a) => new Vector4(a.W, a.Y, a.W, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wyww(this Vector4 a) => new Vector4(a.W, a.Y, a.W, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wzxx(this Vector4 a) => new Vector4(a.W, a.Z, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wzxy(this Vector4 a) => new Vector4(a.W, a.Z, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wzxz(this Vector4 a) => new Vector4(a.W, a.Z, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wzxw(this Vector4 a) => new Vector4(a.W, a.Z, a.X, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wzyx(this Vector4 a) => new Vector4(a.W, a.Z, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wzyy(this Vector4 a) => new Vector4(a.W, a.Z, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wzyz(this Vector4 a) => new Vector4(a.W, a.Z, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wzyw(this Vector4 a) => new Vector4(a.W, a.Z, a.Y, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wzzx(this Vector4 a) => new Vector4(a.W, a.Z, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wzzy(this Vector4 a) => new Vector4(a.W, a.Z, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wzzz(this Vector4 a) => new Vector4(a.W, a.Z, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wzzw(this Vector4 a) => new Vector4(a.W, a.Z, a.Z, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wzwx(this Vector4 a) => new Vector4(a.W, a.Z, a.W, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wzwy(this Vector4 a) => new Vector4(a.W, a.Z, a.W, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wzwz(this Vector4 a) => new Vector4(a.W, a.Z, a.W, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wzww(this Vector4 a) => new Vector4(a.W, a.Z, a.W, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wwxx(this Vector4 a) => new Vector4(a.W, a.W, a.X, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wwxy(this Vector4 a) => new Vector4(a.W, a.W, a.X, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wwxz(this Vector4 a) => new Vector4(a.W, a.W, a.X, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wwxw(this Vector4 a) => new Vector4(a.W, a.W, a.X, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wwyx(this Vector4 a) => new Vector4(a.W, a.W, a.Y, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wwyy(this Vector4 a) => new Vector4(a.W, a.W, a.Y, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wwyz(this Vector4 a) => new Vector4(a.W, a.W, a.Y, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wwyw(this Vector4 a) => new Vector4(a.W, a.W, a.Y, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wwzx(this Vector4 a) => new Vector4(a.W, a.W, a.Z, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wwzy(this Vector4 a) => new Vector4(a.W, a.W, a.Z, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wwzz(this Vector4 a) => new Vector4(a.W, a.W, a.Z, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wwzw(this Vector4 a) => new Vector4(a.W, a.W, a.Z, a.W);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wwwx(this Vector4 a) => new Vector4(a.W, a.W, a.W, a.X);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wwwy(this Vector4 a) => new Vector4(a.W, a.W, a.W, a.Y);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wwwz(this Vector4 a) => new Vector4(a.W, a.W, a.W, a.Z);
        [MethodImpl(MethodImplOptions.AggressiveInlining), Pure] public static Vector4 wwww(this Vector4 a) => new Vector4(a.W, a.W, a.W, a.W);
        #endregion

        #region SetAxis
        public static Vector4 xyzw(this Vector4 a, Vector4 b) { var aa = a; aa.X = b.X; aa.Y = b.Y; aa.Z = b.Z; aa.W = b.W; return aa; }
        public static Vector4 xywz(this Vector4 a, Vector4 b) { var aa = a; aa.X = b.X; aa.Y = b.Y; aa.W = b.Z; aa.Z = b.W; return aa; }
        public static Vector4 xzyw(this Vector4 a, Vector4 b) { var aa = a; aa.X = b.X; aa.Z = b.Y; aa.Y = b.Z; aa.W = b.W; return aa; }
        public static Vector4 xwyz(this Vector4 a, Vector4 b) { var aa = a; aa.X = b.X; aa.W = b.Y; aa.Y = b.Z; aa.Z = b.W; return aa; }
        public static Vector4 xzwy(this Vector4 a, Vector4 b) { var aa = a; aa.X = b.X; aa.Z = b.Y; aa.W = b.Z; aa.Y = b.W; return aa; }
        public static Vector4 xwzy(this Vector4 a, Vector4 b) { var aa = a; aa.X = b.X; aa.W = b.Y; aa.Z = b.Z; aa.Y = b.W; return aa; }
        public static Vector4 yxzw(this Vector4 a, Vector4 b) { var aa = a; aa.Y = b.X; aa.X = b.Y; aa.Z = b.Z; aa.W = b.W; return aa; }
        public static Vector4 yxwz(this Vector4 a, Vector4 b) { var aa = a; aa.Y = b.X; aa.X = b.Y; aa.W = b.Z; aa.Z = b.W; return aa; }
        public static Vector4 zxyw(this Vector4 a, Vector4 b) { var aa = a; aa.Z = b.X; aa.X = b.Y; aa.Y = b.Z; aa.W = b.W; return aa; }
        public static Vector4 wxyz(this Vector4 a, Vector4 b) { var aa = a; aa.W = b.X; aa.X = b.Y; aa.Y = b.Z; aa.Z = b.W; return aa; }
        public static Vector4 zxwy(this Vector4 a, Vector4 b) { var aa = a; aa.Z = b.X; aa.X = b.Y; aa.W = b.Z; aa.Y = b.W; return aa; }
        public static Vector4 wxzy(this Vector4 a, Vector4 b) { var aa = a; aa.W = b.X; aa.X = b.Y; aa.Z = b.Z; aa.Y = b.W; return aa; }
        public static Vector4 yzxw(this Vector4 a, Vector4 b) { var aa = a; aa.Y = b.X; aa.Z = b.Y; aa.X = b.Z; aa.W = b.W; return aa; }
        public static Vector4 ywxz(this Vector4 a, Vector4 b) { var aa = a; aa.Y = b.X; aa.W = b.Y; aa.X = b.Z; aa.Z = b.W; return aa; }
        public static Vector4 zyxw(this Vector4 a, Vector4 b) { var aa = a; aa.Z = b.X; aa.Y = b.Y; aa.X = b.Z; aa.W = b.W; return aa; }
        public static Vector4 wyxz(this Vector4 a, Vector4 b) { var aa = a; aa.W = b.X; aa.Y = b.Y; aa.X = b.Z; aa.Z = b.W; return aa; }
        public static Vector4 zwxy(this Vector4 a, Vector4 b) { var aa = a; aa.Z = b.X; aa.W = b.Y; aa.X = b.Z; aa.Y = b.W; return aa; }
        public static Vector4 wzxy(this Vector4 a, Vector4 b) { var aa = a; aa.W = b.X; aa.Z = b.Y; aa.X = b.Z; aa.Y = b.W; return aa; }
        public static Vector4 yzwx(this Vector4 a, Vector4 b) { var aa = a; aa.Y = b.X; aa.Z = b.Y; aa.W = b.Z; aa.X = b.W; return aa; }
        public static Vector4 ywzx(this Vector4 a, Vector4 b) { var aa = a; aa.Y = b.X; aa.W = b.Y; aa.Z = b.Z; aa.X = b.W; return aa; }
        public static Vector4 zywx(this Vector4 a, Vector4 b) { var aa = a; aa.Z = b.X; aa.Y = b.Y; aa.W = b.Z; aa.X = b.W; return aa; }
        public static Vector4 wyzx(this Vector4 a, Vector4 b) { var aa = a; aa.W = b.X; aa.Y = b.Y; aa.Z = b.Z; aa.X = b.W; return aa; }
        public static Vector4 zwyx(this Vector4 a, Vector4 b) { var aa = a; aa.Z = b.X; aa.W = b.Y; aa.Y = b.Z; aa.X = b.W; return aa; }
        public static Vector4 wzyx(this Vector4 a, Vector4 b) { var aa = a; aa.W = b.X; aa.Z = b.Y; aa.Y = b.Z; aa.X = b.W; return aa; }
        #endregion
        #endregion

    }
}
#pragma warning restore CS1591
