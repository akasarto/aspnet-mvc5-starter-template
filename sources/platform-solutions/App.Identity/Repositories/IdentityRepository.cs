using Dapper;
using Data.Core;
using Domain.Core;
using Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;

namespace App.Identity
{
	public class IdentityRepository : IIdentityRepository
	{
		internal readonly IDbConnectionFactory _dbConnectionFactory = null;

		/// <summary>
		/// Contructor method.
		/// </summary>
		public IdentityRepository(IDbConnectionFactory dbConnectionFactory)
		{
			_dbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory), nameof(IdentityRepository));
		}

		/// <summary>
		/// Create a new user.
		/// </summary>
		/// <param name="adminUser">The new user instance.</param>
		/// <returns>Returns the user instance with the updated id.</returns>
		public AdminUserEntity Create(AdminUserEntity adminUser)
		{
			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				connection.Open();

				using (var transaction = connection.BeginTransaction())
				{
					var parameters = new DynamicParameters();

					parameters.Add("@MobilePhone", adminUser.MobilePhone, DbType.String);
					parameters.Add("@LockoutEnabled", adminUser.LockoutEnabled, DbType.Boolean);
					parameters.Add("@TwoFactorEnabled", adminUser.TwoFactorEnabled, DbType.Boolean);
					parameters.Add("@MobilePhoneConfirmed", adminUser.MobilePhoneConfirmed, DbType.Boolean);
					parameters.Add("@LockoutEndDateUtc", adminUser.LockoutEndDateUtc, DbType.DateTimeOffset);
					parameters.Add("@AccessFailedCount", adminUser.AccessFailedCount, DbType.Int32);
					parameters.Add("@EmailConfirmed", adminUser.EmailConfirmed, DbType.Boolean);
					parameters.Add("@PasswordHash", adminUser.PasswordHash, DbType.String);
					parameters.Add("@SecurityStamp", adminUser.SecurityStamp, DbType.String);
					parameters.Add("@UserName", adminUser.UserName, DbType.String);
					parameters.Add("@FullName", adminUser.FullName, DbType.String);
					parameters.Add("@Email", adminUser.Email, DbType.String);

					parameters.Add("@TimeZoneId", adminUser.TimeZoneId, DbType.String);
					parameters.Add("@UICultureId", adminUser.UICultureId, DbType.String);
					parameters.Add("@CultureId", adminUser.CultureId, DbType.String);

					adminUser.Id = connection.ExecuteScalar<int>(
						sql: "UsersWithAdminProfileInsert",
						commandType: CommandType.StoredProcedure,
						transaction: transaction,
						param: parameters
					);

					UserClaimsSet(connection, transaction, claims: adminUser.Claims, user: adminUser, cleanup: false);
					UserRealmsSet(connection, transaction, realms: adminUser.Realms, user: adminUser, cleanup: false);
					UserRolesSet(connection, transaction, roles: adminUser.Roles, user: adminUser, cleanup: false);

					transaction.Commit();

					return adminUser;
				}
			}
		}

		/// <summary>
		/// Get all existing users.
		/// </summary>
		/// <returns>Returns a list of admin users.</returns>
		public IEnumerable<AdminUserEntity> GetAll()
		{
			var parameters = new DynamicParameters();

			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				using (var reader = connection.QueryMultiple(sql: "UsersWithAdminProfileSelect", commandType: CommandType.StoredProcedure, param: parameters))
				{
					var entities = BuildEntitiesList(reader);

					return entities;
				}
			}
		}

		/// <summary>
		/// Get a single user.
		/// </summary>
		/// <param name="userEmail">The email to search.</param>
		/// <returns>Returns an admin user instance.</returns>
		public AdminUserEntity GetByEmail(string userEmail)
		{
			var parameters = new DynamicParameters();

			parameters.Add("@Email", userEmail, DbType.String);

			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				using (var reader = connection.QueryMultiple(sql: "UsersWithAdminProfileSelect", commandType: CommandType.StoredProcedure, param: parameters))
				{
					var entity = BuildEntity(reader);

					return entity;
				}
			}
		}

		/// <summary>
		/// Get a single user.
		/// </summary>
		/// <param name="userId">The id to search.</param>
		/// <returns>Returns an admin user instance.</returns>
		public AdminUserEntity GetById(int userId)
		{
			var parameters = new DynamicParameters();

			parameters.Add("@Id", userId, DbType.Int32);

			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				using (var reader = connection.QueryMultiple(sql: "UsersWithAdminProfileSelect", commandType: CommandType.StoredProcedure, param: parameters))
				{
					var entity = BuildEntity(reader);

					return entity;
				}
			}
		}

		/// <summary>
		/// Get a single user.
		/// </summary>
		/// <param name="userName">The 'userName' to search.</param>
		/// <returns>Returns an admin user instance.</returns>
		public AdminUserEntity GetByUserName(string userName)
		{
			var parameters = new DynamicParameters();

			parameters.Add("@UserName", userName, DbType.String);

			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				using (var reader = connection.QueryMultiple(sql: "UsersWithAdminProfileSelect", commandType: CommandType.StoredProcedure, param: parameters))
				{
					var entity = BuildEntity(reader);

					return entity;
				}
			}
		}

		/// <summary>
		/// Update the admin user identity information.
		/// </summary>
		/// <param name="adminUser">The admin user instance.</param>
		public void UpdateIdentity(AdminUserEntity adminUser)
		{
			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				connection.Open();

				using (var transaction = connection.BeginTransaction())
				{
					var parameters = new DynamicParameters();

					parameters.Add("@Id", adminUser.Id, DbType.Int32);
					parameters.Add("@IsBlocked", adminUser.IsBlocked, DbType.Boolean);
					parameters.Add("@MobilePhone", adminUser.MobilePhone, DbType.String);
					parameters.Add("@LockoutEnabled", adminUser.LockoutEnabled, DbType.Boolean);
					parameters.Add("@TwoFactorEnabled", adminUser.TwoFactorEnabled, DbType.Boolean);
					parameters.Add("@MobilePhoneConfirmed", adminUser.MobilePhoneConfirmed, DbType.Boolean);
					parameters.Add("@LockoutEndDateUtc", adminUser.LockoutEndDateUtc, DbType.DateTimeOffset);
					parameters.Add("@AccessFailedCount", adminUser.AccessFailedCount, DbType.Int32);
					parameters.Add("@EmailConfirmed", adminUser.EmailConfirmed, DbType.Boolean);
					parameters.Add("@PasswordHash", adminUser.PasswordHash, DbType.String);
					parameters.Add("@SecurityStamp", adminUser.SecurityStamp, DbType.String);
					parameters.Add("@UserName", adminUser.UserName, DbType.String);
					parameters.Add("@FullName", adminUser.FullName, DbType.String);
					parameters.Add("@Email", adminUser.Email, DbType.String);

					int result = connection.Execute(
						sql: "UsersWithIdentityUpdate",
						commandType: CommandType.StoredProcedure,
						transaction: transaction,
						param: parameters
					);

					UserClaimsSet(connection, transaction, claims: adminUser.Claims, user: adminUser, cleanup: true);
					UserRealmsSet(connection, transaction, realms: adminUser.Realms, user: adminUser, cleanup: true);
					UserRolesSet(connection, transaction, roles: adminUser.Roles, user: adminUser, cleanup: true);

					transaction.Commit();
				}
			}
		}

		/// <summary>
		/// Update only the information related to the user profile.
		/// </summary>
		/// <param name="adminUser">The admin user instance.</param>
		public void UpdateProfile(AdminUserEntity adminUser)
		{
			using (var connection = _dbConnectionFactory.CreateConnection())
			{
				connection.Open();

				using (var transaction = connection.BeginTransaction())
				{
					var parameters = new DynamicParameters();

					parameters.Add("@Id", adminUser.Id, DbType.Int32);

					parameters.Add("@UserName", adminUser.UserName, DbType.String);
					parameters.Add("@FullName", adminUser.FullName, DbType.String);
					parameters.Add("@Email", adminUser.Email, DbType.String);

					parameters.Add("@ScreenAutoLockMinutes", adminUser.ScreenAutoLockMinutes, DbType.Int32);
					parameters.Add("@PictureBlobId", adminUser.PictureBlobId, DbType.Guid);
					parameters.Add("@TimeZoneId", adminUser.TimeZoneId, DbType.String);
					parameters.Add("@UICultureId", adminUser.UICultureId, DbType.String);
					parameters.Add("@CultureId", adminUser.CultureId, DbType.String);

					int result = connection.Execute(
						sql: "UsersWithAdminProfileUpdate",
						commandType: CommandType.StoredProcedure,
						transaction: transaction,
						param: parameters
					);

					transaction.Commit();
				}
			}
		}

		/// <summary>
		/// Maps the data reader result to the admin user objects.
		/// </summary>
		private Func<AdminUserEntity, BlobEntity, AdminUserEntity> _userMap = (user, blob) =>
		{
			user.PictureInfo = blob?.GetInfo();

			return user;
		};

		/// <summary>
		/// Build the full admin user instance.
		/// </summary>
		/// <param name="reader">The reader from the executed query.</param>
		/// <returns>Returns an admin user instance.</returns>
		private AdminUserEntity BuildEntity(SqlMapper.GridReader reader)
		{
			var entity = reader.Read(_userMap).SingleOrDefault();

			if (entity != null)
			{
				var claims = reader.Read<UserClaim>().ToList();
				var realms = reader.Read<UserRealm>().ToList();
				var roles = reader.Read<UserRole>().ToList();

				entity.Claims = claims.Select(claim => new Claim(claim.Type, claim.Value)).ToList();
				entity.Realms = realms.Select(realm => realm.Realm).ToList();
				entity.Roles = roles.Select(role => role.Role).ToList();
			}

			return entity;
		}

		/// <summary>
		/// Buils a list of full admin user instances.
		/// </summary>
		/// <param name="reader">The reader from the executed query.</param>
		/// <returns>Returns a list of admin user instances.</returns>
		private IEnumerable<AdminUserEntity> BuildEntitiesList(SqlMapper.GridReader reader)
		{
			var entities = reader.Read(_userMap).ToList();

			var claimsCollection = reader
				.Read<UserClaim>()
				.GroupBy(uClaim => uClaim.UserId)
				.ToDictionary(group => group.Key, group => group.ToList());

			var realmsCollection = reader
				.Read<UserRealm>()
				.GroupBy(uRealm => uRealm.UserId)
				.ToDictionary(group => group.Key, group => group.ToList());

			var rolesCollection = reader
				.Read<UserRole>()
				.GroupBy(uRole => uRole.UserId)
				.ToDictionary(group => group.Key, group => group.ToList());

			foreach (var entity in entities)
			{
				List<UserClaim> claims;
				List<UserRealm> realms;
				List<UserRole> roles;

				if (!claimsCollection.TryGetValue(entity.Id, out claims))
				{
					claims = new List<UserClaim>();
				}

				if (!realmsCollection.TryGetValue(entity.Id, out realms))
				{
					realms = new List<UserRealm>();
				}

				if (!rolesCollection.TryGetValue(entity.Id, out roles))
				{
					roles = new List<UserRole>();
				}

				entity.Claims = claims.Select(claim => new Claim(claim.Type, claim.Value)).ToList();
				entity.Realms = realms.Select(realm => realm.Realm).ToList();
				entity.Roles = roles.Select(role => role.Role).ToList();
			}

			return entities;
		}

		/// <summary>
		/// Manages the admin user related claims.
		/// </summary>
		/// <param name="dbConnection">Current database connection.</param>
		/// <param name="transaction">Current database transaction.</param>
		/// <param name="claims">The current set of claims for the user.</param>
		/// <param name="user">The user begin managed.</param>
		/// <param name="cleanup">Remove previous claims before adding the new ones.</param>
		private void UserClaimsSet(IDbConnection dbConnection, IDbTransaction transaction, List<Claim> claims, AdminUserEntity user, bool cleanup)
		{
			if (cleanup)
			{
				var parameters = new DynamicParameters();

				parameters.Add(@"UserId", user.Id, DbType.Int32);

				dbConnection.Execute(
					sql: "DELETE FROM [UserClaims] WHERE UserId = @UserId",
					commandType: CommandType.Text,
					transaction: transaction,
					param: parameters
				);
			}

			foreach (var claim in claims)
			{
				var parameters = new DynamicParameters();

				parameters.Add("@UserId", user.Id, DbType.Int32);
				parameters.Add("@Type", claim.Type, DbType.String);
				parameters.Add("@Value", claim.Value, DbType.String);

				dbConnection.Query(
					sql: "INSERT INTO [UserClaims] (UserId, [Type], [Value]) VALUES (@UserId, @Type, @Value)",
					commandType: CommandType.Text,
					transaction: transaction,
					param: parameters
				);
			}
		}

		/// <summary>
		/// Manages the admin user related realms.
		/// </summary>
		/// <param name="dbConnection">Current database connection.</param>
		/// <param name="transaction">Current database transaction.</param>
		/// <param name="realms">The current set of realms for the user.</param>
		/// <param name="user">The user begin managed.</param>
		/// <param name="cleanup">Remove previous realms before adding the new ones.</param>
		private void UserRealmsSet(IDbConnection dbConnection, IDbTransaction transaction, List<Realm> realms, AdminUserEntity user, bool cleanup)
		{
			if (cleanup)
			{
				var parameters = new DynamicParameters();

				parameters.Add(@"UserId", user.Id, DbType.Int32);

				dbConnection.Execute(
					sql: "DELETE FROM [UserRealms] WHERE UserId = @UserId",
					commandType: CommandType.Text,
					transaction: transaction,
					param: parameters
				);
			}

			foreach (var realm in realms)
			{
				var parameters = new DynamicParameters();

				parameters.Add("@UserId", user.Id, DbType.Int32);
				parameters.Add("@Realm", realm, DbType.Int32);

				dbConnection.Query(
					sql: "INSERT INTO [UserRealms] (UserId, [Realm]) VALUES (@UserId, @Realm)",
					commandType: CommandType.Text,
					transaction: transaction,
					param: parameters
				);
			}
		}

		/// <summary>
		/// Manages the admin user related roles.
		/// </summary>
		/// <param name="dbConnection">Current database connection.</param>
		/// <param name="transaction">Current database transaction.</param>
		/// <param name="roles">The current set of roles for the user.</param>
		/// <param name="user">The user begin managed.</param>
		/// <param name="cleanup">Remove previous roles before adding the new ones.</param>
		private void UserRolesSet(IDbConnection dbConnection, IDbTransaction transaction, List<Role> roles, AdminUserEntity user, bool cleanup)
		{
			if (cleanup)
			{
				var parameters = new DynamicParameters();

				parameters.Add(@"UserId", user.Id, DbType.Int32);

				dbConnection.Execute(
					sql: "DELETE FROM [UserRoles] WHERE UserId = @UserId",
					commandType: CommandType.Text,
					transaction: transaction,
					param: parameters
				);
			}

			foreach (var role in roles)
			{
				var parameters = new DynamicParameters();

				parameters.Add("@UserId", user.Id, DbType.Int32);
				parameters.Add("@Role", role.ToString(), DbType.String);

				dbConnection.Query(
					sql: "INSERT INTO [UserRoles] (UserId, [Role]) VALUES (@UserId, @Role)",
					commandType: CommandType.Text,
					transaction: transaction,
					param: parameters
				);
			}
		}
	}
}
