using System;
using System.IO;
using Breakout;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Math;
using NUnit.Framework;

namespace GameTests {
    public class TestGame {
        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();
        }
    }
}