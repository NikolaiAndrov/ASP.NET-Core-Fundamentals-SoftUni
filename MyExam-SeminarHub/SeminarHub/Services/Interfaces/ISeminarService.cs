namespace SeminarHub.Services.Interfaces
{
	using Models.Seminar;

	public interface ISeminarService
	{
		Task AddSeminarAsync(SeminarPostModel model, string userId, DateTime dateAndTime);

		Task<IEnumerable<SeminarAllViewModel>> GetAllSeminarAsync();

		Task<bool> IsSeminarExistingByIdAsync(int id);

		Task<bool> IsUserOwnerOfSeminarAsync(int id, string userId);

		Task<SeminarPostModel> GetSeminarForEditAsync(int id);

		Task EditSeminarAsync(int id, SeminarPostModel model, DateTime dateAndTime);

		Task<SeminarDetailsViewModel> GetSeminarDetailsAsync(int id);

		Task<SeminarDeleteViewModel> GetSeminarForDeleteAsync(int id);

		Task DeleteSeminarAsync(int id);

		Task<bool> IsAlreadyInCollectionAsync(int id, string userId);

		Task AddSeminarToCollection(int id, string userId);

		Task RemoveSeminarFromCollection(int id, string userId);

		Task<IEnumerable<SeminarJoinedViewModel>> GetJoinedSeminarsAsync(string userId);
	}
}
