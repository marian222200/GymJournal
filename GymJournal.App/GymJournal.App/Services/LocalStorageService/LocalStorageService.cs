using GymJournal.App.Models;
using GymJournal.Domain.Commands.WorkoutCommands;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace GymJournal.App.Services.LocalStorageService
{
	public class LocalStorageService : ILocalStorageService
	{
		private readonly IdentityService _identityService;
		private readonly string StoredUserIdFilePath = Path.Combine(FileSystem.AppDataDirectory, "UserId.json");

		public LocalStorageService(IdentityService identityService)
		{
			_identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
		}

		//public async Task StoreUserId()
		//{
		//	if(!File.Exists(StoredUserIdFilePath))
		//	{
		//		await File.WriteAllTextAsync(StoredUserIdFilePath, string.Empty);
		//	}

		//	await File.WriteAllTextAsync(StoredUserIdFilePath, JsonSerializer.Serialize(new UserIdStorageModel
		//	{
		//		UserId = _identityService.UserId,
		//		UserToken = _identityService.UserToken,
		//		IsAuthenticated = _identityService.IsAuthenticated,
		//		UserRole = _identityService.UserRole,
		//	}));
		//}

		//public async Task RetrieveUserId()
		//{
		//	if (!File.Exists(StoredUserIdFilePath))
		//	{
		//		throw new InexistentStoredDataException("no previous login");
		//	}
		//	else
		//	{
		//		var data = JsonSerializer.Deserialize<UserIdStorageModel>(await File.ReadAllTextAsync(StoredUserIdFilePath));

		//		_identityService.UserId = data.UserId;
		//		_identityService.UserToken = data.UserToken;
		//		_identityService.UserRole = data.UserRole;
		//		_identityService.IsAuthenticated = data.IsAuthenticated;
		//	}
		//}

		//public void DeleteUserId()
		//{
		//	if (File.Exists(StoredUserIdFilePath))
		//	{
		//		File.Delete(StoredUserIdFilePath);
		//	}
		//}
	}
}
