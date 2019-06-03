# Itage.FontIcon
A simplistic FontIcon control implementation for WPF/netcoreapp3.0.

This isn't actually a complete font icon library, it's rather a framework for building your own.

## Example usage

Let's start with a simple font, called `MyFont`, located in file `myfont.ttf` containing a simple glyph with `e900`.

## Adding package reference

`dotnet add package Itage.FontIcon`

### Embedding font into assebly.

Let's place font file into `Resources\Fonts\myfont.ttf` and set its type to Resource

```xml
  <ItemGroup>
    <None Remove="Resources\Fonts\myfont.ttf" />
    <Resource Include="Resources\Fonts\myfont.ttf" />
  </ItemGroup>
```

Now it will be embedded into your executable or dll.

### Making your own font icon

The icon control consists of two parts: 
* Glyph `enum`
* Control class

Now let's write some actual code:

```csharp
public enum MyFontIconType
{
    None = 0, // This is required
    MyGlyph = 0xe900, // This is the code of our unicode character we want to embed
}

public class MyFontIcon : BaseFontIcon<MyFontIconType>
{
    static MyFontIcon()
    {
        DefaultFontFamily = FontResourceHelper.MakeFontFamily(
            typeof(MyFontIcon).Assembly, "resources/fonts", "MyFont");
    }
    public MyFontIcon()
    {
        FontFamily = DefaultFontFamily;
    }
}

```

That's all!

Now you can use 
```xml
<ns:MyFontIcon Icon="MyGlyph" />

``` 
in your views,
