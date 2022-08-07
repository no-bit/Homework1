using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

using Npgsql;
using Newtonsoft.Json;

namespace CrudOperations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationPgAdminController : ControllerBase
    {
        NpgsqlConnection conn = new NpgsqlConnection(@"Server=localhost; Port=5432; User Id=postgres; Password=1234; Database=CrudOperations;");

        [HttpGet]
        public string getAll()
        {
            string query = @"select * from public.Location";
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn);
            DataTable _dataTable = new DataTable();
            adapter.Fill(_dataTable);
            if (_dataTable.Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(_dataTable);
            }
            else
            {
                return ("No entries found");
            }
        }
        [HttpPost]
        public async Task<string> Add(Location _location)
        {
            string query = @"insert into public.Location(Name,x,y) values(@Name,@x,@y)";
            await using (var cmd = new NpgsqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("Name", _location.Name);
                cmd.Parameters.AddWithValue("x", _location.x);
                cmd.Parameters.AddWithValue("y", _location.y);

                conn.Open();
                await cmd.ExecuteNonQueryAsync();
                conn.Close();

                return "Succesfull in adding";
            }
        }
        [HttpDelete]
        public async Task<string> Delete(int id)
        {
            string query = @"delete from public.Location where id=@Id";
            await using (var cmd = new NpgsqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("Id", id);
                conn.Open();
                await cmd.ExecuteNonQueryAsync();
                conn.Close();

                return "Succesfull in deleting";
            }
        }
        [HttpPut]
        public async Task<string> Update(Location _location, int Id)
        {
            string query = @"update public.Location set Name=@name, x=@x, y=@y where Id=@id";
            await using (var cmd = new NpgsqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("Id", Id);
                cmd.Parameters.AddWithValue("Name", _location.Name);
                cmd.Parameters.AddWithValue("x", _location.x);
                cmd.Parameters.AddWithValue("y", _location.y);
                conn.Open();
                await cmd.ExecuteNonQueryAsync();
                conn.Close();

                return "Succesfull in updating";
            }
        }
        [HttpGet("{id}")]
        public string get(int id)
        {
            string query = @"select * from public.Location where id="+id.ToString();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn);
            DataTable _dataTable = new DataTable();
            adapter.Fill(_dataTable);
            if (_dataTable.Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(_dataTable);
            }
            else
            {
                return ("No entries found");
            }
        }
    }
}
