using ConsoleApp.MenuCore;
using StoreDAL.Data;

namespace ConsoleApp.MenuBuilder;

public interface IMenuCreator
{
    Menu Create(StoreDbContext context);
}