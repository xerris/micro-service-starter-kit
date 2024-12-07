using Moq;

namespace Tests.Services;

public abstract  class BaseMockTest : IDisposable
{
    private readonly MockRepository mocks = new(MockBehavior.Strict);
    protected Mock<T> CreateMock<T>() where T : class => mocks.Create<T>();
    protected Mock<T> CreateStub<T>() where T : class => mocks.Create<T>(MockBehavior.Loose);
    public void Dispose() => mocks.VerifyAll();
}