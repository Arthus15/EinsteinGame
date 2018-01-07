# Einstein Game 
### Develop by Jairo Blanco Aldao 
### Made with Visual Studio 2017
--------------------------------------------------------------------------------------

## 1. Introduction

The main goal of this exercise is to create a WCF RestFull WebService solving the following:
Any number divisible by three is replaced by the word fizz and any divisible by five by the word buzz. Numbers divisible by both become
fizzbuzz.

In the following points I will describe every different proyect that is include in the repository, functionality and why I decide to do it 
in that way.

## 2. EinsteinGameClient

Due to time I decide to make an Application Client instead of a web client, although, it's not the same but it allows us to try the application
in a clearly way.

It's compose for a Program class wich initialize "EinsteinGameClient" that is essentially a Form with 2 labels, 2 textbox and 1 start botton.
On the background code of the form I decided to add a small input-type controll to make sure that the correct parameter is introduced.

## 3. WCFLibraryEinsteinGame

It contains the full logic of the applicatio and it's divide by folders(they act like layers):

- Application: Contains the classes that executes the game.
- Distributed Services: Contains the service call by the client.
- Domain: Contains the Dtos and Custom classes for error control.
- Interfaces: Contains the needed interfaces for Dependecy injection.

### 3.1 Application classes

#### 3.1.1 EinsteinGameService.cs

Probably it should be in another layer but I decide to put it here due to it's funtionality. It's the main class and the one who is
called for the client side, also, it contains the multhread calls:
```
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

                        throw e;


                    }
                    catch (Exception e)
                    {
                        
                        throw e;
                    }

                });
```
The reason why I decide to use a Parallel.For instead of a Parallel.ForEach is because with Parallel.For you can, in some way, control
the order but it's no efficient. Parallel.For controls that no more that the Maximum number of threads is execute at the same time wich
take this information from the App.Config.
You also can apreciate the Catch clausure wich can handle EinsteinGameException (Custom Exceptions) and also Exception that I decide
to control also because some Parse Error could occur and they need to be handle. Because we are in a loop this exception just catch them and rethrow to the external catch wich writes the exception into the Log file and then return a custom Error EinsteinGameDto.

#### 3.1.2 GameCore.cs

This class is on charge of the execution of the funtionalities of the game. It can generate the number list and it has the method 
in wich we execute the cheat. 
It complexity is not to interesting so I think there is nothing interesting to explain about the code.

#### 3.1.2 FileManager.cs

It's the class that handles all the funtionalities related with files. First time that is initialize it check if the file is created
and if it's not it creates.
It's important to see how it controls the write on the file:
```
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
 ```          
As I saw the result was an order list I decide to keep the order of execution of the differents threads with
"while (position != pos) Thread.Sleep(100);" wich it's absolutly non-efficient. With this line I also could control
that no more than one thread is accessing to the file. But I wanted to show how I control the multiaccesses normaly.
For this propose I use locks, wich blocks the threads in that state while another one is accessing to file.
Position is a property of the class wich allows me to have the access control, it has to be inside the lock because a
variable can't be accesses by multiple threads and the same time also control that no one is reading while is being writting.


### 3.2 Distributed Services

Contains the interface of the service.

### 3.3 Domain

#### 3.3.1 EinsteinGameDto.cs

This class is used like a JSON when the webservice is called. In this case as we haven't use a web client is not really needed but
I decide to keep it as return type.

#### 3.3.2 EinsteinGameExceptions.cs

Custom Exception class.

### 3.4 Interfaces.
Interfaces of the Application classes wich are needed for the DI.

## 4. WCFLibraryEinsteinGameTests

First I'd like to explain wich is the difference between Unit test and Integration test.

Unit Test: They are made when the application is not finished and they used Mock instances to try it. 
Integration Tests: It test the full application using real instances of the objects.

Second I couldn't spend more time doing the test so I made one of each type to show how I do the test.

## 4.1 Unit test

For this test I had to use DI to inyect the Mocks created for the test in the following way:

```
          // We declare de mocks
            var GameMock = new Mock<IGameCore>();
            var FileMock = new Mock<IFileManager>();         
```            
After add the desire result to the different function, Insert into de Main class:
```
 EinsteinGame.SetParameters(GameMock.Object, FileMock.Object);
 ```
 
### 4.2 Integration test
 
 I used the class GameCore for the test and I tested all the posible result of the methods including to get the correct
 exception when a wrong value is introduced.
 
## 5. Configure and run
 
 To configure the app go to the app.config in WCFLibraryEinsteinGame:
 ```
 //Last number of the list
 <add key="Limit" value="150" />
 //Maximum number of threas
 <add key="MaxNumThreads" value="100" />
 //Local Path where the output file is created
 <add key="FilePath" value="D:\prueba\text.txt" />
```
Also in the log4net.config you can change the file path where the log is created:
```
<file value="D:\prueba\MyTestAppender.txt" />
```
How to run the application? 
- Set EinsteinGameClient proyect as start proyect
- Press run
- Introduce a number and press the start botton


## 6. Comments

I think I almost get what was asked in the test but I think it wouldn't be posible to create an async method that also use multithreads
to write in the same file and return the example list. When you call multi thread is because you are not worried about the order of the 
executed task. In this case the main problen was the order in the text file and I think there are best differents solution but I was 
running out of time and I preffer to show a little of everything asked in the test than send it uncomplete. 


