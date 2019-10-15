


# API Documentation

Other than the authentication endpoints, output of all other endpoints have the following structure
[ Statuscode="", Status="", Data=""]
for eg. successfull query [ Statuscode="200", Status="Success", Data= {Results Set}]
failure query [ Statuscode="1222", Status="Null Exception Occured", Data= ""]


* isauthenticated means whether the user needs to login to access the end point



# Images

### Filter Images - random
- ENDPOINT : api/filter/images 
- METHOD : get
- AUTHENTICATED : false
- QUERY_PARAMS : Type = random [string, Default=random], Start [int, Optional], Count [int, Optional]
- RESULTS : images[]
- ERROR_CODES : 200, 416, 500

Image
 - PikchaImageId
 - Title
 - Caption 
 - Location 
 - ThumbnailFile 
 - WatermarkedFile 
 - ArtisitId
 - ArtistFirstname 
 - ArtistLastname 
 - ArtistPercity 
 - ArtistPercountry 
 - ArtistAvatarfilename 
 - TotalViews



### Filter Images - pikcha100
- ENDPOINT : api/filter/images 
- METHOD : get
- AUTHENTICATED : false
- QUERY_PARAMS : Type= pikcha100 [string], Start [int, Optional, Default=0], Count [int, Optional, Default=20]
- RESULTS : images[]
- ERROR_CODES : 200, 416, 500

Image
 - PikchaImageId
 - Title
 - Location 
 - ThumbnailFile 
 - WatermarkedFile 
 - ArtistFirstname 
 - ArtistLastname 
 - ArtistPercity 
 - ArtistPercountry 
 - ArtistAvatarfilename 
 - TotalViews


### Get an Image
- ENDPOINT : api/image/{imageId} 
- METHOD : get
- AUTHENTICATED : false

- RESULTS : image
- ERROR_CODES : 200, 416, 500

Image
 - PikchaImageId
 - Title
 - Location 
 - ThumbnailFile 
 - WatermarkedFile 
 - ArtistFirstname 
 - ArtistLastname 
 - ArtistPercity 
 - ArtistPercountry 
 - ArtistAvatarfilename 
 - TotalViews
 - Width
 - Height
 - Caption



### Upload
- ENDPOINT : api/image/upload
- METHOD : post
- AUTHENTICATED : true
- PARAMS : Title [string], Caption [text], Location [string], NumberOfPrint [int], ImageFile [file]
- RESULTS : OK
- ERROR_CODES : 201, 500


### add view count
- ENDPOINT : api/image/incrementviewcount/{imageId}
- METHOD : post
- AUTHENTICATED : false
- RESULTS : OK
- ERROR_CODES : 201, 500

### Tags
- ENDPOINT : api/image/tags
- METHOD : get
- AUTHENTICATED : false
- RESULTS : tags[]
- ERROR_CODES : 200, 500


# Artists

### Filter Artists - random
- ENDPOINT : api/filter/artists 
- METHOD : get
- AUTHENTICATED : false
- QUERY_PARAMS : Type=random [string, Default=random], Start [int, Optional], Count [int, Optional]
- RESULTS : artists[]
- ERROR_CODES : 200, 416, 500

Artist
 - FirstName 
 - LastName
 - PerCountry
 - TotalImageViews
 - TopImageTitle
 - TopImageLocation
 - TopImageThumbnailFile
 - TopImageWatermarkedFile
 - TopImageTotalViews

### Filter Artists - artists100
- ENDPOINT : api/filter/artists 
- METHOD : get
- AUTHENTICATED : false
- QUERY_PARAMS : Type=artists100 [string], Start [int, Optional], Count [int, Optional]
- RESULTS : artists[]
- ERROR_CODES : 200, 416, 500

Artist
 - FirstName 
 - LastName
 - PerCountry
 - TotalImageViews
 - TopImageTitle
 - TopImageLocation
 - TopImageThumbnailFile
 - TopImageWatermarkedFile
 - TopImageTotalViews

### Get an Artist
- ENDPOINT : api/profile/{userId} 
- METHOD : get
- AUTHENTICATED : true
- RESULTS : artist
- ERROR_CODES : 200, 416, 500


ARTIST
 - FirstName
 - LastName 
 - BioInfo 
 - PerAddress1 
 - PerAddress2
 - PerCity 
 - PerPostalCode 
 - PerCountry 
 - ShipAddress1 
 - ShipAddress2 
 - ShipCity 
 - ShipPostalCode 
 - ShipCountry 
 - FacebookLink
 - InstagramLink
 - LinkedInLink

### Get the loggedin user info 
- ENDPOINT : api/profile/loggedinuserinfo/{userId} 
- METHOD : get
- AUTHENTICATED : true
- RESULTS : loggedinuserinfo
- ERROR_CODES : 200, 416, 500


LOGGEDINUSERINFO
 - FirstName
 - LastName 
 - IsPhotoGrapher 
 - LastUploadedOn


### Promote a user to a photographer 
- ENDPOINT : api/profile/promotetophotographer/{userId} 
- METHOD : post
- AUTHENTICATED : true
- RESULTS : SUCCESS
- ERROR_CODES : 200, 416, 500


### Upload user avatar 
- ENDPOINT : api/profile/avatar
- METHOD : post
- AUTHENTICATED : true
- RESULTS : SUCCESS
- ERROR_CODES : 200, 416, 500
