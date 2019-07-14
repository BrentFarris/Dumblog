using System;
using System.Diagnostics;
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
            if (string.IsNullOrEmpty(path))
                return string.Empty;
            if (path == "github-pull")
                PullNewFilesFromGitHub();
            return TryLoadMarkdownAtPath(path);
        }

        private void PullNewFilesFromGitHub()
        {
            try
            {
                var process = new Process()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "/bin/bash",
                        Arguments = $"-c \"git pull\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };
                process.Start();
            }
            catch
            {
                Console.WriteLine("Failed to start the git process for updating");
            }
        }

        private string GetSanitizedPath(string path)
        {

            path = path.Replace("\\", "/");
            if (path.StartsWith("/"))
                path = path.Substring(1, path.Length - 1);
            if (string.IsNullOrEmpty(path))
                path = "index";
            else if (path.StartsWith("..") || Path.IsPathRooted(path))
                throw new Exception();   // TODO:  Throw correct exception
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