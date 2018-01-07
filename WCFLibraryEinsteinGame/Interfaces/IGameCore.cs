using System;
using System.Collections.Generic;

namespace WCFLibraryEinsteinGame.Application
{
    public interface IGameCore
    {

       String ExecuteCheat(int number);

       List<int> GenerateList(int ini);
    



    }
}