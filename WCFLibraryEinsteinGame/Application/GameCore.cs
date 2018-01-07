using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFLibraryEinsteinGame.Domain;

namespace WCFLibraryEinsteinGame.Application
{
    public class GameCore:IGameCore
    {

        /*Execute the Fizz-Buzz game.
         
          Input:
          
            numer -> Number introduce by the user.

          Output:
          
            Result -> The result of the operation(fizz,buzz,number).
             
        */
        public String ExecuteCheat(int number)
        {            
            String result = String.Empty;

            //If number is 0 we return 
            if (number == 0) return number.ToString(); 

            //We check the divisibilty of the number and we save the result.
            Boolean fizz = number % Constants.ciTHREE == 0 ;
            Boolean buzz = number % Constants.ciFIVE == 0;

            //Depending of the result of fizz and buzz we write on the variable result.
            if (fizz && buzz)
                result = Constants.csFIZZBUZZ;
            else if (fizz)
                result = Constants.csFIZZ;
            else if (buzz)
                result = Constants.csBUZZ;
            else
                result = number.ToString();            

            return result;
        }

        /*Generates de number game list.
         
          Input:
          
            ini -> Initial number.

          Output:
          
            gameList -> Number list wich starts in "ini" and ends in the number defined on the configuration.
             
        */

        public List<int> GenerateList(int ini)
        {
            try
            {
                int endNumber = Int32.Parse(ConfigurationManager.AppSettings["limit"]);

                if (ini > endNumber) throw new EinsteinGameExceptions("The number introduced is bigger than the maximum");

                List<int> gameList = new List<int>(endNumber - ini);
                //To save memory we create the list with the exact lenght.
                gameList = Enumerable.Range(ini, endNumber - ini).ToList();
                gameList.Capacity = endNumber - ini;
                return gameList;
            }
            catch(EinsteinGameExceptions e)
            {

                // We rethrow the exception
                throw e;
                

            }
        }

    }
}
