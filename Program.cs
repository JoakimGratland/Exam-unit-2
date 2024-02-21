
using HTTPUtils;
using System.Text.Json;
using AnsiTools;
using Colors = AnsiTools.ANSICodes.Colors;
using Task;


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
string result = string.Join(",", uniqueWords);


Console.WriteLine($"Result: {result}");
Response task1AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + task1.taskID, result);
Console.WriteLine($"Response: {task1AnswerResponse.content}");



