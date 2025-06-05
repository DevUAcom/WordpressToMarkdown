# WordpressToMarkdown
Converts WordPress blog posts to Markdown for a static site generator 
like [Astro](https://astro.build/).

This project is intended for .NET developers who want to migrate blog posts
from WordPress to a static site generator like Astro, Hugo, Gatsby, etc.

Since each WordPress site has its own settings and plugins, it is hard to create
a universal converter that satisfies everyone. Instead, I've created a converter
for my own needs. Other .NET developers can modify it to suit their requirements.

# How to run

This converter is a console application written in C#. To run it on your computer 
clone the repository and open it in your preferred IDE.

Then rename (or copy) `appsettings.example.json` file into `appsettings.json` and open it in
an editor. Update settings according to your configuration.

| Setting Name | Descripton                                                                                                                                            |
|--------------|-------------------------------------------------------------------------------------------------------------------------------------------------------|
| DefaultConnection | Connection string for connecting to the MySql Wordpress database                                                                                      |
| Prefix | Tables prefix in the Wordpress database (wp by default)                                                                                               |
| OutputDirectory | Path to the directory where markdown files will be created                                                                                            |
| FeatureImagesLocation | Path to the directory where feature images are located. This path should be relative from the location of markdown files in Astro blog file structure |

Run it from the IDE or from a console:
```PowerShell
dotnet restore
dotnet run
```

# Dependencies

This converter uses third party libraries:
- [ReverseMarkdown](https://github.com/mysticmind/reversemarkdown-net) to convert html to markdown.
- [scriban](https://github.com/scriban/scriban) as template engine to generate a resulting 
markdown files with header properties.

# Program structure

App.cs:
```csharp
    public async Task Run(CancellationToken cancellationToken)
    {
        var posts = dataProvider.GetPosts();

        int postCount = 0;
        await Parallel.ForEachAsync(posts, cancellationToken, async (post, ct) =>
        {
            await post
                .ConvertCodeBlock()
                .ConvertSingleLineBreaks()
                .ConvertDialogs()
                .RemoveMore()
                
                .ConvertToMarkdown()
                .AddAstroHeader(appSettings.FeatureImagesLocation)
                
                .ConvertToc()
                .ConvertBoxes()
                
                .SaveTo(appSettings.OutputDirectory, ct)
            ;
            postCount++;
        });
        Console.WriteLine($"{postCount} posts converted.");
    }
```

`dataProvider.GetPosts()` returns all posts from the DB. The `Post` is an immutable C# record.

Inside `ForEach` loop each post is passed into a static method for conversion. 
Each "convert" static method is located in a separate class (SRP and OCP).

## ConvertCodeBlock

Converts block like
```html
<pre class="lang:"csharp">
// some code
</pre>
```
into markdown code block.

## ConvertSingleLineBreaks

Converts single line breaks into "  \r\n" (with a leading space) to be displayed as a line break
in the markdown.

## ConvertDialogs

Converts dialog formating to markdown:

\- Hello! How are you?\
\- Great!

```markdown
\- Hello! How are you?
\- Great!
```

## RemoveMore

Deletes `<!--more-->` strings.

## ConvertToMarkdown

Uses ReverseMarkdown library to convert html into markdown.

## AddAstroHeader

Creates a markdown content with an Astro header. To provide header property values it uses 
the Scriban library.

## ConvertToc

Replaces `[toc]` with `<!-- toc -->`.

## ConvertBoxes

Converts `[box type="info"]Info[/box]` or `[stextbox id="info"]Info[/stextbox]` into
```markdown
:::info
Info
:::
```

## SaveTo

Saves a resulting content into a file.