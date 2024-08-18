using HotelSmartManagement.Common.Database.Repositories;
using HotelSmartManagement.EmployeeSelfService.MVVM.Models;

namespace HotelSmartManagement.Common.Database.Services
{
    public class JobService
    {
        private readonly UserRepository _userRepository;
        private readonly JobRepository _jobRepository;

        public JobService(UserRepository userRepository, JobRepository jobRepository)
        {
            // Repositories retrieved through DI.
            _userRepository = userRepository;
            _jobRepository = jobRepository;
        }

        public async Task<Guid?> NewJob(string jobTitle, string jobDescription, JobUrgencyLevel urgencyLevel, JobType taskType, Guid createdByUser) => await NewJob(jobTitle, jobDescription, urgencyLevel, taskType, createdByUser, null);
        public async Task<Guid?> NewJob(string jobTitle, string jobDescription, JobUrgencyLevel urgencyLevel, JobType taskType, Guid createdByUser, Guid? assignedToUser)
        {
            if (!await _userRepository.ContainsById(createdByUser) || (assignedToUser != null && !await _userRepository.ContainsById(assignedToUser.Value)))
            {
                // There isn't a user currently registered by that Id.
                // We shouldn't create a job with a link to a non-existent user.
                return null;
            }

            var newJob = new Job() { Title = jobTitle, Description = jobDescription, UrgencyLevel = urgencyLevel, TaskType = taskType, CreatedById = createdByUser, CreatedAtUtc = DateTime.UtcNow, Status = JobStatus.Pending, AssignedToId = assignedToUser };
            _jobRepository.Add(newJob);
            _jobRepository.Save();

            return newJob.UniqueId;
        }
        public async Task<Guid?> NewJob(string jobTitle, string jobDescription, JobUrgencyLevel urgencyLevel, JobType taskType, string createdByUser) => await NewJob(jobTitle, jobDescription, urgencyLevel, taskType, createdByUser, null);
        public async Task<Guid?> NewJob(string jobTitle, string jobDescription, JobUrgencyLevel urgencyLevel, JobType taskType, string createdByUser, string? assignedToUser)
        {
            if (!await _userRepository.ContainsAny(user => user.Username == createdByUser) || (assignedToUser != null && !await _userRepository.ContainsAny(user => user.Username == assignedToUser)))
            {
                // There isn't a user currently registered by that Id.
                // We shouldn't create a job with a link to a non-existent user.
                return null;
            }

            var createdByUser1 = await _userRepository.GetBy(user => user.Username == createdByUser);
            var assignedToUser1 = await _userRepository.GetBy(user => user.Username == assignedToUser);
            var createdByUserId = createdByUser1?.UniqueId ?? throw new ArgumentNullException("We should have never reached this point, but createdByUser1 is null somehow?");
            var assignedToUserId = assignedToUser1?.UniqueId;

            var newJob = new Job() { Title = jobTitle, Description = jobDescription, UrgencyLevel = urgencyLevel, TaskType = taskType, CreatedById = createdByUserId, CreatedAtUtc = DateTime.UtcNow, Status = JobStatus.Pending, AssignedToId = assignedToUserId };
            _jobRepository.Add(newJob);
            _jobRepository.Save();

            return newJob.UniqueId;
        }

        public async Task<Job?> GetJob(Guid jobId)
        {
            return await _jobRepository.GetById(jobId);
        }
        public IEnumerable<Job> GetJobsRelatingTo(Guid userId)
        {
            var jobsCreatedBy = GetJobsCreatedBy(userId);
            var jobsAssignedTo = GetJobsAssignedTo(userId);
            var jobsClosedBy = GetJobsClosedBy(userId);

            return jobsCreatedBy.Concat(jobsAssignedTo).Concat(jobsClosedBy);
        }
        public IEnumerable<Job> GetJobsCreatedBy(Guid userId)
        {
            return _jobRepository.GetAll().Where(job => job.CreatedById == userId);
        }
        public IEnumerable<Job> GetJobsAssignedTo(Guid userId)
        {
            return _jobRepository.GetAll().Where(job => job.AssignedToId == userId);
        }
        public IEnumerable<Job> GetJobsClosedBy(Guid userId)
        {
            return _jobRepository.GetAll().Where(job => job.ClosedById == userId);
        }

        public async void UpdateJob(Job job)
        {
            if (await _jobRepository.Contains(job))
            {
                _jobRepository.Update(job);
            }
            else
            {
                _jobRepository.Add(job);
            }
            _jobRepository.Save();
        }

        public void DeleteJob(Job job)
        {
            _jobRepository.Delete(job);
            _jobRepository.Save();
        }
        public void DeleteAllJobs()
        {
            var allJobs = _jobRepository.GetAll().ToList();
            _jobRepository.DeleteRange(allJobs);
            _jobRepository.Save();
        }
        public async void DeleteAllJobsByUser(Guid userId)
        {
            if (!await _userRepository.ContainsById(userId))
            {
                // There isn't a user currently registered by that Id.
                // We shouldn't delete jobs linked to a non-existent user.
                return;
            }

            var allJobs = _jobRepository.GetAll().Where(job => job.CreatedById == userId || job.AssignedToId == userId || job.ClosedById == userId).ToList();
            _jobRepository.DeleteRange(allJobs);
            _jobRepository.Save();
        }
    }
}
