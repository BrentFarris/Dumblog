using System.IO;

namespace Dumblog.View
{
    /// <summary>
    /// Feedback Page Code
    /// </summary>
    public class FeedbackLoader
    {
        public static bool IsFeedback(string path)
        {
            return path.Equals("/feedback.html");
        }

        public static string Get()
        {
            return File.ReadAllText("Content/feedback.html");
        }
    }
}