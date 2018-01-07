using System;
using System.Collections.Generic;

namespace WCFLibraryEinsteinGame.Application
{
    public interface IFileManager
    {
        void WriteToFile(String text, int pos);

        List<String> getList();

        void WriteEnd();


    }
}