using Domain;

namespace LogicInterface.Interfaces.IManagerLogic
{
    public interface IManagerLogic
    {
        IEnumerable<Manager> GetAll();
    }
}
