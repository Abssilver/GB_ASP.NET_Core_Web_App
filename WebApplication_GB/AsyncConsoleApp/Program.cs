using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AsyncConsoleApp
{
    class Program
    {
        private static HttpClient _client;
        private static readonly Uri Uri = new ("https://jsonplaceholder.typicode.com/posts/");
        private const string FilePath = "result.txt";
        private const int StartNumberOfPost = 4;
        private const int NumOfPosts = 10;

        private static readonly CancellationToken CancellationToken = new();

        private static async Task Main()
        {
            var tasks = await DownloadTasksAsync();
            var parsedData = HandleResult(tasks);
            await WriteDataToFile(parsedData, FilePath);
            Console.ReadKey();
        }

        private static async Task<Task<string>[]> DownloadTasksAsync()
        {
            using (_client = new HttpClient())
            {
                var tasks = GetPostTasks(NumOfPosts, StartNumberOfPost, Uri, CancellationToken);

                try
                {
                    await Task.WhenAll(tasks);
                    Console.WriteLine("Downloading is completed");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nDownload Exception!");
                    Console.WriteLine($"Message :{ex.Message}");
                }
                
                return tasks;
            }
        }

        private static Task<string>[] GetPostTasks(
            int numOfTasks, 
            int startPost, 
            Uri baseUri, 
            CancellationToken cancellationToken) =>
            Enumerable
                .Range(startPost, numOfTasks)
                .Select(i => new Uri(baseUri, i.ToString()))
                .Select(uri => _client.GetStringAsync(uri, cancellationToken))
                .ToArray();

        
        private static List<JsonPostData> HandleResult(Task<string>[] tasks)
        {
            var data = new List<JsonPostData>();

            try
            {
                foreach (var task in tasks)
                {
                    if (task.IsCompleted)
                    {
                        var result = JsonConvert.DeserializeObject<JsonPostData>(task.Result);
                        data.Add(result);
                    }
                }
                Console.WriteLine("Parsing is completed");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nParsing Exception!");
                Console.WriteLine($"Message :{ex.Message}");
            }

            return data;
        }
        
        private static async Task WriteDataToFile(List<JsonPostData> data, string filePath)
        {
            try
            {
                await using var writer = new StreamWriter(File.Open(filePath, FileMode.Create));
                foreach (var postData in data)
                {
                    await writer.WriteLineAsync(postData.UserId.ToString());
                    await writer.WriteLineAsync(postData.Id.ToString());
                    await writer.WriteLineAsync(postData.Title);
                    await writer.WriteLineAsync(postData.Body);
                    await writer.WriteLineAsync();
                }
                Console.WriteLine("Writing is completed");
                Console.WriteLine($"Data was written to file \"{FilePath}\"");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nWriting Exception!");
                Console.WriteLine($"Message :{ex.Message}");
            }
        }
        
        private class JsonPostData
        {
            public int UserId { get; set; }
            public int Id { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }
        }
    }
}