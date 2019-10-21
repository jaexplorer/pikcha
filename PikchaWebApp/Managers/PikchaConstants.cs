using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikchaWebApp.Managers
{
    public class PikchaConstants
    {

        // pikcha roles
        public const string PIKCHA_ROLES_USER_NAME = "User"; 
        public const string PIKCHA_ROLES_PHOTOGRAPHER_NAME = "Photographer"; 
        public const string PIKCHA_ROLES_ADMIN_NAME = "Picka-admin";
        public const string PIKCHA_USER_DEFAULT_AVATAR = @"Uploads\Avatars\default-avatar.jpg";

        // image
        public const string PIKCHA_IMAGE_SAVE_EXTENTION = ".jpg";
        public const string PIKCHA_IMAGE_UPLOAD_ROOT_FOLDER = @"wwwroot\";

        // product
        public const string PIKCHA_PRODUCT_TYPE_OWNER = "Primary";
        public const string PIKCHA_PRODUCT_TYPE_SELLER = "Secondary";

    }
}
