using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCFLibraryEinsteinGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WCFLibraryEinsteinGame.Application;

namespace WCFLibraryEinsteinGame.Tests
{
    [TestClass()]
    public class EinsteinGameServiceTests
    {
        [TestMethod()]
        public void RunGameTest()
        {
            EinsteinGameService EinsteinGame = new EinsteinGameService();
            List<String> result = new List<String> { "149" };
            // We declare de mocks
            var GameMock = new Mock<IGameCore>();
            var FileMock = new Mock<IFileManager>();

            // Now we set the Mock return
            GameMock.Setup(x => x.ExecuteCheat(149)).Returns("149");
            GameMock.Setup(x => x.GenerateList(149)).Returns(new List<int>{149});
            FileMock.Setup(y => y.getList()).Returns(new List<String> { "149" });

            // Now we start the game

            EinsteinGame.SetParameters(GameMock.Object, FileMock.Object);

            Assert.AreEqual(result[0], EinsteinGame.RunGame(149).OutPut[0]);
        }
    }
}