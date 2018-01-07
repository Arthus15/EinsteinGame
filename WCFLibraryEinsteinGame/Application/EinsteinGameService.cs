using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using WCFLibraryEinsteinGame.Application;
using log4net;
using WCFLibraryEinsteinGame.Domain;

namespace WCFLibraryEinsteinGame
{
    public class EinsteinGameService : IEinsteinGameService
    {
        //Game will execute the logic of the game and give the result
        private IGameCore Game = new GameCore();
        private IFileManager FileManager = new FileManager();

        //Declare an instance for log4net
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public EinsteinGameDto RunGame(int cheat)
        {

            try
            {

                Log.Info("Runinng Game...");

                //Generate the game number list.
                List<int> gameList = Game.GenerateList(cheat);

                //Generates the outputList
                List<String> outList = new List<string>(gameList.Capacity);

                String result = String.Empty;

                //Gets the MaxNumThreads from app.config
                int maxNumThreads = Int32.Parse(ConfigurationManager.AppSettings["MaxNumThreads"]);

                var options = new ParallelOptions { MaxDegreeOfParallelism = maxNumThreads };

                Parallel.For(0, gameList.Capacity, options, number =>
                {
                    try
                    {
                        // We start the execution of the game
                        result = Game.ExecuteCheat(gameList[number]);

                        //Write the result in the file
                        FileManager.WriteToFile(result, number);

                    }
                    catch (EinsteinGameExceptions e)
                    {

                        //We write the exception on the log file
                        Log.Error("An error occur during the excution: " + e.Message);
                        Log.Info("Game finished with errors");


                    }
                    catch (Exception e)
                    {
                        //We write the exception on the log file
                        Log.Error(e);
                        Log.Info("Game finished with errors");
                    }

                });

            }
            catch (EinsteinGameExceptions e)
            {
                Log.Error("An error occur during the excution: " + e.Message);
                Log.Info("Game finished with errors");
                //Return a error list TODO ERROR LIST
                return new EinsteinGameDto(FileManager.getList());
            }
            catch (Exception e)
            {
                //We write the exception on the log file
                Log.Error(e);
                Log.Info("Game finished with errors");
            }
            EinsteinGameDto GameResult = new EinsteinGameDto(FileManager.getList());

            // Write the date at the end of the file
            FileManager.WriteEnd();

            Log.Info("Game finished sucsesfully");
            return GameResult;
        }

        /*
         Setter for dependency injection
             */

        public void SetParameters(IGameCore Game, IFileManager FileManager)
        {

            this.Game = Game;

            this.FileManager = FileManager;

        }

    }
}
