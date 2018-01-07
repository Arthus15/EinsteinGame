using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;
using System.IO;
using WCFLibraryEinsteinGame.Domain;
namespace WCFLibraryEinsteinGame.Application
{
    public class FileManager:IFileManager
    {
        private Object thisLock = new Object();
        private string Filepath = @ConfigurationManager.AppSettings["FilePath"];
        // ensures the order of writing in the file
        private int position = 0;
        public FileManager()
        {
            try
            {
                // Delete the file if it exists.
                if (!File.Exists(Filepath))
                {
                    // Create the file.
                    File.Create(Filepath).Close();
                }
            }
            catch
            {

                throw new EinsteinGameExceptions("Error Creating File");

            }
            
            }


        /*
         
            Take a String a write in the file if it's free.

        */

        public void WriteToFile(String text, int pos)
        {
            //acts like a semaphore, not eficient but necessary to keep the order.
            while (position != pos) Thread.Sleep(100);
            try
            {
                lock (thisLock)
                {
                    using (FileStream file = new FileStream(Filepath, FileMode.Append, FileAccess.Write, FileShare.Read))
                    using (StreamWriter writer = new StreamWriter(file, Encoding.Unicode))
                    {
                        writer.Write(text.ToString() + ", ");
                        position++;
                    }
                }
            }
            catch(Exception e)
            {
                // We reThrow the exception 
                throw e;

            }

        }

        /*
         
            Return the file content as a list.

        */

        public List<String> getList()
        {
            List<String> listStrLineElements = new List<string>();
            try
            {
                lock (thisLock)
                {
                    var lastLine = File.ReadLines(Filepath).Last();

                    listStrLineElements = lastLine.Split(',').ToList();
                    listStrLineElements.Remove(" ");
                }
            }
            catch
            {

            }

            return listStrLineElements;
        }


        /*

           End file method.

       */

        public void WriteEnd()
        {

            try
            {
                lock (thisLock)
                {
                    using (FileStream file = new FileStream(Filepath, FileMode.Append, FileAccess.Write, FileShare.Read))
                    using (StreamWriter writer = new StreamWriter(file, Encoding.Unicode))
                    {
                        writer.Write(" ==> " + DateTime.Now + Environment.NewLine);
                        position++;
                    }
                }
            }
            catch (Exception e)
            {
                // We reThrow the exception 
                throw e;

            }

        }
    }
}
