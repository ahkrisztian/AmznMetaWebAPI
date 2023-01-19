using AmznMetaLibrary.Models.OpinionModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmznMetaLibrary.Repo.Data
{
    public class SqliteDbContext : IDataRepo
    {
        private readonly ISqliteDataAccess dataAccess;

        public SqliteDbContext(ISqliteDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public async Task<Badop> CreateBadOp(Badop badop)
        {
            var model = await dataAccess.LoadData<Badop, dynamic>("INSERT INTO BadTags (Name) VALUES (@Name); SELECT * FROM BadTags WHERE Id = last_insert_rowid()", new { Id = badop.Id, Name = badop.Name }, "Default");

            return model.FirstOrDefault();
        }

        public async Task<GoodOp> CreateGoodOp(GoodOp goodOp)
        {
            var model = await dataAccess.LoadData<GoodOp, dynamic>("INSERT INTO GoodTags (Name) VALUES (@Name); SELECT * FROM GoodTags WHERE Id = last_insert_rowid()", new {Id = goodOp.Id, Name = goodOp.Name }, "Default");

            return model.FirstOrDefault();
        }

        public Task<List<Badop>> GetallBadOps()
        {
            return dataAccess.LoadData<Badop, dynamic>("select * from BadTags", new { }, "Default");
        }

        public Task<List<GoodOp>> GetallGoodOps()
        {

            return dataAccess.LoadData<GoodOp, dynamic>("select * from GoodTags", new { }, "Default");
        }

        public async Task<Badop> GetBadOpById(int id)
        {
            var output =  await dataAccess.LoadData<Badop, dynamic>("select * from BadTags where Id = @Id", new {Id = id}, "Default");

            return output.FirstOrDefault();
        }

        public async Task<GoodOp> GetGoodOpById(int id)
        {
            var output = await dataAccess.LoadData<GoodOp, dynamic>("select * from Goodtags where Id = @Id", new { Id = id }, "Default");

            return output.FirstOrDefault();
        }
    }
}
