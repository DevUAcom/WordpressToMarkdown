namespace WordpressToMarkdown.Templates;

public static class MarkdownTemplate
{
    public const string Template = """
                                    ---
                                    title: '{{ title }}'
                                    slug: "{{ slug }}"
                                    id: "{{ id }}"
                                    description: '{{ description }}'
                                    date: {{ date }}
                                    image: "{{ image }}"
                                    category: ["{{ categories }}"]
                                    tag: [{{ tags }}] 
                                    showToc: {{ toc }}
                                    ---

                                    {{ content }}
                                    """;
}