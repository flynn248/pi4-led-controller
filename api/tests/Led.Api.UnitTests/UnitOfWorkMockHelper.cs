using Led.SharedKernal.UoW;
using NSubstitute;

namespace Led.Api.UnitTests;

internal static class UnitOfWorkMockHelper
{
    public static IUnitOfWorkManager GetUnitOfWorkManagerMock(out IUnitOfWork unitOfWorkMock)
    {
        unitOfWorkMock = Substitute.For<IUnitOfWork>();

        var unitOfWorkManagerMock = Substitute.For<IUnitOfWorkManager>();
        unitOfWorkManagerMock.Begin().Returns(unitOfWorkMock);

        return unitOfWorkManagerMock;
    }
}
