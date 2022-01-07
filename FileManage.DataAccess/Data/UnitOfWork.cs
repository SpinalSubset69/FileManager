using FileManage.DataAccess.DbAccess;
using FileManage.DataAccess.Interfaces;
using FileManage.DataAccess.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManage.DataAccess.Data;

public class UnitOfWork: IUnitOfWork
{
    private readonly ISqlDataAccess _db;
    public UnitOfWork(ISqlDataAccess db)
    {
        _db = db;
        Users = new UserData(_db);
        Folders = new FolderData(_db);
    }

    public IUserRepository Users {get; private set;}

    public IFolderRepository Folders { get; private set; }
}
