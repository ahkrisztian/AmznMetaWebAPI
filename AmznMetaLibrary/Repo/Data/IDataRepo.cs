using AmznMetaLibrary.Models.OpinionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmznMetaLibrary.Repo.Data
{
    public interface IDataRepo
    {
        Task<List<GoodOp>> GetallGoodOps();
        Task<List<Badop>> GetallBadOps();

        Task<Badop> CreateBadOp(Badop badop);
        Task<GoodOp> CreateGoodOp(GoodOp goodOp);

        Task<Badop> GetBadOpById(int id);
        Task<GoodOp> GetGoodOpById(int id);
    }
}
