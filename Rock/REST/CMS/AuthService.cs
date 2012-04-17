//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the T4\Model.tt template.
//
//     Changes to this file will be lost when the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
//
// THIS WORK IS LICENSED UNDER A CREATIVE COMMONS ATTRIBUTION-NONCOMMERCIAL-
// SHAREALIKE 3.0 UNPORTED LICENSE:
// http://creativecommons.org/licenses/by-nc-sa/3.0/
//
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace Rock.REST.CMS
{
	/// <summary>
	/// REST WCF service for Auths
	/// </summary>
    [Export(typeof(IService))]
    [ExportMetadata("RouteName", "CMS/Auth")]
	[AspNetCompatibilityRequirements( RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed )]
    public partial class AuthService : IAuthService, IService
    {
		/// <summary>
		/// Gets a Auth object
		/// </summary>
		[WebGet( UriTemplate = "{id}" )]
        public Rock.CMS.DTO.Auth Get( string id )
        {
            var currentUser = Rock.CMS.UserRepository.GetCurrentUser();
            if ( currentUser == null )
                throw new WebFaultException<string>("Must be logged in", System.Net.HttpStatusCode.Forbidden );

            using (Rock.Data.UnitOfWorkScope uow = new Rock.Data.UnitOfWorkScope())
            {
				uow.objectContext.Configuration.ProxyCreationEnabled = false;
				Rock.CMS.AuthRepository AuthRepository = new Rock.CMS.AuthRepository();
				Rock.CMS.Auth Auth = AuthRepository.Get( int.Parse( id ) );
				if ( Auth.Authorized( "View", currentUser ) )
					return Auth.DataTransferObject;
				else
					throw new WebFaultException<string>( "Not Authorized to View this Auth", System.Net.HttpStatusCode.Forbidden );
            }
        }
		
		/// <summary>
		/// Gets a Auth object
		/// </summary>
		[WebGet( UriTemplate = "{id}/{apiKey}" )]
        public Rock.CMS.DTO.Auth ApiGet( string id, string apiKey )
        {
            using (Rock.Data.UnitOfWorkScope uow = new Rock.Data.UnitOfWorkScope())
            {
				Rock.CMS.UserRepository userRepository = new Rock.CMS.UserRepository();
                Rock.CMS.User user = userRepository.AsQueryable().Where( u => u.ApiKey == apiKey ).FirstOrDefault();

				if (user != null)
				{
					uow.objectContext.Configuration.ProxyCreationEnabled = false;
					Rock.CMS.AuthRepository AuthRepository = new Rock.CMS.AuthRepository();
					Rock.CMS.Auth Auth = AuthRepository.Get( int.Parse( id ) );
					if ( Auth.Authorized( "View", user ) )
						return Auth.DataTransferObject;
					else
						throw new WebFaultException<string>( "Not Authorized to View this Auth", System.Net.HttpStatusCode.Forbidden );
				}
				else
					throw new WebFaultException<string>( "Invalid API Key", System.Net.HttpStatusCode.Forbidden );
            }
        }
		
		/// <summary>
		/// Updates a Auth object
		/// </summary>
		[WebInvoke( Method = "PUT", UriTemplate = "{id}" )]
        public void UpdateAuth( string id, Rock.CMS.DTO.Auth Auth )
        {
            var currentUser = Rock.CMS.UserRepository.GetCurrentUser();
            if ( currentUser == null )
                throw new WebFaultException<string>("Must be logged in", System.Net.HttpStatusCode.Forbidden );

            using ( Rock.Data.UnitOfWorkScope uow = new Rock.Data.UnitOfWorkScope() )
            {
				uow.objectContext.Configuration.ProxyCreationEnabled = false;
				Rock.CMS.AuthRepository AuthRepository = new Rock.CMS.AuthRepository();
				Rock.CMS.Auth existingAuth = AuthRepository.Get( int.Parse( id ) );
				if ( existingAuth.Authorized( "Edit", currentUser ) )
				{
					uow.objectContext.Entry(existingAuth).CurrentValues.SetValues(Auth);
					
					if (existingAuth.IsValid)
						AuthRepository.Save( existingAuth, currentUser.PersonId );
					else
						throw new WebFaultException<string>( existingAuth.ValidationResults.AsDelimited(", "), System.Net.HttpStatusCode.BadRequest );
				}
				else
					throw new WebFaultException<string>( "Not Authorized to Edit this Auth", System.Net.HttpStatusCode.Forbidden );
            }
        }

		/// <summary>
		/// Updates a Auth object
		/// </summary>
		[WebInvoke( Method = "PUT", UriTemplate = "{id}/{apiKey}" )]
        public void ApiUpdateAuth( string id, string apiKey, Rock.CMS.DTO.Auth Auth )
        {
            using ( Rock.Data.UnitOfWorkScope uow = new Rock.Data.UnitOfWorkScope() )
            {
				Rock.CMS.UserRepository userRepository = new Rock.CMS.UserRepository();
                Rock.CMS.User user = userRepository.AsQueryable().Where( u => u.ApiKey == apiKey ).FirstOrDefault();

				if (user != null)
				{
					uow.objectContext.Configuration.ProxyCreationEnabled = false;
					Rock.CMS.AuthRepository AuthRepository = new Rock.CMS.AuthRepository();
					Rock.CMS.Auth existingAuth = AuthRepository.Get( int.Parse( id ) );
					if ( existingAuth.Authorized( "Edit", user ) )
					{
						uow.objectContext.Entry(existingAuth).CurrentValues.SetValues(Auth);
					
						if (existingAuth.IsValid)
							AuthRepository.Save( existingAuth, user.PersonId );
						else
							throw new WebFaultException<string>( existingAuth.ValidationResults.AsDelimited(", "), System.Net.HttpStatusCode.BadRequest );
					}
					else
						throw new WebFaultException<string>( "Not Authorized to Edit this Auth", System.Net.HttpStatusCode.Forbidden );
				}
				else
					throw new WebFaultException<string>( "Invalid API Key", System.Net.HttpStatusCode.Forbidden );
            }
        }

		/// <summary>
		/// Creates a new Auth object
		/// </summary>
		[WebInvoke( Method = "POST", UriTemplate = "" )]
        public void CreateAuth( Rock.CMS.DTO.Auth Auth )
        {
            var currentUser = Rock.CMS.UserRepository.GetCurrentUser();
            if ( currentUser == null )
                throw new WebFaultException<string>("Must be logged in", System.Net.HttpStatusCode.Forbidden );

            using ( Rock.Data.UnitOfWorkScope uow = new Rock.Data.UnitOfWorkScope() )
            {
				uow.objectContext.Configuration.ProxyCreationEnabled = false;
				Rock.CMS.AuthRepository AuthRepository = new Rock.CMS.AuthRepository();
				Rock.CMS.Auth existingAuth = new Rock.CMS.Auth();
				AuthRepository.Add( existingAuth, currentUser.PersonId );
				uow.objectContext.Entry(existingAuth).CurrentValues.SetValues(Auth);

				if (existingAuth.IsValid)
					AuthRepository.Save( existingAuth, currentUser.PersonId );
				else
					throw new WebFaultException<string>( existingAuth.ValidationResults.AsDelimited(", "), System.Net.HttpStatusCode.BadRequest );
            }
        }

		/// <summary>
		/// Creates a new Auth object
		/// </summary>
		[WebInvoke( Method = "POST", UriTemplate = "{apiKey}" )]
        public void ApiCreateAuth( string apiKey, Rock.CMS.DTO.Auth Auth )
        {
            using ( Rock.Data.UnitOfWorkScope uow = new Rock.Data.UnitOfWorkScope() )
            {
				Rock.CMS.UserRepository userRepository = new Rock.CMS.UserRepository();
                Rock.CMS.User user = userRepository.AsQueryable().Where( u => u.ApiKey == apiKey ).FirstOrDefault();

				if (user != null)
				{
					uow.objectContext.Configuration.ProxyCreationEnabled = false;
					Rock.CMS.AuthRepository AuthRepository = new Rock.CMS.AuthRepository();
					Rock.CMS.Auth existingAuth = new Rock.CMS.Auth();
					AuthRepository.Add( existingAuth, user.PersonId );
					uow.objectContext.Entry(existingAuth).CurrentValues.SetValues(Auth);

					if (existingAuth.IsValid)
						AuthRepository.Save( existingAuth, user.PersonId );
					else
						throw new WebFaultException<string>( existingAuth.ValidationResults.AsDelimited(", "), System.Net.HttpStatusCode.BadRequest );
				}
				else
					throw new WebFaultException<string>( "Invalid API Key", System.Net.HttpStatusCode.Forbidden );
            }
        }

		/// <summary>
		/// Deletes a Auth object
		/// </summary>
		[WebInvoke( Method = "DELETE", UriTemplate = "{id}" )]
        public void DeleteAuth( string id )
        {
            var currentUser = Rock.CMS.UserRepository.GetCurrentUser();
            if ( currentUser == null )
                throw new WebFaultException<string>("Must be logged in", System.Net.HttpStatusCode.Forbidden );

            using ( Rock.Data.UnitOfWorkScope uow = new Rock.Data.UnitOfWorkScope() )
            {
				uow.objectContext.Configuration.ProxyCreationEnabled = false;
				Rock.CMS.AuthRepository AuthRepository = new Rock.CMS.AuthRepository();
				Rock.CMS.Auth Auth = AuthRepository.Get( int.Parse( id ) );
				if ( Auth.Authorized( "Edit", currentUser ) )
				{
					AuthRepository.Delete( Auth, currentUser.PersonId );
					AuthRepository.Save( Auth, currentUser.PersonId );
				}
				else
					throw new WebFaultException<string>( "Not Authorized to Edit this Auth", System.Net.HttpStatusCode.Forbidden );
            }
        }

		/// <summary>
		/// Deletes a Auth object
		/// </summary>
		[WebInvoke( Method = "DELETE", UriTemplate = "{id}/{apiKey}" )]
        public void ApiDeleteAuth( string id, string apiKey )
        {
            using ( Rock.Data.UnitOfWorkScope uow = new Rock.Data.UnitOfWorkScope() )
            {
				Rock.CMS.UserRepository userRepository = new Rock.CMS.UserRepository();
                Rock.CMS.User user = userRepository.AsQueryable().Where( u => u.ApiKey == apiKey ).FirstOrDefault();

				if (user != null)
				{
					uow.objectContext.Configuration.ProxyCreationEnabled = false;
					Rock.CMS.AuthRepository AuthRepository = new Rock.CMS.AuthRepository();
					Rock.CMS.Auth Auth = AuthRepository.Get( int.Parse( id ) );
					if ( Auth.Authorized( "Edit", user ) )
					{
						AuthRepository.Delete( Auth, user.PersonId );
						AuthRepository.Save( Auth, user.PersonId );
					}
					else
						throw new WebFaultException<string>( "Not Authorized to Edit this Auth", System.Net.HttpStatusCode.Forbidden );
				}
				else
					throw new WebFaultException<string>( "Invalid API Key", System.Net.HttpStatusCode.Forbidden );
            }
        }

    }
}
