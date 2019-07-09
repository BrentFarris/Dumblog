using System;
using System.IO;

namespace Dumblog.View
{
    public class PageLoader
    {
        private const string MARKDOWN_REPLACE_TEXT = "MARKDOWN_REPLACE";
        private readonly string _template;
        MarkdownWrapper _markdown = new MarkdownWrapper();

        public PageLoader()
        {
            _template = File.ReadAllText("Content/template.html");
        }

        public string LoadFile(string path)
        {
            path = GetSanitizedPath(path);
            if (!string.IsNullOrEmpty(path))
                return TryLoadMarkdownAtPath(path);
            return string.Empty;
        }

        private string GetSanitizedPath(string path)
        {
            path = path.Replace("\\", "/");
            if (string.IsNullOrEmpty(path))
                path = "index";
            else if (path.StartsWith("..") || Path.IsPathRooted(path))
                throw new System.Exception();   // TODO:  Throw correct exception
            else if (path.EndsWith(".ico"))
                path = string.Empty;
            return path;
        }

        private string TryLoadMarkdownAtPath(string path)
        {
            Console.WriteLine($"Requesting: {path}");
            string fullPath = $"../../../Content/Pages/{path.ToLower()}.md";
            if (!File.Exists(fullPath))
                return string.Empty;
            string contents = File.ReadAllText(fullPath);
            return GetReplacementText(contents);
        }

        private string GetReplacementText(string contents)
        {
            return _template.Replace(MARKDOWN_REPLACE_TEXT, _markdown.GetHtml(contents));
        }
    }
}