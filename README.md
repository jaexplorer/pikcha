


# API Documentation


# Images

### Filter Images - random/ pikcha100
- ENDPOINT : api/filter/images 
- METHOD : get
- AUTHENTICATED : false
- QUERY_PARAMS : Type = random/ [string, Default=random], Start [int, Optional], Count [int, Optional]
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
 - Height
 - TotalPhotosSold
 - AveragePriice
 - Sellers
 

### Get an Image by image id
- ENDPOINT : api/image/{imageId} 
- METHOD : get
- AUTHENTICATED : false
- RESULTS : image
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
 - Height
 - TotalImageSold
 - AveragePrice
 - Sellers



### Upload
- ENDPOINT : api/image/upload
- METHOD : post
- AUTHENTICATED : true
- PARAMS : Title [string], Caption [text], Location [string], ImageFile [file], Tags [list of strings]
- RESULTS : OK
- ERROR_CODES : 201, 500


### add view count
- ENDPOINT : api/image/views/increment/{imageId}
- METHOD : post
- AUTHENTICATED : false
- RESULTS : OK
- ERROR_CODES : 201, 500



# Artists

### Filter Artists - random/ artists100
- ENDPOINT : api/filter/artists 
- METHOD : get
- AUTHENTICATED : false
- QUERY_PARAMS : Type=random/ artists100 [string, Default=random], Start [int, Optional], Count [int, Optional]
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
 - SocialLinks
 - Performance
 - TotalImageSold
 - AveragePrice

### Get an Artist
- ENDPOINT : api/profile/{userId} 
- METHOD : get
- AUTHENTICATED : false
- RESULTS : artist
- ERROR_CODES : 200, 404, 500


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
 - SocialLinks
 - Performance
 - TotalImageSold
 - AveragePrice
 - Following

### Get the loggedin user info 
- ENDPOINT : api/profile/myinfo/{userId} 
- METHOD : get
- AUTHENTICATED : true
- RESULTS : loggedinuserinfo
- ERROR_CODES : 200, 404, 500


LOGGEDINUSERINFO
- Id
- AvatarFileName
- FirstName
- LastName
- BioInfo
- Location
- Email
- PhoneNumber
- ShipAddress1
- ShipAddress2
- ShipCity
- ShipPostalCode
- ShipCountry
- Sociallinks - json string
- Following[] - an array of artists the user is following (E.g [{Id, Firstname, Lastname, AvatarFileName}])
- Role - string [ comma seperated roles]

### Update user info
- ENDPOINT : api/profile/{userId} 
- METHOD : PUT
- AUTHENTICATED : true
- PARAMS : FirstName, LastName, BioInfo, Sociallinks, ShipAddress1 , ShipAddress2, ShipCity, ShipPostalCode, ShipCountry
- RESULTS : loggedinuserinfo
- ERROR_CODES : 200, 404, 500

### Promote a user to an artist 
- ENDPOINT : api/profile/artist/promote/{userId} 
- METHOD : post
- AUTHENTICATED : true
- PARAMS : signatureFile [file]
- RESULTS : loggedinuserinfo
- ERROR_CODES : 200, 404, 500

### Follow an artist 
- ENDPOINT : api/profile/artist/follow/{artistId}/{userId} 
- METHOD : post
- AUTHENTICATED : true
- RESULTS : loggedinuserinfo
- ERROR_CODES : 200, 404, 500


### Unfollow an artist 
- ENDPOINT : api/profile/artist/unfollow/{artistId}/{userId}  
- METHOD : post
- AUTHENTICATED : true
- RESULTS : loggedinuserinfo
- ERROR_CODES : 200, 404, 500


### Upload user avatar 
- ENDPOINT : api/profile/avatar/{userId}
- METHOD : post
- AUTHENTICATED : true
- PARAMS : imageFile [file]
- RESULTS : loggedinuserinfo
- ERROR_CODES : 200, 404, 500
