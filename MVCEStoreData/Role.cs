using Microsoft.AspNetCore.Identity;



namespace MvcEStoreData
{
    public class Role :IdentityRole<int>
    { 


        #region Properties

        public string FriendlyName { get; set; }
       
        #endregion

        #region Navigation

        

        #endregion


    }
}