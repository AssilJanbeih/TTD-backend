namespace Domain.Constants;

public static class Validation
{
    public static class Regex
    {
        public const string PASSWORD =
            "(?=^.{6,20}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$";
    }
}