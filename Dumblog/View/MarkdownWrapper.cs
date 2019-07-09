using Markdig;

namespace Dumblog.View
{
    public class MarkdownWrapper
    {
        private MarkdownPipeline _pipeline;

        public MarkdownWrapper()
        {
            _pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .Build();
        }

        public string GetHtml(string markdown)
        {
            return Markdown.ToHtml(markdown, _pipeline);
        }
    }
}
