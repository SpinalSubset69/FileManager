using FileManage.DataAccess.DbAccess;
using FileManage.DataAccess.Interfaces.Repositories;
using FileManager.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManage.DataAccess.Data;

public class FolderData : GenericRepository<Folder>, IFolderRepository
{
    private readonly ISqlDataAccess _db;
    public FolderData(ISqlDataAccess db) : base(db)
    {
        _db = db;
    }
}
