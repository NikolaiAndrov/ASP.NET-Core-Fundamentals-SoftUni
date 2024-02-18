namespace SeminarHub.Services
{
	using System;
	using System.Threading.Tasks;
	using System.Collections.Generic;
	using Microsoft.EntityFrameworkCore;

	using Data;
	using Data.Models;
	using Models.Seminar;
	using Services.Interfaces;
	using static Common.GeneralApplicationConstants;

	public class SeminarService : ISeminarService
	{
		private readonly SeminarHubDbContext dbContext;

        public SeminarService(SeminarHubDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

		public async Task AddSeminarAsync(SeminarPostModel model, string userId, DateTime dateAndTime)
		{
			Seminar seminar = new Seminar
			{
				Topic = model.Topic,
				Lecturer = model.Lecturer,
				Details = model.Details,
				DateAndTime = dateAndTime,
				Duration = model.Duration,
				CategoryId = model.CategoryId,
				OrganizerId = userId
			};

			await this.dbContext.Seminars.AddAsync(seminar);
			await this.dbContext.SaveChangesAsync();
		}

		public async Task AddSeminarToCollection(int id, string userId)
		{
			SeminarParticipant seminarParticipant = new SeminarParticipant
			{
				SeminarId = id,
				ParticipantId = userId
			};

			await this.dbContext.SeminarsPartecipiants.AddAsync(seminarParticipant);
			await this.dbContext.SaveChangesAsync();
		}

		public async Task DeleteSeminarAsync(int id)
		{
			Seminar seminar = await this.dbContext.Seminars
				.FirstAsync(s => s.Id == id);

			this.dbContext.Seminars.Remove(seminar);
			await this.dbContext.SaveChangesAsync();
		}

		public async Task EditSeminarAsync(int id, SeminarPostModel model, DateTime dateAndTime)
		{
			Seminar seminar = await this.dbContext.Seminars
				.FirstAsync(s => s.Id == id);

			seminar.Topic = model.Topic;
			seminar.Lecturer = model.Lecturer;
			seminar.Details = model.Details;
			seminar.DateAndTime = dateAndTime;
			seminar.Duration = model.Duration;
			seminar.CategoryId = model.CategoryId;

			await this.dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<SeminarAllViewModel>> GetAllSeminarAsync()
		{
			IEnumerable<SeminarAllViewModel> seminars = await this.dbContext.Seminars
				.AsNoTracking()
				.Select(s => new SeminarAllViewModel
				{
					Id = s.Id,
					Topic = s.Topic,
					Lecturer = s.Lecturer,
					Category = s.Category.Name,
					DateAndTime = s.DateAndTime.ToString(DateTimeFormat),
					Organizer = s.Organizer.UserName
				})
				.ToArrayAsync();

			return seminars;
		}

		public async Task<IEnumerable<SeminarJoinedViewModel>> GetJoinedSeminarsAsync(string userId)
		{
			IEnumerable<SeminarJoinedViewModel> seminars = await this.dbContext.SeminarsPartecipiants
				.AsNoTracking()
				.Where(sp => sp.ParticipantId == userId)
				.Select(sp => new SeminarJoinedViewModel
				{
					Id = sp.SeminarId,
					Topic = sp.Seminar.Topic,
					Lecturer = sp.Seminar.Lecturer,
					DateAndTime = sp.Seminar.DateAndTime.ToString(DateTimeFormat),
					Organizer = sp.Seminar.Organizer.UserName
				})
				.ToArrayAsync();

			return seminars;
		}

		public async Task<SeminarDetailsViewModel> GetSeminarDetailsAsync(int id)
		{
			SeminarDetailsViewModel seminar = await this.dbContext.Seminars
				.AsNoTracking()
				.Where(s => s.Id == id)
				.Select(s => new SeminarDetailsViewModel
				{
					Id = s.Id,
					Topic = s.Topic,
					DateAndTime = s.DateAndTime.ToString(DateTimeFormat),
					Duration = s.Duration,
					Lecturer = s.Lecturer,
					Category = s.Category.Name,
					Details = s.Details,
					Organizer = s.Organizer.UserName
				})
				.FirstAsync();

			return seminar;
		}

		public async Task<SeminarDeleteViewModel> GetSeminarForDeleteAsync(int id)
		{
			SeminarDeleteViewModel seminar = await this.dbContext.Seminars
				.AsNoTracking()
				.Where(s => s.Id == id)
				.Select(s => new SeminarDeleteViewModel
				{
					Id= s.Id,
					Topic = s.Topic,
					DateAndTime = s.DateAndTime
				})
				.FirstAsync();

			return seminar;
		}

		public async Task<SeminarPostModel> GetSeminarForEditAsync(int id)
		{
			SeminarPostModel model = await this.dbContext.Seminars
				.AsNoTracking()
				.Where(s => s.Id == id)
				.Select(s => new SeminarPostModel
				{
					Topic = s.Topic,
					Lecturer = s.Lecturer,
					Details = s.Details,
					DateAndTime = s.DateAndTime.ToString(DateTimeFormat),
					Duration = s.Duration,
					CategoryId = s.CategoryId
				})
				.FirstAsync();

			return model;
		}

		public async Task RemoveSeminarFromCollection(int id, string userId)
		{
			SeminarParticipant seminarParticipant = await this.dbContext.SeminarsPartecipiants
				.FirstAsync(sp => sp.SeminarId == id && sp.ParticipantId == userId);

			this.dbContext.SeminarsPartecipiants.Remove(seminarParticipant);
			await this.dbContext.SaveChangesAsync();
		}

		public async Task<bool> IsAlreadyInCollectionAsync(int id, string userId)
			=> await this.dbContext.SeminarsPartecipiants.AnyAsync(sp => sp.SeminarId == id && sp.ParticipantId == userId);

		public async Task<bool> IsSeminarExistingByIdAsync(int id)
			=> await this.dbContext.Seminars.AnyAsync(s => s.Id == id);

		public async Task<bool> IsUserOwnerOfSeminarAsync(int id, string userId)
			=> await this.dbContext.Seminars.AnyAsync(s => s.Id == id && s.OrganizerId == userId);
	}
}
