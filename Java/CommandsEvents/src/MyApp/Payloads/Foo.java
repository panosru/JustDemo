package MyApp.Payloads;

import MyLibrary.IPayload;

public final class Foo implements IPayload {
    private String Value;

    public Foo(String value) {
        this.Value = value;
    }

    public String Value() { return this.Value; }
}
