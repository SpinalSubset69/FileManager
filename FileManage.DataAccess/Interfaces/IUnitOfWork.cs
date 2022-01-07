using FileManage.DataAccess.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManage.DataAccess.Interfaces;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    
    IFolderRepository Folders { get; }
}
