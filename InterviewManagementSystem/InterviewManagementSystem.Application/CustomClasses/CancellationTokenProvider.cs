namespace InterviewManagementSystem.Application.CustomClasses;

public static class CancellationTokenProvider
{

    private static readonly AsyncLocal<CancellationToken> _token = new();

    public static CancellationToken CancellationToken
    {
        get => _token.Value;
        set => _token.Value = value;
    }

}
