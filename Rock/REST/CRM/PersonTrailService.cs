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

namespace Rock.REST.CRM
{
	/// <summary>
	/// REST WCF service for PersonTrails
	/// </summary>
    [Export(typeof(IService))]
    [ExportMetadata("RouteName", "CRM/PersonTrail")]
	[AspNetCompatibilityRequirements( RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed )]
    public partial class PersonTrailService : IPersonTrailService, IService
    {
		/// <summary>
		/// Gets a PersonTrail object
		/// </summary>
		[WebGet( UriTemplate = "{id}" )]
        public Rock.CRM.DTO.PersonTrail Get( string id )
        {
            var currentUser = Rock.CMS.UserRepository.GetCurrentUser();
            if ( currentUser == null )
                throw new WebFaultException<string>("Must be logged in", System.Net.HttpStatusCode.Forbidden );

            using (Rock.Data.UnitOfWorkScope uow = new Rock.Data.UnitOfWorkScope())
            {
				uow.objectContext.Configuration.ProxyCreationEnabled = false;
				Rock.CRM.PersonTrailRepository PersonTrailRepository = new Rock.CRM.PersonTrailRepository();
				Rock.CRM.PersonTrail PersonTrail = PersonTrailRepository.Get( int.Parse( id ) );
				if ( PersonTrail.Authorized( "View", currentUser ) )
					return PersonTrail.DataTransferObject;
				else
					throw new WebFaultException<string>( "Not Authorized to View this PersonTrail", System.Net.HttpStatusCode.Forbidden );
            }
        }
		
		/// <summary>
		/// Gets a PersonTrail object
		/// </summary>
		[WebGet( UriTemplate = "{id}/{apiKey}" )]
        public Rock.CRM.DTO.PersonTrail ApiGet( string id, string apiKey )
        {
            using (Rock.Data.UnitOfWorkScope uow = new Rock.Data.UnitOfWorkScope())
            {
				Rock.CMS.UserRepository userRepository = new Rock.CMS.UserRepository();
                Rock.CMS.User user = userRepository.AsQueryable().Where( u => u.ApiKey == apiKey ).FirstOrDefault();

				if (user != null)
				{
					uow.objectContext.Configuration.ProxyCreationEnabled = false;
					Rock.CRM.PersonTrailRepository PersonTrailRepository = new Rock.CRM.PersonTrailRepository();
					Rock.CRM.PersonTrail PersonTrail = PersonTrailRepository.Get( int.Parse( id ) );
					if ( PersonTrail.Authorized( "View", user ) )
						return PersonTrail.DataTransferObject;
					else
						throw new WebFaultException<string>( "Not Authorized to View this PersonTrail", System.Net.HttpStatusCode.Forbidden );
				}
				else
					throw new WebFaultException<string>( "Invalid API Key", System.Net.HttpStatusCode.Forbidden );
            }
        }
		
		/// <summary>
		/// Updates a PersonTrail object
		/// </summary>
		[WebInvoke( Method = "PUT", UriTemplate = "{id}" )]
        public void UpdatePersonTrail( string id, Rock.CRM.DTO.PersonTrail PersonTrail )
        {
            var currentUser = Rock.CMS.UserRepository.GetCurrentUser();
            if ( currentUser == null )
                throw new WebFaultException<string>("Must be logged in", System.Net.HttpStatusCode.Forbidden );

            using ( Rock.Data.UnitOfWorkScope uow = new Rock.Data.UnitOfWorkScope() )
            {
				uow.objectContext.Configuration.ProxyCreationEnabled = false;
				Rock.CRM.PersonTrailRepository PersonTrailRepository = new Rock.CRM.PersonTrailRepository();
				Rock.CRM.PersonTrail existingPersonTrail = PersonTrailRepository.Get( int.Parse( id ) );
				if ( existingPersonTrail.Authorized( "Edit", currentUser ) )
				{
					uow.objectContext.Entry(existingPersonTrail).CurrentValues.SetValues(PersonTrail);
					
					if (existingPersonTrail.IsValid)
						PersonTrailRepository.Save( existingPersonTrail, currentUser.PersonId );
					else
						throw new WebFaultException<string>( existingPersonTrail.ValidationResults.AsDelimited(", "), System.Net.HttpStatusCode.BadRequest );
				}
				else
					throw new WebFaultException<string>( "Not Authorized to Edit this PersonTrail", System.Net.HttpStatusCode.Forbidden );
            }
        }

		/// <summary>
		/// Updates a PersonTrail object
		/// </summary>
		[WebInvoke( Method = "PUT", UriTemplate = "{id}/{apiKey}" )]
        public void ApiUpdatePersonTrail( string id, string apiKey, Rock.CRM.DTO.PersonTrail PersonTrail )
        {
            using ( Rock.Data.UnitOfWorkScope uow = new Rock.Data.UnitOfWorkScope() )
            {
				Rock.CMS.UserRepository userRepository = new Rock.CMS.UserRepository();
                Rock.CMS.User user = userRepository.AsQueryable().Where( u => u.ApiKey == apiKey ).FirstOrDefault();

				if (user != null)
				{
					uow.objectContext.Configuration.ProxyCreationEnabled = false;
					Rock.CRM.PersonTrailRepository PersonTrailRepository = new Rock.CRM.PersonTrailRepository();
					Rock.CRM.PersonTrail existingPersonTrail = PersonTrailRepository.Get( int.Parse( id ) );
					if ( existingPersonTrail.Authorized( "Edit", user ) )
					{
						uow.objectContext.Entry(existingPersonTrail).CurrentValues.SetValues(PersonTrail);
					
						if (existingPersonTrail.IsValid)
							PersonTrailRepository.Save( existingPersonTrail, user.PersonId );
						else
							throw new WebFaultException<string>( existingPersonTrail.ValidationResults.AsDelimited(", "), System.Net.HttpStatusCode.BadRequest );
					}
					else
						throw new WebFaultException<string>( "Not Authorized to Edit this PersonTrail", System.Net.HttpStatusCode.Forbidden );
				}
				else
					throw new WebFaultException<string>( "Invalid API Key", System.Net.HttpStatusCode.Forbidden );
            }
        }

		/// <summary>
		/// Creates a new PersonTrail object
		/// </summary>
		[WebInvoke( Method = "POST", UriTemplate = "" )]
        public void CreatePersonTrail( Rock.CRM.DTO.PersonTrail PersonTrail )
        {
            var currentUser = Rock.CMS.UserRepository.GetCurrentUser();
            if ( currentUser == null )
                throw new WebFaultException<string>("Must be logged in", System.Net.HttpStatusCode.Forbidden );

            using ( Rock.Data.UnitOfWorkScope uow = new Rock.Data.UnitOfWorkScope() )
            {
				uow.objectContext.Configuration.ProxyCreationEnabled = false;
				Rock.CRM.PersonTrailRepository PersonTrailRepository = new Rock.CRM.PersonTrailRepository();
				Rock.CRM.PersonTrail existingPersonTrail = new Rock.CRM.PersonTrail();
				PersonTrailRepository.Add( existingPersonTrail, currentUser.PersonId );
				uow.objectContext.Entry(existingPersonTrail).CurrentValues.SetValues(PersonTrail);

				if (existingPersonTrail.IsValid)
					PersonTrailRepository.Save( existingPersonTrail, currentUser.PersonId );
				else
					throw new WebFaultException<string>( existingPersonTrail.ValidationResults.AsDelimited(", "), System.Net.HttpStatusCode.BadRequest );
            }
        }

		/// <summary>
		/// Creates a new PersonTrail object
		/// </summary>
		[WebInvoke( Method = "POST", UriTemplate = "{apiKey}" )]
        public void ApiCreatePersonTrail( string apiKey, Rock.CRM.DTO.PersonTrail PersonTrail )
        {
            using ( Rock.Data.UnitOfWorkScope uow = new Rock.Data.UnitOfWorkScope() )
            {
				Rock.CMS.UserRepository userRepository = new Rock.CMS.UserRepository();
                Rock.CMS.User user = userRepository.AsQueryable().Where( u => u.ApiKey == apiKey ).FirstOrDefault();

				if (user != null)
				{
					uow.objectContext.Configuration.ProxyCreationEnabled = false;
					Rock.CRM.PersonTrailRepository PersonTrailRepository = new Rock.CRM.PersonTrailRepository();
					Rock.CRM.PersonTrail existingPersonTrail = new Rock.CRM.PersonTrail();
					PersonTrailRepository.Add( existingPersonTrail, user.PersonId );
					uow.objectContext.Entry(existingPersonTrail).CurrentValues.SetValues(PersonTrail);

					if (existingPersonTrail.IsValid)
						PersonTrailRepository.Save( existingPersonTrail, user.PersonId );
					else
						throw new WebFaultException<string>( existingPersonTrail.ValidationResults.AsDelimited(", "), System.Net.HttpStatusCode.BadRequest );
				}
				else
					throw new WebFaultException<string>( "Invalid API Key", System.Net.HttpStatusCode.Forbidden );
            }
        }

		/// <summary>
		/// Deletes a PersonTrail object
		/// </summary>
		[WebInvoke( Method = "DELETE", UriTemplate = "{id}" )]
        public void DeletePersonTrail( string id )
        {
            var currentUser = Rock.CMS.UserRepository.GetCurrentUser();
            if ( currentUser == null )
                throw new WebFaultException<string>("Must be logged in", System.Net.HttpStatusCode.Forbidden );

            using ( Rock.Data.UnitOfWorkScope uow = new Rock.Data.UnitOfWorkScope() )
            {
				uow.objectContext.Configuration.ProxyCreationEnabled = false;
				Rock.CRM.PersonTrailRepository PersonTrailRepository = new Rock.CRM.PersonTrailRepository();
				Rock.CRM.PersonTrail PersonTrail = PersonTrailRepository.Get( int.Parse( id ) );
				if ( PersonTrail.Authorized( "Edit", currentUser ) )
				{
					PersonTrailRepository.Delete( PersonTrail, currentUser.PersonId );
					PersonTrailRepository.Save( PersonTrail, currentUser.PersonId );
				}
				else
					throw new WebFaultException<string>( "Not Authorized to Edit this PersonTrail", System.Net.HttpStatusCode.Forbidden );
            }
        }

		/// <summary>
		/// Deletes a PersonTrail object
		/// </summary>
		[WebInvoke( Method = "DELETE", UriTemplate = "{id}/{apiKey}" )]
        public void ApiDeletePersonTrail( string id, string apiKey )
        {
            using ( Rock.Data.UnitOfWorkScope uow = new Rock.Data.UnitOfWorkScope() )
            {
				Rock.CMS.UserRepository userRepository = new Rock.CMS.UserRepository();
                Rock.CMS.User user = userRepository.AsQueryable().Where( u => u.ApiKey == apiKey ).FirstOrDefault();

				if (user != null)
				{
					uow.objectContext.Configuration.ProxyCreationEnabled = false;
					Rock.CRM.PersonTrailRepository PersonTrailRepository = new Rock.CRM.PersonTrailRepository();
					Rock.CRM.PersonTrail PersonTrail = PersonTrailRepository.Get( int.Parse( id ) );
					if ( PersonTrail.Authorized( "Edit", user ) )
					{
						PersonTrailRepository.Delete( PersonTrail, user.PersonId );
						PersonTrailRepository.Save( PersonTrail, user.PersonId );
					}
					else
						throw new WebFaultException<string>( "Not Authorized to Edit this PersonTrail", System.Net.HttpStatusCode.Forbidden );
				}
				else
					throw new WebFaultException<string>( "Invalid API Key", System.Net.HttpStatusCode.Forbidden );
            }
        }

    }
}
