﻿// See https://aka.ms/new-console-template for more information

using NLTDotNetCore.ConsoleApp;

Console.WriteLine("Hello, World!");

AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
// adoDotNetExample.Read();
// adoDotNetExample.Create("title", "author", "content");
// adoDotNetExample.Update(11,"test title", "test author", "test content");
// adoDotNetExample.Delete(11);
adoDotNetExample.Edit(12);
Console.ReadLine();