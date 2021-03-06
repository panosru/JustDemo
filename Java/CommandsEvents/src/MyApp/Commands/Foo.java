package MyApp.Commands;

import MyLibrary.Command.Base;

public final class Foo extends Base<String>
{
    private final String Greeting;
    private final String Value;

    public Foo(String greeting)
    {
        this.Greeting = greeting;
        this.Value = "World";
    }

    public String Greeting()
    {
        return this.Greeting;
    }

    public String Value()
    {
        return this.Value;
    }
}
