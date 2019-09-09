using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Dumblog.View
{
    public class PageLoader
    {
        private const int NEW_UPDATES_PAGES_COUNT = 10;
        private const string MARKDOWN_REPLACE_TEXT = "MARKDOWN_REPLACE";
        private const string RECENT_LIST_REPLACE_TEXT = "RECENT_LIST";
        private readonly string _template = string.Empty;
        private string _indexRecents = string.Empty;
        MarkdownWrapper _markdown = new MarkdownWrapper();

        public PageLoader()
        {
            _template = File.ReadAllText("Content/template.html");
            RefreshRecentList();
        }

        public string LoadFile(string path)
        {
            path = GetSanitizedPath(path);
            if (string.IsNullOrEmpty(path))
                return string.Empty;
            else if (path == "github-pull")
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
                process.EnableRaisingEvents = true;
                process.Start();
                process.Exited += GitPullComplete;
            }
            catch
            {
                Console.WriteLine("Failed to start the git process for updating");
            }
        }

        private void GitPullComplete(object sender, EventArgs e)
        {
            RefreshRecentList();
        }

        private void RefreshRecentList()
        {
            var files = new DirectoryInfo("Content/Pages")
                .GetFiles("**.*md", SearchOption.AllDirectories)
                .OrderByDescending(f => f.LastWriteTime)
                .Take(NEW_UPDATES_PAGES_COUNT)
                .Select(f => Path.GetRelativePath("Content/Pages", f.FullName).Replace(".md", ""))
                .ToList();
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < files.Count; i++)
            {
                if (files[i].EndsWith("index"))
                    continue;
                DateTime dt = File.GetCreationTime($"Content/Pages/{files[i]}.md");
                string displayName = textInfo.ToTitleCase(files[i].Replace('-', ' ')).Replace(".Md", "");
                builder.AppendLine($"{dt.ToString("MMM dd, yyyy")} >>{{style=font-size:12px;}} **[{displayName}](/{files[i]})**{{style=font-size:18px;}}\n");
            }
            _indexRecents = builder.ToString();
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
            string fullPath;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                fullPath = $"../../../Content/Pages/{path.ToLower()}.md";
            else
                fullPath = $"Content/Pages/{path.ToLower()}.md";
            if (!File.Exists(fullPath))
                return string.Empty;
            string contents = File.ReadAllText(fullPath);
            if (path == "index")
                contents = contents.Replace(RECENT_LIST_REPLACE_TEXT, _indexRecents);
            return GetReplacementText(contents);
        }

        private string GetReplacementText(string contents)
        {
            return _template.Replace(MARKDOWN_REPLACE_TEXT, _markdown.GetHtml(contents));
        }
    }
}