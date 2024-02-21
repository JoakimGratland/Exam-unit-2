
using HTTPUtils;
using System.Text.Json;
using AnsiTools;
using Colors = AnsiTools.ANSICodes.Colors;
using Task;
using romanNumerals;

Console.Clear();
Console.WriteLine("Starting Assignment 2");

// SETUP 
const string myPersonalID = "9d901a5a9e148d764b5b9dd5377cd75929f665d37a7d012b214f512cfe3c42de"; // GET YOUR PERSONAL ID FROM THE ASSIGNMENT PAGE https://mm-203-module-2-server.onrender.com/
const string baseURL = "https://mm-203-module-2-server.onrender.com/";
const string startEndpoint = "start/"; // baseURl + startEndpoint + myPersonalID
const string taskEndpoint = "task/";   // baseURl + taskEndpoint + myPersonalID + "/" + taskID

// Creating a variable for the HttpUtils so that we dont have to type HttpUtils.instance every time we want to use it
HttpUtils httpUtils = HttpUtils.instance;

//#### REGISTRATION
// We start by registering and getting the first task
Response startRespons = await httpUtils.Get(baseURL + startEndpoint + myPersonalID);
Console.WriteLine($"Start:\n{Colors.Magenta}{startRespons}{ANSICodes.Reset}\n\n"); // Print the response from the server to the console
// We get the taskID from the previous response and use it to get the task (look at the console output to find the taskID)

//#### FIRST TASK 
// Fetch the details of the task from the server.
Task.InfoFomAPI task1 = new Task.InfoFomAPI();
task1.taskID = "otYK2";
Response task1Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + task1.taskID); // Get the task from the server
task1 = JsonSerializer.Deserialize<Task.InfoFomAPI>(task1Response.content);


Console.WriteLine($"Task 1: {task1.title}");
Console.WriteLine($"{task1.description}");
Console.WriteLine($"Parameters: {task1.parameters}");


string[] words = task1.parameters.Split(",");
string[] uniqueWords = words.Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(word => word).ToArray();
string resultTask1 = string.Join(",", uniqueWords);


Console.WriteLine($"Result: {resultTask1}");
Response task1AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + task1.taskID, resultTask1);
Console.WriteLine($"Response: {task1AnswerResponse.content}");

//#### SECOND TASK
Task.InfoFomAPI task2 = new Task.InfoFomAPI();
task2.taskID = "aLp96";
Response task2Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + task2.taskID); 
task2 = JsonSerializer.Deserialize<Task.InfoFomAPI>(task2Response.content);

Console.WriteLine($"Task 2: {task2.title}");
Console.WriteLine($"{task2.description}");
Console.WriteLine($"Parameters: {task2.parameters}");

int number = int.Parse(task2.parameters);
string oddOrEven(int number)
{
    if (number % 2 == 0)
    {
        return "even";
    }
    else
    {
        return "odd";
    }
}

string resultTask2 = oddOrEven(number);

Console.WriteLine($"Result: {resultTask2}");
Response task2AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + task2.taskID, resultTask2);
Console.WriteLine($"Response: {task2AnswerResponse.content}");

//#### THIRD TASK
Task.InfoFomAPI task3 = new Task.InfoFomAPI();
task3.taskID = "KO1pD3";
Response task3Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + task3.taskID);
task3 = JsonSerializer.Deserialize<Task.InfoFomAPI>(task3Response.content);

Console.WriteLine($"Task 3: {task3.title}");
Console.WriteLine($"{task3.description}");
Console.WriteLine($"Parameters: {task3.parameters}");

string[] numbers = task3.parameters.Split(",");
int[] numbersArray = Array.ConvertAll(numbers, int.Parse);
int difference = numbersArray[1] - numbersArray[0];

for (int i = 2; i < numbersArray.Length; i++)
{
    if (numbersArray[i] - numbersArray[i - 1] != difference)
    {
        difference = numbersArray[i] - numbersArray[i - 1];
        break;
    }
}

int nextNumber = numbersArray[^1] + difference;
string resultTask3 = nextNumber.ToString();

Console.WriteLine($"Result: {resultTask3}");
Response task3AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + task3.taskID, resultTask3);
Console.WriteLine($"Response: {task3AnswerResponse.content}");

//#### FOURTH TASK
Task.InfoFomAPI task4 = new Task.InfoFomAPI();
task4.taskID = "rEu25ZX";
Response task4Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + task4.taskID);
task4 = JsonSerializer.Deserialize<Task.InfoFomAPI>(task4Response.content);
romanNumberTranslator romanNumerals = new romanNumberTranslator();

Console.WriteLine($"Task 4: {task4.title}");
Console.WriteLine($"{task4.description}");
Console.WriteLine($"Parameters: {task4.parameters}");

int numberTask4 = 0;

for (int i = 0; i < task4.parameters.Length; i++)
{
    if (i == 0 || romanNumerals.RomanNumbersTranslated[task4.parameters[i]] <= romanNumerals.RomanNumbersTranslated[task4.parameters[i - 1]])
    {
        numberTask4 += romanNumerals.RomanNumbersTranslated[task4.parameters[i]];
    }
    else
    {
        numberTask4 += romanNumerals.RomanNumbersTranslated[task4.parameters[i]] - 2 * romanNumerals.RomanNumbersTranslated[task4.parameters[i - 1]];
    }
}

string resultTask4 = numberTask4.ToString();

Console.WriteLine($"Result: {resultTask4}");
Response task4AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + task4.taskID, resultTask4);
Console.WriteLine($"Response: {task4AnswerResponse.content}");
