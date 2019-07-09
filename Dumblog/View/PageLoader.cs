using Markdig;
using System;
using System.IO;

namespace Dumblog.View
{
    public class PageLoader
    {
        private const string MARKDOWN_REPLACE_TEXT = "MARKDOWN_REPLACE";
        private readonly string _template;
        MarkdownPipeline _pipeline;

        public PageLoader()
        {
            _template = File.ReadAllText("Content/template.html");
            _pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .Build();
        }

        public string LoadFile(string path)
        {
            path = path.Replace("\\", "/");

            if (string.IsNullOrEmpty(path))
                path = "index";
            else if (path.StartsWith("..") || Path.IsPathRooted(path))
                throw new System.Exception();   // TODO:  Throw correct exception
            else if (path.EndsWith(".ico"))
                return string.Empty;

            Console.WriteLine($"Requesting: {path}");
            string fullPath = $"../../../Content/Pages/{path.ToLower()}.md";
            if (!File.Exists(fullPath))
                return string.Empty;

            string contents = File.ReadAllText(fullPath);
            return GetReplacementText(contents);
        }

        private string GetReplacementText(string contents)
        {
            string converted = Markdown.ToHtml(contents, _pipeline);
            return _template.Replace(MARKDOWN_REPLACE_TEXT, converted);
        }
    }
}