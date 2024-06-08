using BusinessLogic.Logics;
using Domain;
using IDataAccess;
using Moq;

namespace BusinessLogicTest;

[TestClass]
public class ManagerLogicTest
{
    private Mock<IManagerRepository> _managerRepositoryMock;
    private ManagerLogic _managerLogic;

    [TestInitialize]
    public void TestSetup()
    {
        _managerRepositoryMock = new Mock<IManagerRepository>(MockBehavior.Strict);
        _managerLogic = new ManagerLogic(_managerRepositoryMock.Object);
    }

    [TestMethod]
    public void GetAll_Succes()
    {
        _managerRepositoryMock.Setup(m => m.GetAll()).Returns(new List<Manager>());

        IEnumerable<Manager> response = _managerLogic.GetAll();

        Assert.IsNotNull(response);
        Assert.AreEqual(0, response.Count());

        _managerRepositoryMock.VerifyAll();
    }

    [TestMethod]
    public void GetAll_Fail()
    {
        _managerRepositoryMock.Setup(m => m.GetAll()).Returns((IEnumerable<Manager>)null);

        Exception exception = null;

        try
        {
            _managerLogic.GetAll();
        }
        catch (Exception e)
        {
            exception = e;
        }

        Assert.IsNotNull(exception);
        Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));

        _managerRepositoryMock.VerifyAll();
    }
}