using System.Data;
using Core.DTOs;
using Core.DTOs.Task;
using Core.Interfaces.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly string _connectionString;

        public TaskRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<AppResponse> DeleteAsync(string id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("DeleteTask", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar, 450) { Value = id });

                    SqlParameter resultParam = new SqlParameter("@Result", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(resultParam);

                    await command.ExecuteNonQueryAsync();

                    var result = (int)resultParam.Value;

                    return AppResponse.Create(status: result);
                }
            }
        }

        public async Task<AppResponse<List<TaskGet>>> GetAllAsync()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SelectTasks", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar) { Value = DBNull.Value });

                    SqlParameter resultParam = new SqlParameter("@Result", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(resultParam);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        var tasks = new List<TaskGet>();

                        while (await reader.ReadAsync())
                        {
                            var task = new TaskGet
                            {
                                Id = reader["Id"].ToString(),
                                Title = reader["Title"].ToString(),
                                Description = reader["Description"].ToString(),
                                IsCompleted = (bool)reader["IsCompleted"],
                                DueDate = (DateTime)reader["DueDate"]
                            };

                            tasks.Add(task);
                        }
                        await reader.CloseAsync();

                        int result = (int)resultParam.Value;

                        return AppResponse<List<TaskGet>>.Create(status: result, data: tasks);
                    }
                }
            }
        }

        public async Task<AppResponse<TaskGet>> GetAsync(string id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SelectTasks", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar) { Value = id });

                    SqlParameter resultParam = new SqlParameter("@Result", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(resultParam);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        TaskGet? task = default;
                        if (await reader.ReadAsync())
                        {
                            task = new TaskGet
                            {
                                Id = reader["Id"].ToString(),
                                Title = reader["Title"].ToString(),
                                Description = reader["Description"].ToString(),
                                IsCompleted = (bool)reader["IsCompleted"],
                                DueDate = (DateTime)reader["DueDate"]
                            };
                        }
                        await reader.CloseAsync();

                        int result = (int)resultParam.Value;

                        return AppResponse<TaskGet>.Create(status: result, data: task);
                    }
                }
            }
        }

        public async Task<AppResponse<string>> InsertAsync(TaskCreateRepo task)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("InsertTask", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar, 450) { Value = task.Id });
                    command.Parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar, 100) { Value = task.Title });
                    command.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 500) { Value = task.Description });

                    SqlParameter resultParam = new SqlParameter("@Result", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(resultParam);

                    await command.ExecuteNonQueryAsync();

                    var result = (int)resultParam.Value;

                    return AppResponse<string>.Create(status: result, data: task.Id);
                }
            }
        }

        public async Task<AppResponse> UpdateAsync(TaskUpdateRepo task)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("UpdateTask", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar, 450) { Value = task.Id });
                    command.Parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar, 100) { Value = task.Title });
                    command.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 500) { Value = task.Description });
                    command.Parameters.Add(new SqlParameter("@IsCompleted", SqlDbType.Bit) { Value = task.IsCompleted });

                    SqlParameter resultParam = new SqlParameter("@Result", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(resultParam);

                    await command.ExecuteNonQueryAsync();

                    var result = (int)resultParam.Value;

                    return AppResponse.Create(status: result);
                }
            }
        }
    }
}
