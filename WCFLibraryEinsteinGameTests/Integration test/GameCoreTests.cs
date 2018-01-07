using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCFLibraryEinsteinGame.Application;
using WCFLibraryEinsteinGame.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFLibraryEinsteinGame.Application.Tests
{
    [TestClass()]
    public class GameCoreTests
    {
        [TestMethod()]
        public void ExecuteCheatResultTest1()
        {
            String AssertNumber = "1";

            GameCore Game = new GameCore();

            String result = Game.ExecuteCheat(1);

            Assert.AreEqual(AssertNumber,result);
        }


        [TestMethod()]
        public void ExecuteCheatResultTest2()
        {
            String ExpectedResult = "Fizz";

            GameCore Game = new GameCore();

            String result = Game.ExecuteCheat(3333);

            Assert.AreEqual(ExpectedResult, result);
        }


        [TestMethod()]
        public void ExecuteCheatResultTest3()
        {
            String ExpectedResult = "Buzz";

            GameCore Game = new GameCore();

            String result = Game.ExecuteCheat(5555);

            Assert.AreEqual(ExpectedResult, result);
        }


        [TestMethod()]
        public void ExecuteCheatResultTest4()
        {
            String ExpectedResult = "FizzBuzz";

            GameCore Game = new GameCore();

            String result = Game.ExecuteCheat(10125);

            Assert.AreEqual(ExpectedResult, result);
        }


        [TestMethod()]
        public void GenerateListTest()
        {
            int max = 150;

            GameCore Game = new GameCore();

            List<int> example = Game.GenerateList(0);

            Assert.AreEqual(150, example.Capacity);
        }


        [TestMethod()]
        [ExpectedException(typeof(EinsteinGameExceptions))]
        public void GenerateListExceptionTest()
        {
            int max = 151;

            GameCore Game = new GameCore();

            List<int> example = Game.GenerateList(200);
            
            //Don't need assert because we expect an exception
        }

    }
}