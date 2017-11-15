using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Markdown
{
	class Program
	{
		static void Main(string[] args)
		{
		    string input;
		    string output;
		    switch (args.Length)
		    {
		        case 1:
		            input = args[1];
		            var splitted = input.Split('.');
		            var path = string.Join("", splitted.Take(splitted.Length - 1));
		            output = path + ".html";
		            break;
                case 2:
                    input = args[1];
                    output = args[2];
                    break;
                default:
                    PrintUsage();
                    input = "text.md";
                    output = "text.html";
                    break;
		    }

		    var tags = new Dictionary<string, string>(); //Тут можно будет добавлять поддержку новых тегов
            //Можно будет делать не только словарем, но и методами а-ля md.add("md tag", "html tag")
            var md = new Md(tags);
		    var mdText = File.ReadAllText(input);
		    var result = md.RenderToHtml(mdText);
		    File.WriteAllText(output, result);
		}

	    private static void PrintUsage()
	    {
	        Console.WriteLine("Convert your md file to html. Usage:");
	        Console.WriteLine("\tMarkdown.exe inputPath [outputPath]");
	        Console.WriteLine("if no outputPath file will be saved to $(inputPath).html");
	    }
	}
}
