using mersetaWebAPI.CustomModels.Task;
using mersetaWebAPI.Models;
using mersetaWebAPI.Wrappers;

namespace mersetaWebAPI.Interface
{
    public interface ITasks
    {
        public Task<Response<string>> CreateTaskForBacthModeration(TaskResponse taskResponse);
        public Task<Response<List<Models.Task>>> GetAssessmentTask();
        public Task<Response<string>> UpdateTaskStatus(long taskid, int status);
        public Task<Response<string>> CreateTaskUser(TaskUser taskResponse);
        public Task<Response<List<TaskUser>>> GetTaskUser(long userid);
        public Task<Response<List<Models.Task>>> GetuserTasks(long userid);
        public Task<Response<Models.Task>> GetSingleTasks(long taskid);

    }
}
